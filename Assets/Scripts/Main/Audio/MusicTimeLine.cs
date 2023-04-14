using UnityEngine;

public class MusicTimeLine : MonoBehaviour
{

    public void Play() => MusicPlayer.Play();
    public void Pause() => MusicPlayer.Pause();
    public void Resume() => MusicPlayer.Resume();
    public void Stop() => MusicPlayer.Stop();
    public void Rewind(float time) => MusicPlayer.RewindMusic(time);
    public void SelectThisStartOffset()
    {

    }
}
