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
    Ghost ghost1;
    [SerializeField] 
    Ghost ghost2;
    [SerializeField] 
    Ghost ghost3;
    [SerializeField] 
    Ghost ghost4;
    
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
    GameObject[] powerups;

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
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
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
        
        if (playerObject.PelletsEaten() == pellets.Length*level)
        {
            if (!won)
            {
                won = true;
                ghost1.GetComponent<NavMeshAgent>().speed = 0;
                ghost2.GetComponent<NavMeshAgent>().speed = 0;
                ghost3.GetComponent<NavMeshAgent>().speed = 0;
                ghost4.GetComponent<NavMeshAgent>().speed = 0;
                LevelComplete();
            }
        }
        else if (playerObject.lives == 0 && !died)
        {
            playerObject.SetPelletsEaten(0);
            ghost1.GetComponent<NavMeshAgent>().speed = 0;
            ghost2.GetComponent<NavMeshAgent>().speed = 0;
            ghost3.GetComponent<NavMeshAgent>().speed = 0;
            ghost4.GetComponent<NavMeshAgent>().speed = 0;
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
            SetupGame(true);
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
            SetupGame(false);
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            playerObject.SetPelletsEaten(0);
            StartMainMenu();
        }
    }


    void SetupGame(Boolean reset)
    {
        // Reset the game if the user goes back to the main menu, else advance to the next level.
        if (reset)
        {
            died = false;
            won = false;
            playerObject.Reset(true);
            
            if (level < 3)
            {
                ghost1.Reset(true);
            }else if (level <5)
            {
                ghost1.Reset(true);
                ghost2.Reset(true);
                
                ghost2.gameObject.SetActive(false);
            }else if (level <7)
            {
                ghost1.Reset(true);
                ghost2.Reset(true);
                ghost3.Reset(true);
                
                ghost2.gameObject.SetActive(false);
                ghost3.gameObject.SetActive(false);
            }
            else
            {
                ghost1.Reset(true);
                ghost2.Reset(true);
                ghost3.Reset(true);
                ghost4.Reset(true);
                
                ghost2.gameObject.SetActive(false);
                ghost3.gameObject.SetActive(false);
                ghost4.gameObject.SetActive(false);
            }
            level = 1;
            
        }
        else
        {
            level++;
            won = false;
            died = false;
            playerObject.Reset(false);

            if (level == 3)
            {
                ghost2.gameObject.SetActive(true);
                ghost2.Start();
            }else if (level == 5)
            {
                ghost3.gameObject.SetActive(true);
                ghost3.Start();
            }else if (level == 7)
            {
                ghost4.gameObject.SetActive(true);
                ghost4.Start();
            }

            if (level < 3)
            {
                ghost1.Reset(false);
            }else if (level <5)
            {
                ghost1.Reset(false);
                ghost2.Reset(false);
            }else if (level <7)
            {
                ghost1.Reset(false);
                ghost2.Reset(false);
                ghost3.Reset(false);
            }
            else
            {
                ghost1.Reset(false);
                ghost2.Reset(false);
                ghost3.Reset(false);
                ghost4.Reset(false);
            }
        }
        
    
        foreach (var pellet in pellets)
        {
            Pellet pelletObject = pellet.GetComponent<Pellet>();
            pelletObject.Start();
        }

        foreach (var powerup in powerups)
        {
            Powerup powerupObject = powerup.GetComponent<Powerup>();
            powerupObject.Start();
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
