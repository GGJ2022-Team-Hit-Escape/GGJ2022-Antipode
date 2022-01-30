using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip intro;
    [SerializeField]
    private AudioClip loop;

    private AudioSource introAudioSource;
    private AudioSource loopAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        introAudioSource = gameObject.AddComponent<AudioSource>();
        introAudioSource.clip = intro;
        introAudioSource.Play();
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource.Stop();
        loopAudioSource.clip = loop;
        loopAudioSource.PlayScheduled(AudioSettings.dspTime + intro.length);
        loopAudioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
