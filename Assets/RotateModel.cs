using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateModel : MonoBehaviour
{
	public Transform target;
	public int MouseWheelSensitivity = 1; //Roller sensitivity setting
	public int MouseZoomMin = 3; // camera distance minimum
	public int MouseZoomMax = 5; // camera distance maximum

	public float moveSpeed = 10; //The camera follows the speed (when the middle button is panned), it works when using the smooth mode, and the bigger the motion, the smoother the motion.

	private float xSpeed = 250.0f; // Camera x-axis speed when rotating the angle of view
	private float ySpeed = 120.0f; // Camera y-axis speed when rotating the angle of view

	public int yMinLimit = -360;
	public int yMaxLimit = 360;

	private float x = 0.0f; // Storage camera euler angle
	private float y = 0.0f; // Storage camera euler angle

	private float Distance = 3; //The distance between the camera and the target, because the Z axis of the camera always points to the target, which is the distance in the z-axis direction of the camera.
	private Vector3 targetOnScreenPosition; //target screen coordinates, the third value is the z-axis distance
	private Quaternion storeRotation; // Store the camera's attitude quaternion
	private Vector3 CameraTargetPosition; //target location
	private Vector3 initPosition; // used to store the starting position of the translation when panning
	private Vector3 cameraX; // camra x-axis direction vector
	private Vector3 cameraY; // camera y-axis direction vector
	private Vector3 cameraZ; // camera z-axis direction vector

	private Vector3 initScreenPos; //The screen coordinates of the mouse when the middle button is pressed (the third value is actually useless)
	private Vector3 curScreenPos; // The current mouse screen coordinates(the third value is actually useless)
	void Start()
	{
		//This is the initial camera view and some other variables, x and y here. . . Is corresponding to mouse x and mouse y of getAxis below
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		CameraTargetPosition = target.position;
		storeRotation = Quaternion.Euler(y + 60, x, 0);
		transform.rotation = storeRotation; // Set the camera pose
		Vector3 position = storeRotation * new Vector3(0.0F, 0.0F, -Distance) + CameraTargetPosition; //The quaternion represents a rotation, the quaternion multiplied by the vector is equivalent to rotating the vector to the corresponding angle, and then the position of the target object is the camera position.
		transform.position = storeRotation * new Vector3(0, 0, -Distance) + CameraTargetPosition; // Set the camera position

		// Debug.Log("Camera x: "+transform.right);
		// Debug.Log("Camera y: "+transform.up);
		// Debug.Log("Camera z: "+transform.forward);

		// //-------------TEST-----------------
		// testScreenToWorldPoint();
		
	}

	void Update()
	{
		// right mouse button rotation function
		if (Input.GetMouseButton(0))
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			storeRotation = Quaternion.Euler(y + 60, x, 0);
			var position = storeRotation * new Vector3(0.0f, 0.0f, -Distance) + CameraTargetPosition;

			transform.rotation = storeRotation;
			transform.position = position;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") != 0) // mouse wheel zoom function
		{
			if (Distance >= MouseZoomMin && Distance <= MouseZoomMax)
			{
				Distance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
			}
			if (Distance < MouseZoomMin)
			{
				Distance = MouseZoomMin;
			}
			if (Distance > MouseZoomMax)
			{
				Distance = MouseZoomMax;
			}
			var rotation = transform.rotation;

			transform.position = storeRotation * new Vector3(0.0F, 0.0F, -Distance) + CameraTargetPosition;
		}

		// Mouse middle button translation
		if (Input.GetMouseButtonDown(2))
		{
			cameraX = transform.right;
			cameraY = transform.up;
			cameraZ = transform.forward;

			initScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);
			Debug.Log("downOnce");

			//targetOnScreenPosition.z is the normal distance from the target object to the xmidbuttonDownPositiony plane of the camera.
			targetOnScreenPosition = Camera.main.WorldToScreenPoint(CameraTargetPosition);
			initPosition = CameraTargetPosition;
		}

		if (Input.GetMouseButton(2))
		{
			curScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);
			//0.01 This coefficient is the speed of controlling the translation. It should be flexibly selected according to the distance between the camera and the target object.
			target.position = initPosition - 0.01f * ((curScreenPos.x - initScreenPos.x) * cameraX + (curScreenPos.y - initScreenPos.y) * cameraY);

			// Recalculate the position
			Vector3 mPosition = storeRotation * new Vector3(0.0F, 0.0F, -Distance) + target.position;
			transform.position = mPosition;

			// //Use this to make the camera's panning smoother, but it may not move the camera to the desired position when you buttonup, causing a brief jitter when rotating and zooming.
			//transform.position=Vector3.Lerp(transform.position,mPosition,Time.deltaTime*moveSpeed);

		}
		if (Input.GetMouseButtonUp(2))
		{
			Debug.Log("upOnce");
			// End of the translation to update the position of the cameraTargetPosition, otherwise it will affect the zoom and rotate function
			CameraTargetPosition = target.position;
		}

	}

	// Limit the angle between min ~max
	static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}
