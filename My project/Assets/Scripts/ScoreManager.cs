using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI resourceText;

    private int resource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        resourceText.text = $"- {resource}";
    }

    public void AddResource(int value)
    {
        resource += value;
    }

    public void RemoveResource(int value)
    {
        resource -= value;
    }
}
