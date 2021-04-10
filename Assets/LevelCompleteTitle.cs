using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject game = GameObject.Find("Game");
        YellowFellowGame yellowFellowGame = game.GetComponent<YellowFellowGame>();
        int level = yellowFellowGame.level;
        Debug.Log(level);
        GetComponent<Text>().text = "Level "+level+" complete!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
