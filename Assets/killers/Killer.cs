using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Killer : ActionTrigger 
{
    [Space]
    public AudioSource diedAudio;
    public float playDelay = 0.2f;
    public float randomPitchOffset = 0.5f;


    private float originalPitch;
    private float lastPlayTime = 0;

    void Start()
    {
        if (diedAudio != null) originalPitch = diedAudio.pitch;
    }

    protected override void OnDetect(GameObject gObject)
    {
        base.OnDetect(gObject);
        Killable killable = gObject.GetComponent<Killable>();
        
        if (killable != null)
            killable.Kill();
        PlayAudio();
    }

    void PlayAudio()
    {
        if (diedAudio != null && lastPlayTime + playDelay < Time.time)
        {
            lastPlayTime = Time.time;
            diedAudio.pitch = originalPitch + originalPitch * Random.Range(randomPitchOffset / -2f, randomPitchOffset / 2f);
            diedAudio.Play();
        }
    }
}
