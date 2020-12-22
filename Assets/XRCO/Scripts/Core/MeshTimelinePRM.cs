using prometheus;
using UnityEngine;

[ExecuteInEditMode]
public class MeshTimelinePRM : MonoBehaviour
{
    public float speed;
    public double time;
    private MeshPlayerPRM meshPlayerPRM;
    public MeshFilter meshFilter;
    public bool hideModelInPause;
    [HideInInspector]
    public bool runInTimeline;
    private void Awake()
    {
        runInTimeline = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
        if (hideModelInPause) {
         
            meshFilter.mesh = null;
        }    
    }

    [ExecuteInEditMode]
    public void Update()
    {
        if (runInTimeline) {
            meshPlayerPRM.Update();
            //Debug.Log("RunInTimeLine");
        }
    }
    public void StartPlay(string sourceURL,double timeLineStartTime) {
        GetMeshPlayComp();
        if (Application.isPlaying)
        {
            meshFilter.mesh = null;
            //Debug.LogWarning($"{timeLineStartTime} StartPlay");
            time = timeLineStartTime;

            if (!string.IsNullOrEmpty(sourceURL))
            {
                meshPlayerPRM.sourceUrl = sourceURL;
            }

            meshPlayerPRM.OpenCurrentSource((float)timeLineStartTime, true);
      
        }
        else {
            //Debug.LogError("RunInTimeLine");
            meshFilter.mesh = null;
            //Debug.LogWarning($"{timeLineStartTime} StartPlay");
            time = timeLineStartTime;

            if (!string.IsNullOrEmpty(sourceURL))
            {
                meshPlayerPRM.sourceUrl = sourceURL;
            }

            meshPlayerPRM.OpenCurrentSource((float)timeLineStartTime, true);
            runInTimeline = true;
        }  
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
        GetMeshPlayComp();
        meshPlayerPRM.SetSpeedRatio(speed);
    }

    public void TimelinePreview(string sourceURL, double timeLineTime)
    {
        time = timeLineTime;    

        GetMeshPlayComp();

        if (!string.IsNullOrEmpty(sourceURL))
        {
            meshPlayerPRM.sourceUrl = sourceURL;
        }

        meshPlayerPRM.Preview((float)time);
        meshPlayerPRM.Uninitialize();
    }

    public void ResetTime() {
        time = 0;
    }

    public void StopPlaying() {

        runInTimeline = false;
        if (hideModelInPause) {
            meshFilter.mesh = null;
        }
    
        ResetTime();
        GetMeshPlayComp();
        meshPlayerPRM.ClearBuffer();
        meshPlayerPRM.Pause();

    }

    public void GetMeshPlayComp() {
        if (meshPlayerPRM == null)
        {
            meshPlayerPRM = GetComponent<MeshPlayerPRM>();
        }
    }

}
