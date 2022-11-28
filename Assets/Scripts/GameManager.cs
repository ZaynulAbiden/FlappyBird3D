using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion
    public GameObject MenuPanel;
    public GameObject GameOverPanel;
    public GameObject inGameUI;
    public GameObject retryPanel;
    public FlappyBirdController player;  
    public Text scoreTxt;
    public Text highScoreTxt;
    public Text gemsTxt;
    public bool isGameRunning;
    public float gameplaySpeed=5;
    private float score = 0;
    public int Gems;
    public int HighScore;
    private void Start()
    {
        SetPlayerPrefs();
        MainMenu();
        SetGems(0);
    }
    void SetPlayerPrefs()
    {
        HighScore = PlayerPrefs.GetInt(nameof(HighScore));
        highScoreTxt.text = "Best "+HighScore;
    }
    private void Update()
    {
        if (isGameRunning)
        {
            score += Time.deltaTime;
            scoreTxt.text = "Score "+(int)score;
            gameplaySpeed += Time.deltaTime / 20;
        }
    }

    public void StartGame( )
    {
        ActivatePlayer(0);
        MenuPanel.SetActive(false);
        gameplaySpeed = 5;
    }
    public void MainMenu()
    {
        ResetPlayer();
        Camera.main.transform.position = new Vector3(20, 0, 10);
        MenuPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        isGameRunning = false;
        inGameUI.SetActive(false);
    }

    void ResetPlayer()
    {
        if (score > HighScore) {
            PlayerPrefs.SetInt(nameof(HighScore), HighScore);
            highScoreTxt.text = "Best " + HighScore;

        }
        score = 0;
        player.transform.position = new Vector3(10, 0, 10);
        player.collisionPoint = player.transform.position;
        player.rb.isKinematic = true;
    }
    public void ActivatePlayer(int gemUsed)
    {
        if (gemUsed > 0)
            player.transform.position = player.collisionPoint;

        isGameRunning = true;
        player.rb.isKinematic = false;
        retryPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        inGameUI.SetActive(true);
        Gems -= gemUsed;
        StartCoroutine(player.BecomeInvisible(2));
    
    }

    public void GameOver()
    { 
        isGameRunning = false;
        GameOverPanel.SetActive(true);
        inGameUI.SetActive(false);
    }

    public void SetGems(int gems)
    {
        Gems += gems;
        gemsTxt.text = Gems.ToString();
    }


}
