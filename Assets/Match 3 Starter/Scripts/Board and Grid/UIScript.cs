using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public Text TurnsLeft;
    public Text Score;
    public Text Timer;
    public Text EndText;
    public Text Difficulty;
    public Text PointsNeeded;
    public Button RestartButton;

    private float minutes;
    private float seconds;

    private bool GameOver;

    public Slider PointSlider;
    private void Start()
    {
        if(BoardManager.difficulty == 1)
        {
            PointSlider.maxValue = 500;
        }
        if (BoardManager.difficulty == 2)
        {
            PointSlider.maxValue = 600;
        }
        if (BoardManager.difficulty == 3)
        {
            PointSlider.maxValue = 700;
        }
        PointSlider.minValue = 0;
        minutes = 6;
        seconds = 0;
        EndText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        GameOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        TurnsLeft.text = "Turns left: " + BoardManager.Instance.turnsLeft.ToString();
        Score.text = "Score: " + BoardManager.Instance.score.ToString();
        Timer.text = "Time: " + minutes.ToString() + ":" + seconds.ToString("#00");
        Difficulty.text = "Difficulty: " + BoardManager.difficulty.ToString();
        PointsNeeded.text = PointSlider.maxValue.ToString();

        PointSlider.value = BoardManager.Instance.score;

        if (seconds <= 0)
        {
            minutes -= 1;
            seconds = 60;
        }
        if (GameOver == false)
        {
            seconds -= Time.deltaTime;
        }
        if(minutes <= 0 && seconds <= 0)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You're out of time! Your final score is " + BoardManager.Instance.score.ToString();
        }
        if(BoardManager.Instance.turnsLeft <= 0)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You're out of turns! Your final score is " + BoardManager.Instance.score.ToString();
        }
        if(PointSlider.value >= PointSlider.maxValue)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You collected enough items! You can now continue!";
        }
        
    }

    public void GameOverFunc()
    {
        Score.gameObject.SetActive(false);
        TurnsLeft.gameObject.SetActive(false);
        BoardManager.Instance.gameObject.SetActive(false);
        EndText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        BoardManager.difficulty = Random.Range(1, 4);
        //BoardManager.Instance.turnsLeft = BoardManager.difficulty * 20;
        SceneManager.LoadScene("Game");
        //minutes = 2;
        //seconds = 0;
        //EndText.gameObject.SetActive(false);
        //RestartButton.gameObject.SetActive(false);
        //Score.gameObject.SetActive(true);
        //TurnsLeft.gameObject.SetActive(true);
        //GameOver = false;

        //BoardManager.Instance.gameObject.SetActive(true);
        //BoardManager.Instance.score = 0;
        //BoardManager.Instance.turnsLeft = 20;
    }
}
