using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText, rewardText;

    [SerializeField]
    private Button pauseButton, resumeButton;

    [SerializeField]
    private Transform pausePanel, gameoverPanel, winPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        EventsHandeler.OnUpdateScore += UpdateScore;
        EventsHandeler.OnGamePause += Pause;
        EventsHandeler.OnGameResume += Resume;
        EventsHandeler.OnGameWin += WinGame;
        EventsHandeler.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        EventsHandeler.OnUpdateScore -= UpdateScore;
        EventsHandeler.OnGamePause -= Pause;
        EventsHandeler.OnGameResume -= Resume;
        EventsHandeler.OnGameWin -= WinGame;
        EventsHandeler.OnGameOver -= GameOver;
    }

    private void Start()
    {
        pauseButton.onClick.AddListener(()=>EventsHandeler.GamePause());
        resumeButton.onClick.AddListener(()=>EventsHandeler.GameResume());
    }

    void UpdateScore()
    { 
        scoreText.text = ((int)GameManager.Instance.score).ToString();
    }

    void Pause()
    {
        pausePanel.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    void Resume() 
    {
        pausePanel.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    void WinGame() 
    {
        rewardText.text = "+ $ " + 3 * (int)GameManager.Instance.score;
        winPanel.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
        pauseButton.enabled = false;
        Time.timeScale = 0;
    }

    void GameOver() 
    {
        gameoverPanel.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
        pauseButton.enabled = false;

    }


}
