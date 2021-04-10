using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowFellowGame : MonoBehaviour
{
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

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartMainMenu();
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameMode)
        {
            case GameMode.MainMenu:     UpdateMainMenu(); break;
            case GameMode.HighScores:   UpdateHighScores(); break;
            case GameMode.InGame:       UpdateMainGame(); break;
            case GameMode.LevelWon:     LevelSetup(); break;
        }

        if (playerObject.PelletsEaten() == 5)
        {
            if (!won)
            {
                Debug.Log("Won");
                LevelComplete();
                won = true;
            }
        }
    }

    void UpdateMainGame()
    {
        
    }
    
    void UpdateMainMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
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


    void LevelSetup()
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
