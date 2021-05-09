using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneGhost : MonoBehaviour
{
    private Boolean stop = false;
    
    [SerializeField]
    AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x+5, position.y, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!stop)
        // {
        //     Vector3 position = gameObject.transform.position;
        //     gameObject.transform.position = new Vector3(position.x+0.03f, position.y, position.z);
        // }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fellow"))
        {
            deathSound.Play();
            collision.gameObject.SetActive(false);
            stop = true;
        }
    }
}
