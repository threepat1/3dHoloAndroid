using UnityEngine;
using System.Collections;


// macro definition rotation observation
[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class RotateModel : MonoBehaviour
{
    private Touch oldTouch1; // Last touch point 1 (finger 1)  
    private Touch oldTouch2; // Last touch point 2 (finger 2)  
    private Touch newTouch1; //new touch point 1
    private Touch newTouch2; //new touch point

    Vector2 m_screenPos = new Vector2(); //record where the finger touched

    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 3;
    public float minDistance = 1.5f;
    public float maxDistanceP = 5;
    public float minDistanceP = 3f;
    public float xSpeed = 100.0f;
    public float ySpeed = 100.0f;
    public int yMinLimit = 0;
    public int yMaxLimit = 90;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    void Start() { Init(); }
    void OnEnable() { Init(); }

    public void Init()
    {
        //If there is no target, create a temporary target current camera "distance" point of view
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //The current rotation is used as the starting point.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    void LateUpdate()
    {
#if UNITY_ANDROID  
        if (Input.touchCount <= 0)
        {
            return;

        }
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                m_screenPos = Input.touches[0].position; //record the location where the finger just touched
            if (Input.touches[0].phase == TouchPhase.Moved) //The finger moves on the screen, moving the camera
            {
                //transform.Translate(new Vector3(Input.touches[0].deltaPosition.x * Time.deltaTime, Input.touches[0].deltaPosition.y * Time.deltaTime, 0));
                xDeg += Input.touches[0].deltaPosition.x * xSpeed * 0.02f;
                yDeg -= Input.touches[0].deltaPosition.y * ySpeed * 0.02f;
            }
            //xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            //yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
        else if (Input.touchCount > 1)
        {
            //Multiple touch, zoom in and out  
            newTouch1 = Input.GetTouch(0);
            newTouch2 = Input.GetTouch(1);
        }
        //The second point is just touching the screen, only recording, no processing  
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        // Calculate the old two - point distance and the new distance between the two points, become larger to enlarge the model, become smaller to scale the model

        // The difference between the two distances, positive for the zoom gesture, negative for the zoom gesture
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
        float offset = newDistance - oldDistance;

#endif


#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
#endif

        //The vertical axis orbit of the angle
        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
        // Set the camera rotation

        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;


        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;

#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN
        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
#endif

#if UNITY_ANDROID  
        desiredDistance -= offset * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
#endif
        if(Screen.orientation == ScreenOrientation.Landscape) { 
        //clamp the zoom min/max
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        }
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            //clamp the zoom min/max
            desiredDistance = Mathf.Clamp(desiredDistance, minDistanceP, maxDistanceP);
        }

        // For smoothing of the zoom, lerp distance
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        // calculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
#if UNITY_ANDROID
        //Remember the latest touch points, next time  
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
#endif
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
