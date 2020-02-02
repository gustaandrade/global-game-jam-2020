using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Space(10), Header("Objects")]
    public RadialSlider GlobalTimerSlider;
    public Image GlobalTimerImage;
    public GameObject Transition;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI TimeOfDayText;

    [Space(10), Header("Values")]
    [SerializeField, ReadOnly] private float _globalTimer;
    public float GlobalTimerLimit;
    
    [SerializeField, ReadOnly] private float _callEventTimer;
    public float CallEventLimitTimer;

    private bool _rolling = true;
    private int _waveCount = 1;
    private bool _isNightTime = true;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void FixedUpdate()
    {
        TimeOfDayText.text = _isNightTime ? "Night" : "Day";
        WaveText.text = $"Wave {_waveCount}";

        if (!_rolling) return;

        _globalTimer += Time.fixedDeltaTime;
        _callEventTimer += Time.fixedDeltaTime;

        GlobalTimerSlider.Value = _globalTimer / GlobalTimerLimit;

        // don't ask why this stupid line is here, just don't...
        GlobalTimerImage.fillMethod = Image.FillMethod.Horizontal;

        if (_callEventTimer >= CallEventLimitTimer)
        {
            EventManager.Instance().StartEventInRndGrave();
            _callEventTimer = 0f;
        }

        if (_globalTimer >= GlobalTimerLimit)
        {
            _globalTimer = 0f;
            _rolling = false;
            Transition.SetActive(true);

            _isNightTime = !_isNightTime;
            
            StartCoroutine(FreeRolling());
        }
    }

    private IEnumerator FreeRolling()
    {
        yield return new WaitForSeconds(10f);

        _isNightTime = !_isNightTime;
        _waveCount++;

        Transition.SetActive(false);
        _rolling = true;
    }
}
