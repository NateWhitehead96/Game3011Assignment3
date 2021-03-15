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
    public Button RestartButton;

    private float minutes;
    private float seconds;

    private bool GameOver;
    private void Start()
    {
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

        if(seconds <= 0)
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
