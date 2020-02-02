using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer BGM;
    public AudioMixer SFX;

    public GameObject BGMAudioSources;
    private List<AudioSource> _audioSources;

    public AudioClip NightClip;
    public AudioClip NightToDayClip;
    public AudioClip DayClip;
    public AudioClip DayToNightClip;

    private const float SOUND_OFF = -80f;
    private const float SOUND_ON = 0f;

    private bool _isBGMOn = true;
    private bool _isSFXOn = true;

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        BGM.SetFloat("Volume", SOUND_ON);
        SFX.SetFloat("Volume", SOUND_ON);

        _audioSources = BGMAudioSources.GetComponents<AudioSource>().ToList();
        StartNight();
    }

    public void ChangeBGM()
    {
        BGM.SetFloat("Volume", _isBGMOn ? SOUND_OFF : SOUND_ON);
        _isBGMOn = !_isBGMOn;
    }

    public void ChangeSFX()
    {
        SFX.SetFloat("Volume", _isSFXOn ? SOUND_OFF : SOUND_ON);
        _isSFXOn = !_isSFXOn;
    }

    public void StartNight()
    {
        _audioSources.ForEach(a => a.Stop());
        _audioSources.FirstOrDefault(a => a.clip == NightClip)?.Play();
    }

    public void TransitionToDay()
    {
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        //_audioSources.ForEach(a => a.Stop());
        //_audioSources.FirstOrDefault(a => a.clip == NightToDayClip)?.Play();

        //yield return new WaitForSeconds(NightToDayClip.length / 2);

        _audioSources.ForEach(a => a.Stop());
        _audioSources.FirstOrDefault(a => a.clip == DayClip)?.Play();

        yield return new WaitForSeconds(TimerManager.Instance.DayEventLimitTimer / 2);

        //_audioSources.ForEach(a => a.Stop());
        //_audioSources.FirstOrDefault(a => a.clip == DayToNightClip)?.Play();

        yield return new WaitForSeconds(DayToNightClip.length / 2);
    }
}
