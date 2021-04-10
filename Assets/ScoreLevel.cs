using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject fellowObject = GameObject.Find("Fellow");
            Fellow fellow = fellowObject.GetComponent<Fellow>();
            GetComponent<Text>().text = "Score: " + fellow.score;
        }
        catch (Exception e)
        {
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
