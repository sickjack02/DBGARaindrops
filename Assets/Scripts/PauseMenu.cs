using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIspaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIspaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIspaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIspaused = false;
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadMenu());
    }

    public void Click()
    {
        audioManager.PlaySFX(audioManager.Buttons);
    }

    IEnumerator LoadMenu()
    {
        audioManager.PlayGameMusic();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
