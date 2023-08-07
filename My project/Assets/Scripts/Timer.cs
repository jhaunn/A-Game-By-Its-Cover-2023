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

    [SerializeField] private TextMeshPro highScore;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        highScore.text = $"High Score\n{PlayerPrefs.GetInt("highScoreMin",0)}:{PlayerPrefs.GetInt("highScoreSec",0).ToString("00")}";
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

        text.text = $"{currentMin}:{currentSec.ToString("00")}";
    }

    public void StopTimer()
    {
        isRunning = false;

        PlayerPrefs.SetInt("highScoreMin", currentMin);
        PlayerPrefs.SetInt("highScoreSec", Mathf.FloorToInt(currentSec));
    }
}
