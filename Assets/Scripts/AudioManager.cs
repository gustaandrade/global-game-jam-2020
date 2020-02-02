using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer BGM;
    public AudioMixer SFX;

    private const float SOUND_OFF = -80f;
    private const float SOUND_ON = 0f;

    private bool _isBGMOn = true;
    private bool _isSFXOn = true;

    private void Start()
    {
        BGM.SetFloat("Volume", SOUND_ON);
        SFX.SetFloat("Volume", SOUND_ON);
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
}
