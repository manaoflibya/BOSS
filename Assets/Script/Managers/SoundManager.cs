using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{

    private static SoundManager instance = null; //private로 선언을 해도 전역 변수처럼 사용가능 하다

    public static SoundManager I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
                if (instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load("SoundManager")) as GameObject;
                    obj.name = "SoundManger";
                    instance = obj.GetComponent<SoundManager>();
                }
            }

            return instance;
        }
    }

    AudioSource _musicplayer = null;
    public AudioSource MusicPlayer
    {
        get
        {
            if (_musicplayer == null)
            {
                _musicplayer = Camera.main.GetComponent<AudioSource>();

            }
            return _musicplayer;

        }
    }

    float MusicVolume = 1f;
    float EffectVolume = 1f;

    private void Awake()
    {
        MusicVolume = 1f - PlayerPrefs.GetFloat("GameMusicVolume");
        EffectVolume = 1f - PlayerPrefs.GetFloat("GameEffectVolume");
    }
    AudioSource _effectSound = null;
    public AudioSource EffectSound
    {
        get
        {
            if (_effectSound == null)
            {
                _effectSound = this.GetComponent<AudioSource>();
            }
            return _effectSound;
        }
    }

    public void PlayBMG(AudioClip bgm, bool bLoop = true)
    {
        MusicPlayer.clip = bgm;
        MusicPlayer.loop = bLoop;
        MusicPlayer.Play();
    }
    public void SetMusicVoluem(float v)
    {
        MusicVolume = v;
        MusicPlayer.volume = v;
        PlayerPrefs.SetFloat("GameMusicVolume", 1f - v);
    }
    public void SetEffectVoluem(float v)
    {
        EffectVolume = v;
        EffectSound.volume = v;
        PlayerPrefs.SetFloat("GameEffectVolume", 1f - v);

    }


    public void PlayEffectSound(AudioClip eff, float pitch = 1.0f)
    {
        EffectSound.pitch = pitch;

        EffectSound.PlayOneShot(eff, EffectVolume);
        // EffectSound.PlayOneShot(eff);

        EffectSound.clip = eff;
        EffectSound.Play();

    }

    //단독으로 소리를 내야 할 때 이걸 사용함
    public void PlayEffectSound(GameObject obj, AudioClip eff)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        if (audio == null)
        {
            audio = obj.AddComponent<AudioSource>();

        }

        // audio.PlayOneShot(eff,EffectVolume);
        audio.PlayOneShot(eff);
    }
}



    

