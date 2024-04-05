using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayedScoreTxt;
    [SerializeField] TextMeshProUGUI gameOverScoreTxt;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI timePlayed;
    [SerializeField] GameObject gameOverPannel;

    int score;
    float elapsedTime;
    int minutes;
    int seconds;

    private void OnEnable()
    {
        DropSpawner.UpdateScore += DropSpawner_UpdateScore;
        MoveWaterV1.GameOverEvent += SetGameOverScreen;
    }

    private void OnDisable()
    {
        DropSpawner.UpdateScore -= DropSpawner_UpdateScore;
        MoveWaterV1.GameOverEvent -= SetGameOverScreen;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //gameOverPannel = GameObject.FindGameObjectWithTag("Game Over");
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    private void DropSpawner_UpdateScore(int num)
    {
        if(num > 0)
        {
            score+=num;
        }

        if(num < 0)
        {
            score -= num;
        }

        displayedScoreTxt.text = $"Score: {score}"; 

        CheckHighScore();
    }

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void SetGameOverScreen()
    {
        gameOverPannel.SetActive(true);
        Time.timeScale = 0f;
        gameOverScoreTxt.text = $"Score: {score}";
        highScore.text = $"High score: {PlayerPrefs.GetInt("HighScore", 0)}";

        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);

        timePlayed.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

}
