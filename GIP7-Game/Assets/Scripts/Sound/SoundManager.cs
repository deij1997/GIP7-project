using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    private static SoundManager instance = null;
    public static SoundManager Instance { get { return (instance != null) ? instance : (Instantiate(Resources.Load("Audio/AudioManager", typeof(GameObject))) as GameObject).GetComponent<SoundManager>(); } }

#if UNITY_EDITOR
    [Range(0, 100)]
    public int master = 80;

    [Range(0, 100)]
    public int menu = 80;

    [Range(0, 100)]
    public int music = 80;

    [Range(0, 100)]
    public int game = 80;

    [Range(0, 100)]
    public int ambient = 80;

    [Range(0, 100)]
    public int helper = 80;

    [Range(0, 100)]
    public int player = 80;

    [Range(0, 100)]
    public int characters = 80;
#endif

    public bool destroy = false;

    private ObjectSound[] objectSounds;

    public ObjectSound[] ObjectSounds { get { return objectSounds; } }

    private void Awake()
    {
        DontDestroyObject();
    }

    private void DontDestroyObject()
    {
        if (!destroy)
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        objectSounds = FindObjectsOfType<ObjectSound>();
        Instance.Play(AudioParamater.Ambient);
    }


    private void FixedUpdate()
    {
#if UNITY_EDITOR
        SetAudio();
#endif
    }

#if UNITY_EDITOR
    private void SetAudio()
    {
        SetVolumeOnDifference(AudioParamater.Ambient, ambient);
        SetVolumeOnDifference(AudioParamater.Characters, characters);
        SetVolumeOnDifference(AudioParamater.Game, game);
        SetVolumeOnDifference(AudioParamater.Helper, helper);
        SetVolumeOnDifference(AudioParamater.Master, master);
        SetVolumeOnDifference(AudioParamater.Menu, menu);
        SetVolumeOnDifference(AudioParamater.Music, music);
        SetVolumeOnDifference(AudioParamater.Player, player);
    }
#endif

    /// <summary>
    /// Finds all objects using the ObjectSound script.
    /// </summary>
    public void FindAudioObjects()
    {
        objectSounds = FindObjectsOfType<ObjectSound>();
    }


    /// <summary>
    /// Plays all audiosources.
    /// </summary>
    public void Play()
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                objSound.Play();
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
    }

    /// <summary>
    /// Plays all audiosources from the given channel.
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Play(AudioParamater ap)
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                if (objSound.audioChannel == ap)
                {
                    objSound.Play();
                }
            }
        }
        else
        {
            Debug.LogWarning("No audio objects found!");
        }
    }

    /// <summary>
    /// Stops all audiosources
    /// </summary>
    public void Stop()
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                objSound.Stop();
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
    }

    /// <summary>
    /// Stops all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Stop(AudioParamater ap)
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                if (objSound.audioChannel == ap)
                {
                    objSound.Stop();
                }
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");

        }
    }

    /// <summary>
    /// Pauses all audiosources
    /// </summary>
    public void Pause()
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                objSound.Pause();
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
    }

    /// <summary>
    /// Pauses all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void Pause(AudioParamater ap)
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                if (objSound.audioChannel == ap)
                {
                    objSound.Pause();
                }
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
    }

    /// <summary>
    /// UnPauses all audiosources
    /// </summary>
    public void UnPause()
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                objSound.UnPause();
            }
        }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
    }

    /// <summary>
    /// UnPauses all audiosources
    /// </summary>
    /// <param name="ap">The audiochannel</param>
    public void UnPause(AudioParamater ap)
    {
        if (objectSounds.Length > 0)
        {
            foreach (ObjectSound objSound in objectSounds)
            {
                if (objSound.audioChannel == ap)
                {
                    objSound.UnPause();
                }
            }
                    }
        else
        {
            Debug.LogWarning("No audio object found!");
        }
        }

    /// <summary>
    /// Gets the volume of the given channel.
    /// </summary>
    /// <param name="ap">The audio channel you want to know the volume of</param>
    /// <returns>The currentlevel of the volume</returns>
    public int GetVolume(AudioParamater ap)
    {
        float apVolume;
        audioMixer.GetFloat(Volume.GetVolumeParamater(ap), out apVolume);
        Debug.Log("Audio for " + Volume.GetVolumeParamater(ap) + " is " + apVolume);
        return (int)apVolume;
    }

    /// <summary>
    /// Checks for the current volume and changes it if its different with the volume paramater.
    /// </summary>
    /// <param name="ap">The audio channel you want to check for a volume change</param>
    /// <param name="volume">The volume you want to check on</param>
    /// <returns>The current volume</returns>
    public int SetVolumeOnDifference(AudioParamater ap, int volume = 80)
    {
        if (volume < 0 || volume > 100) throw new System.Exception("The volume value cannot be lower than 0 or higher than 100");

        float mixerVolume;
        audioMixer.GetFloat(Volume.GetVolumeParamater(ap), out mixerVolume);

        if ((volume - 80) != (int)mixerVolume)
        {
            SetVolume(ap, volume);
            return volume;
        }
        else
        {
            return volume;
        }
    }

    /// <summary>
    /// Sets the volume of a certain audio channel.
    /// </summary>
    /// <param name="ap">The audio channel you want to change</param>
    /// <param name="volume">The volume of the audiochannel</param>
    public void SetVolume(AudioParamater ap, int volume = 80)
    {
        if (volume < 0 || volume > 100) throw new System.Exception("The volume value cannot be lower than 0 or higher than 100");

        float apVolume = (float)(volume - 80);
        Debug.Log("Setting volume to:" + apVolume + " on " + Volume.GetVolumeParamater(ap));
        audioMixer.SetFloat(Volume.GetVolumeParamater(ap), apVolume);
    }

    /// <summary>
    /// Plays button click sound.
    /// </summary>
    public void PlayButtonClickSound()
    {
        Instance.ObjectSounds[0].PlayAudioClip(3);
    }
}

public static class Volume
{
    private const string MASTER = "masterVolume";
    private const string MENU = "menuVolume";
    private const string MUSIC = "musicVolume";
    private const string GAME = "gameVolume";
    private const string AMBIENT = "ambientVolume";
    private const string HELPER = "helperVolume";
    private const string PLAYER = "playerVolume";
    private const string CHARACTERS = "characterVolume";

    private const string MASTERPATH = "Master";
    private const string MENUPATH = "Master/Menu";
    private const string MUSICPATH = "Master/Music";
    private const string GAMEPATH = "Master/Game";
    private const string AMBIENTPATH = "Master/Game/Ambient";
    private const string HELPERPATH = "Master/Game/Helper";
    private const string PLAYERPATH = "Master/Game/Player";
    private const string CHARACTERSPATH = "Master/Game/Characters";

    public static string GetVolumeParamater(AudioParamater ap)
    {
        switch (ap)
        {
            case AudioParamater.Ambient:
                return AMBIENT;
            case AudioParamater.Characters:
                return CHARACTERS;
            case AudioParamater.Game:
                return GAME;
            case AudioParamater.Helper:
                return HELPER;
            case AudioParamater.Master:
                return MASTER;
            case AudioParamater.Menu:
                return MENU;
            case AudioParamater.Music:
                return MUSIC;
            case AudioParamater.Player:
                return PLAYER;
            default:
                return null;
        }
    }

    public static AudioMixerGroup GetMixerGroup(AudioMixer mixer, AudioParamater ap)
    {
        switch (ap)
        {
            case AudioParamater.Ambient:
                return mixer.FindMatchingGroups(AMBIENTPATH)[0];
            case AudioParamater.Characters:
                return mixer.FindMatchingGroups(CHARACTERSPATH)[0];
            case AudioParamater.Game:
                return mixer.FindMatchingGroups(GAMEPATH)[0];
            case AudioParamater.Helper:
                return mixer.FindMatchingGroups(HELPERPATH)[0];
            case AudioParamater.Master:
                return mixer.FindMatchingGroups(MASTERPATH)[0];
            case AudioParamater.Menu:
                return mixer.FindMatchingGroups(MENUPATH)[0];
            case AudioParamater.Music:
                return mixer.FindMatchingGroups(MUSICPATH)[0];
            case AudioParamater.Player:
                return mixer.FindMatchingGroups(PLAYERPATH)[0];
            default:
                return null;
        }
    }

    public static AudioMixerGroup GetMixerParentGroup(AudioMixer mixer, AudioParamater ap)
    {
        switch (ap)
        {
            case AudioParamater.Ambient:
                return mixer.FindMatchingGroups(GAMEPATH)[0];
            case AudioParamater.Characters:
                return mixer.FindMatchingGroups(GAMEPATH)[0];
            case AudioParamater.Game:
                return mixer.FindMatchingGroups(MASTERPATH)[0];
            case AudioParamater.Helper:
                return mixer.FindMatchingGroups(GAMEPATH)[0];
            case AudioParamater.Master:
                return mixer.FindMatchingGroups(MASTERPATH)[0];
            case AudioParamater.Menu:
                return mixer.FindMatchingGroups(MASTERPATH)[0];
            case AudioParamater.Music:
                return mixer.FindMatchingGroups(MASTERPATH)[0];
            case AudioParamater.Player:
                return mixer.FindMatchingGroups(GAMEPATH)[0];
            default:
                return null;
        }
    }
}

public enum AudioParamater
{
    Master,
    Menu,
    Music,
    Game,
    Ambient,
    Helper,
    Player,
    Characters
}