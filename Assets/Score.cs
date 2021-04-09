using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
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
            GameObject fellowObject = GameObject.Find("Fellow");
            Fellow fellow = fellowObject.GetComponent<Fellow>();
            GetComponent<Text>().text = "Score: " + fellow.score;
        }
        catch (Exception e)
        {
        }
    }
}
