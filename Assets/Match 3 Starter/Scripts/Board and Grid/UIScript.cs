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
    public Button CloseButton;

    public GameObject BackgroundAssets;

    private float minutes;
    private float seconds;

    private bool GameOver;
    

    public Slider PointSlider;
    public Slider TurnsLeftSlider;
    private void Awake() // depending on the difficulty set the pointslider (enemy health) max value
    {
        if (BoardManager.difficulty == 1)
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
    }
    private void Start()
    {
        // UI setup, turnleftslider (player health) is the games number of turns left
        TurnsLeftSlider.maxValue = BoardManager.Instance.turnsLeft;
        minutes = 6;
        seconds = 0;
        EndText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        CloseButton.gameObject.SetActive(false);
        GameOver = false;
        if(!Manager.InGame)
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        TurnsLeft.text = BoardManager.Instance.turnsLeft.ToString();
        Score.text = "Score: " + BoardManager.Instance.score.ToString();
        Timer.text = "Time: " + minutes.ToString() + ":" + seconds.ToString("#00");
        Difficulty.text = "Difficulty: " + BoardManager.difficulty.ToString();
        PointsNeeded.text = PointSlider.value.ToString();

        PointSlider.value = BoardManager.Instance.score;
        TurnsLeftSlider.value = BoardManager.Instance.turnsLeft;

        if (seconds <= 0)
        {
            minutes -= 1;
            seconds = 60;
        }
        if (GameOver == false && Manager.InGame == true)
        {
            seconds -= Time.deltaTime;
        }
        if(minutes <= 0 && seconds <= 0)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You're out of time! You have been defeated.";
        }
        if(BoardManager.Instance.turnsLeft <= 0)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You're out of life! You have been defeated.";
        }
        if (PointSlider.value <= 0)
        {
            GameOver = true;
            GameOverFunc();
            EndText.text = "You slain the enemy! You can now continue!";
        }

    }

    public void GameOverFunc() // helper function to make only important things visible when the game is over
    {
        Score.gameObject.SetActive(false);
        TurnsLeft.gameObject.SetActive(false);
        BoardManager.Instance.gameObject.SetActive(false);
        EndText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        CloseButton.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        BoardManager.difficulty = Random.Range(1, 4);
        SceneManager.LoadScene("Game");
    }

    public void OnClose()
    {
        BoardManager.Instance.gameObject.SetActive(false);
        gameObject.SetActive(false);
        BackgroundAssets.SetActive(false);
    }
}
