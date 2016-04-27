using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioEvent : MonoBehaviour
{
    public bool setTrigger = false;
    public Animator animator;
    public string startTrigger, endTrigger;
    public Button.ButtonClickedEvent onStart, onEnd;

    private AudioSource audioSource;
    private bool isStarted = false;

	void Start ()
    {
		audioSource = GetComponent<AudioSource>();
		ValidateUtils.Validate (gameObject, onStart);
		ValidateUtils.Validate (gameObject, onEnd);
    }
	
	void Update ()
    {
        if (audioSource.isPlaying && !isStarted) OnStart();
        if (!audioSource.isPlaying && isStarted) OnEnd();

        isStarted = audioSource.isPlaying;
    }

    void OnEnd()
    {
        onEnd.Invoke();
        if (setTrigger) animator.SetTrigger(endTrigger);
    }

    void OnStart()
    {
        onStart.Invoke();
        if (setTrigger) animator.SetTrigger(startTrigger);
    }
}
