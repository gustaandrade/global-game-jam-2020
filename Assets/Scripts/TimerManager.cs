using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [SerializeField, ReadOnly] private float _globalTimer;
    public float GlobalTimerLimit;
    public RadialSlider GlobalTimerSlider;
    public Image GlobalTimerImage;

    [SerializeField, ReadOnly] private float _callEventTimer;
    public float CallEventLimitTimer;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void FixedUpdate()
    {
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
            Debug.Log("cycle time");
        }
    }
}
