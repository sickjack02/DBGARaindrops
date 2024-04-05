using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {

    }

    public void PlayGame()
    {
        StartCoroutine(LoadGame());
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Click()
    {
        audioManager.PlaySFX(audioManager.Buttons);
    }

    IEnumerator LoadGame()
    {
        audioManager.PlayGameMusic();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

}
