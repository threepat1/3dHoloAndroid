using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayableBehaviourPRM : PlayableBehaviour
{
    [SerializeField]
    private float firstSec=0;
    [SerializeField]
    private float lastSec=0;
    [SerializeField]
    private string url;

    private float speed;
    private bool isEditorMode;
    private bool isRuntimePlaying;
    private MeshTimelinePRM meshTimelinePRM;
    private PlayableDirector director;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        //Debug.Log("OnGraphStart");
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        //Debug.Log("OnGraphStop");
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.LogWarning("OnBehaviourPlay");
        speed = CalculateSpeed(playable.GetDuration());
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Debug.Log("OnBehaviourPause");
        if (meshTimelinePRM)
        {
            meshTimelinePRM.StopPlaying();
        }

        isRuntimePlaying = false;
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        CheckIsEditorMode(playable);
        if (!meshTimelinePRM)
        {
            meshTimelinePRM = playerData as MeshTimelinePRM;
        }

        if (isEditorMode)
        {
            meshTimelinePRM.SetSpeed(speed);
            meshTimelinePRM.TimelinePreview(url,firstSec + playable.GetTime()* speed);
        }
        else {
            if (!isRuntimePlaying) {
                isRuntimePlaying = true;
                //Debug.LogWarning("isRuntimePlaying");
                meshTimelinePRM.SetSpeed(speed);
                meshTimelinePRM.StartPlay(url,firstSec + playable.GetTime());
            }    
        }
   
        base.ProcessFrame(playable, info, playerData);
    }

    public void CheckIsEditorMode(Playable playable) {
        if (director == null)
        {
            director = (playable.GetGraph().GetResolver() as PlayableDirector);
        }

        if (!Application.isPlaying && director.state == PlayState.Paused)
        {
            isEditorMode = true;
        }
        else
        {
            isEditorMode = false;
        }
    }

    public float CalculateSpeed(double duration) {
        float newSpeed = 1;

        if (lastSec==-1) {
            return 1;
        }

        if (firstSec < lastSec && firstSec >= 0)
        {
            newSpeed = (lastSec - firstSec) / (float)duration;
        }
        return newSpeed;
    }
}
