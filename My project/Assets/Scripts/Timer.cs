using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int currentMin = 0;
    private float currentSec = 0f;
    private bool isRunning = true;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentSec += Time.deltaTime;
        }

        if (currentSec > 59.99f)
        {
            currentSec = 0f;
            currentMin += 1;
            FeralityManager.instance.AdjustFeralityMultiplier(0.75f);
        }

        text.text = $"{currentMin.ToString("0")}:{currentSec.ToString("00")}";
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
