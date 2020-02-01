using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public float GlobalTimer;
    public RadialSlider GlobalTimerSlider;
    public Image GlobalTimerImage;

    public float CallEventTimer;
    public float CallEventLimitTimer;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void FixedUpdate()
    {
        GlobalTimer += Time.fixedDeltaTime;
        CallEventTimer += Time.fixedDeltaTime;
        GlobalTimerSlider.Value = GlobalTimer;
        GlobalTimerImage.fillMethod = Image.FillMethod.Horizontal;
        //GlobalTimerImage.fillAmount = GlobalTimer / 10;

        if (CallEventTimer >= CallEventLimitTimer)
        {
            EventManager.Instance().StartEventInRndGrave();
            CallEventTimer = 0f;
        }
    }
}
