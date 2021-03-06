using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YellowFellowGame : MonoBehaviour
{
    public AudioSource menuSound;
    public AudioSource deathSound;
    public AudioSource levelCompleteSound;
    
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
    GameObject[] doublepoints;
    GameObject[] speedups;

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
        doublepoints = GameObject.FindGameObjectsWithTag("DoublePoints");
        speedups = GameObject.FindGameObjectsWithTag("SpeedUp");
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
        
        if ((playerObject.PelletsEaten() == pellets.Length*level) && !won)
        {
            levelCompleteSound.Play();
            won = true;
            ghost1.GetComponent<NavMeshAgent>().speed = 0;
            ghost2.GetComponent<NavMeshAgent>().speed = 0;
            ghost3.GetComponent<NavMeshAgent>().speed = 0;
            ghost4.GetComponent<NavMeshAgent>().speed = 0;
            LevelComplete();
        }
        else if (playerObject.lives == 0 && !died)
        {
            GlobalVariables.score = playerObject.score;
            SceneManager.LoadScene("Cutscene1");
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

    public void AddHighScore()
    {
        using (FileStream fs = new FileStream(highscoreFile,FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(playerObject.name + " " + playerObject.score);
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
            }else
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

        foreach (var speedup in speedups)
        {
            SpeedUp speedupObject = speedup.GetComponent<SpeedUp>();
            speedupObject.Start();
        }
        
        foreach (var doublepoint in doublepoints)
        {
            DoublePoints doublepointObject = doublepoint.GetComponent<DoublePoints>();
            doublepointObject.Start();
        }
    }

    void UpdateMainGame()
    {
        
    }
    
    void UpdateMainMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            menuSound.Play();
            SetupGame(true);
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuSound.Play();
            StartHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuSound.Play();
            StartMainMenu();
        }
    }


    void UpdateWinMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            menuSound.Play();
            SetupGame(false);
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuSound.Play();
            playerObject.SetPelletsEaten(0);
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

public static class GlobalVariables
{
    public static int score;
}
