using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class PlayableAssetPRM : PlayableAsset, ITimelineClipAsset
{
    public PlayableBehaviourPRM promePlayableBehaviour;
    public ClipCaps clipCaps => ClipCaps.None;
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<PlayableBehaviourPRM>.Create(graph, promePlayableBehaviour);
    }
}
