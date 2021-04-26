using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fellow : MonoBehaviour
{
    [SerializeField]
    Vector3 ghostStartPos = new Vector3(7.56599f, 0.8f, 8.028f);
    private Vector3 startPos;
    
    [SerializeField]
    float speed = 0.05f;
    
    [SerializeField]
    int pointsPerPellet = 100;
    public int score = 0;
    int pelletsEaten = 0;

    [SerializeField]
    float powerupDuration = 10.0f; //How long the powerups should last
    float powerupTime = 0.0f; // How long left on the current powerup


    public string name = "Frankie";
    public int lives = 3;

    [SerializeField] 
    GameObject heart1;
    [SerializeField]
    GameObject heart2;
    [SerializeField]
    GameObject heart3;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    public void Reset(Boolean gameOver)
    {
        gameObject.transform.position = startPos;
        if (gameOver)
        {
            score = 0;
            lives = 3;
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else
        {
            
        }
    }

    void Update()
    {
        Rigidbody b = GetComponent<Rigidbody>();
        Vector3 velocity = b.velocity;

        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z = -speed;
        }
        b.velocity = velocity;


        powerupTime = Mathf.Max(0.0f, powerupTime - Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            pelletsEaten++;
            score += pointsPerPellet;
        }


        if (other.gameObject.CompareTag("Powerup"))
        {
            powerupTime = powerupDuration;
        }

    }

    public bool PowerupActive()
    {
        return powerupTime > 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost") && PowerupActive())
        {
            collision.gameObject.GetComponent<Ghost>().KilledByFellow();
            Debug.Log("You killed the ghost!");
            powerupTime = 0.0f;
        } else if (collision.gameObject.CompareTag("Ghost"))
        {
            if (lives == 0)
            {
                Debug.Log("You died!");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Life used!");
                lives--;
                gameObject.transform.position = startPos;

                GameObject ghostGameObject = GameObject.Find("Ghost");
                ghostGameObject.transform.position = ghostStartPos;

                try
                {
                    if (heart1.activeInHierarchy)
                    {
                        heart1.SetActive(false);
                    } else if (heart2.activeInHierarchy)
                    {
                        heart2.SetActive(false);
                    }else if (heart3.activeInHierarchy)
                    {
                        heart3.SetActive(false);
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
    }

    public int PelletsEaten()
    {
        return pelletsEaten;
    }

    public int GetScore()
    {
        return score;
    }
}
