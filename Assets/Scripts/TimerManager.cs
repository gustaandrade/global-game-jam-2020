using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }
}
