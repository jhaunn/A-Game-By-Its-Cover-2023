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
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private GameObject gameOverParticles;
    private bool isGameOver = false;

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

    public bool GetIsGameOver()
    {
        return isGameOver;
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
        if (!isGameOver)
        {
            menu.SetActive(false);
            tutorial.SetActive(false);

            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            GameObject.FindObjectOfType<Timer>().StopTimer();

            SoundManager.instance.GetComponent<AudioSource>().clip = gameOverMusic;
            SoundManager.instance.GetComponent<AudioSource>().Play();
            Instantiate(gameOverParticles, gameOverParticles.transform.position, gameOverParticles.transform.rotation);
            gameOverMenu.SetActive(true);

            isGameOver = true;
        }
        
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
