using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class YellowFellowGame : MonoBehaviour
{
    [SerializeField]
    string highscoreFile = "scores.txt";
    
    [SerializeField]
    GameObject highScoreUI;

    [SerializeField]
    GameObject mainMenuUI;

    [SerializeField]
    GameObject gameUI;

    [SerializeField] 
    GameObject winUI;

    [SerializeField]
    Fellow playerObject;

    GameObject[] pellets;


    enum GameMode
    {
        InGame,
        MainMenu,
        HighScores,
        LevelWon
    }

    GameMode gameMode = GameMode.MainMenu;

    private Boolean won;
    
    private Boolean died;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartMainMenu();
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        Debug.Log(pellets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameMode)
        {
            case GameMode.MainMenu:     UpdateMainMenu(); break;
            case GameMode.HighScores:   UpdateHighScores(); break;
            case GameMode.InGame:       UpdateMainGame(); break;
            case GameMode.LevelWon:     UpdateWinMenu(); break;
        }
        

        if (playerObject.PelletsEaten() == pellets.Length)
        {
            if (!won)
            {
                GameObject ghost = GameObject.Find("Ghost");
                NavMeshAgent ghostAgent = ghost.GetComponent<NavMeshAgent>();
                ghostAgent.speed = 0;
                LevelComplete();
                won = true;
            }
        }
        else if (playerObject.lives == 0 && !died)
        {
            GameObject ghost = GameObject.Find("Ghost");
            NavMeshAgent ghostAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAgent.speed = 0;
            AddHighScore();
            StartMainMenu();
            died = true;
        }
    }

    void UpdateMainGame()
    {
        
    }
    
    void UpdateMainMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //SetupGame();
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            StartHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMainMenu();
        }
    }
    
    public void AddHighScore()
    {
        using (FileStream fs = new FileStream(highscoreFile,FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(playerObject.name + " " + playerObject.score);
        }
    }


    void UpdateWinMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMainMenu();
        }
    }


    // void SetupGame()
    // {
    //     GameObject ghostGameObject = GameObject.Find("Ghost");
    //     Ghost ghost = ghostGameObject.GetComponent<Ghost>();
    //     
    //     //ghost.Start();
    //     playerObject.Start();
    //
    //     foreach (var pellet in pellets)
    //     {
    //         Pellet pelletObject = pellet.GetComponent<Pellet>();
    //         pelletObject.Start();
    //     }
    // }
    
    void StartMainMenu()
    {
        gameMode                        = GameMode.MainMenu;
        mainMenuUI.gameObject.SetActive(true);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
    }


    void StartHighScores()
    {
        gameMode                = GameMode.HighScores;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
    }

    void StartGame()
    {
        gameMode                = GameMode.InGame;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        winUI.gameObject.SetActive(false);
    }

    void LevelComplete()
    {
        gameMode = GameMode.LevelWon;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(true);
    }
}
