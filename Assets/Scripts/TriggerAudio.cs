using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    public bool PlayOnAwake;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Event);
    }

    private void Start()
    {
        if (PlayOnAwake)
            PlayOneShot();
    }
}
