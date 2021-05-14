using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFellow : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x + 0.008f, position.y, position.z);
    }
    
}

