using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    [Header("Game Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tutorial;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Transform feralityNpc;

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

    private void Start()
    {
        tutorial.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }

    public void GameResume()
    {
        menu.SetActive(false);
    }

    public void Tutorial()
    {
        tutorial.SetActive(true);
        menu.SetActive(false);
    }

    public void TutorialResume()
    {
        tutorial.SetActive(false);
    }

    public void GameOver()
    {
        menu.SetActive(false);
        tutorial.SetActive(false);

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }

        GameObject.FindObjectOfType<CinemachineVirtualCamera>().Follow = feralityNpc;
        gameOver.SetActive(true);
    }

    public void GameOverRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
