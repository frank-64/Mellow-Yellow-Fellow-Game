using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLevel : MonoBehaviour
{
    [SerializeField] 
    Fellow player;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            GetComponent<Text>().text = "Score: " + player.score;
        }
        catch (Exception e)
        {
        } 
    }
}
