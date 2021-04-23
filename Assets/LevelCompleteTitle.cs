using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteTitle : MonoBehaviour
{
    [SerializeField]
    YellowFellowGame game;
    // Start is called before the first frame update
    void Start()
    {
        int level = game.level;
        GetComponent<Text>().text = "Level "+level+" complete!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
