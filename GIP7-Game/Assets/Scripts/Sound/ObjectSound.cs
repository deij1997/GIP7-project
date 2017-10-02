using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectSound : MonoBehaviour
{

    public AudioParamater audioChannel = AudioParamater.Master;
    public bool ChangeAudioMixerChannel = false;
    public bool GetAudioMixerParentChannel = false;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public bool loop;
    public bool playOnAwake;
    public AudioClip clipToPlayOnAwake;

    private AudioMixer audioMixer;

    // Use this for initialization
    void Start()
    {
        SetupAudioMixer();

        if (loop) audioSource.loop = loop;

        if (playOnAwake)
        {
            audioSource.clip = clipToPlayOnAwake;
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = audioClips[4];
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        SetChangesAudio();
#endif
    }

#if UNITY_EDITOR
    public void SetChangesAudio()
    {
        if (GetAudioMixerParentChannel)
        {
            AudioMixerGroup amg = Volume.GetMixerParentGroup(audioMixer, audioChannel);
            if(audioSource.outputAudioMixerGroup != amg)
            {
                audioSource.outputAudioMixerGroup = amg;
            }
        }
        else
        {
            AudioMixerGroup amg = Volume.GetMixerGroup(audioMixer, audioChannel);
            if (audioSource.outputAudioMixerGroup != amg)
            {
                audioSource.outputAudioMixerGroup = amg;
            }
        }
    }
#endif

    /// <summary>
    /// Setups the AudioMixer
    /// </summary>
    private void SetupAudioMixer()
    {
        audioMixer = Resources.Load("Audio/MasterMixer") as AudioMixer;

        if (audioSource == null)
        {
            gameObject.GetComponent<AudioSource>();
        }

        if (ChangeAudioMixerChannel)
        {
            if (GetAudioMixerParentChannel)
            {
                audioSource.outputAudioMixerGroup = Volume.GetMixerParentGroup(audioMixer, audioChannel);
            }
            else
            {
                audioSource.outputAudioMixerGroup = Volume.GetMixerGroup(audioMixer, audioChannel);
            }
        }
    }

    /// <summary>
    /// Changes the audiosources mixergroup.
    /// </summary>
    /// <param name="ap">The audiochannel you want to change the mixergroup to.</param>
    /// <param name="parent">If you want to change it to it's parent channel.</param>
    private void ChangeAudioMixerGroup(AudioParamater ap, bool parent = false)
    {
        if (parent)
        {
            audioSource.outputAudioMixerGroup = Volume.GetMixerParentGroup(audioMixer, ap);
        }
        else
        {
            audioSource.outputAudioMixerGroup = Volume.GetMixerGroup(audioMixer, ap);
        }
    }

    /// <summary>
    /// Plays the audiosource.
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }

    /// <summary>
    /// Pauses the audioSource.
    /// </summary>
    public void Pause()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// Resumes the audiosource.
    /// </summary>
    public void UnPause()
    {
        audioSource.UnPause();
    }

    /// <summary>
    /// Stops the audiosource
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Selects a audioclip from the array with audioclips;
    /// </summary>
    /// <param name="audioClip">The audioclip you want to pick.</param>
    public void SelectAudioClip(int audioClip)
    {
        audioSource.clip = audioClips[audioClip];
    }

    /// <summary>
    /// Plays a audioclip from the array with audioclips based on index.
    /// </summary>
    /// <param name="audioClip">The audioclip you want to play</param>
    public void PlayAudioClip(int audioClip)
    {
        audioSource.PlayOneShot(audioClips[audioClip]);
    }

    /// <summary>
    /// Plays a random audioclip from the objects audioclips.
    /// </summary>
    public void PlayRandomAudioClip()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }

    /// <summary>
    /// Selects a random audioclip from the objects audioclips.
    /// </summary>
    public void SelectRandomAudioClip()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
    }
}
