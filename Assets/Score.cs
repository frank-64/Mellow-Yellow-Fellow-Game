using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
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
        // try catch used to ignore null pointer which is given once the Fellow is killed by ghost
        try
        {
            GetComponent<Text>().text = "Score \n" + player.score;
        }
        catch (Exception e)
        {
        }
    }
}
