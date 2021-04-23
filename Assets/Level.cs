using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject game = GameObject.Find("Game");
        YellowFellowGame yellowFellowGame = game.GetComponent<YellowFellowGame>();
        int level = yellowFellowGame.level;
        GetComponent<Text>().text = "" + level;
    }
}
