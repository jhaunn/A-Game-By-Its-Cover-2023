using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tutorial;

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

    public void TutorialResume()
    {
        tutorial.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
