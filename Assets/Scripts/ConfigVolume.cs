using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;
using System;

[RequireComponent(typeof(Slider))]
public class ConfigVolume : MonoBehaviour
{
    public enum VolumeType {
        MASTER,
        MUSIC,
        SOUNDS,
        UISOUNDS
    }

    public enum AudioType
    {
        MUSIC,
        SOUND,
        UISOUND
    }

    public VolumeType volume;

    [Header("Tester")]
    public AudioType playmode;
    public AudioClip testAudio;
    
    [HideInInspector]
    public int persent;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>()
            .onValueChanged
            .AsObservable()
            .StartWith(GetVolume(volume))
            .Subscribe(value =>
            {
                persent = (int)(value * 100);
                SetVolume(volume, value);
            })
            .AddTo(this);

        GetComponent<Slider>()
            .onValueChanged
            .AsObservable()
            .Sample(TimeSpan.FromMilliseconds(2))
            .Subscribe(_ =>
            {
                Play(playmode, testAudio);
            })
            .AddTo(this);
    }

    private float GetVolume(VolumeType type)
    {
        switch(type)
        {
            case VolumeType.MASTER:
                return EazySoundManager.GlobalVolume;
            case VolumeType.MUSIC:
                return EazySoundManager.GlobalMusicVolume;
            case VolumeType.SOUNDS:
                return EazySoundManager.GlobalSoundsVolume;
            case VolumeType.UISOUNDS:
                return EazySoundManager.GlobalUISoundsVolume;
            default:
                return -1f;
        }
    }

    private void SetVolume(VolumeType type, float volume)
    {
        switch (type)
        {
            case VolumeType.MASTER:
                EazySoundManager.GlobalVolume = volume;
                break;
            case VolumeType.MUSIC:
                EazySoundManager.GlobalMusicVolume = volume;
                break;
            case VolumeType.SOUNDS:
                EazySoundManager.GlobalSoundsVolume = volume;
                break;
            case VolumeType.UISOUNDS:
                EazySoundManager.GlobalUISoundsVolume = volume;
                break;
        }
        
    }

    private void Play(AudioType type, AudioClip clip)
    {
        switch(type)
        {
            case AudioType.MUSIC:
                EazySoundManager.PlayMusic(clip);
                break;
            case AudioType.SOUND:
                EazySoundManager.PlaySound(clip);
                break;
            case AudioType.UISOUND:
                EazySoundManager.PlayUISound(clip);
                break;
        }
    }
}
