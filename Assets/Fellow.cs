using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fellow : MonoBehaviour
{
    public AudioSource pelletCollectSound;
    
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
        gameObject.SetActive(true);
        gameObject.transform.position = startPos;
        powerupTime = 0;
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
            powerupDuration = powerupDuration * 0.8f;
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
            pelletCollectSound.Play();
            pelletsEaten++;
            score += pointsPerPellet;
        }


        if (other.gameObject.CompareTag("Powerup"))
        {
            pelletCollectSound.Play();
            powerupTime = powerupDuration;
        }

    }

    public bool PowerupActive()
    {
        return powerupTime > 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ghost ghostObject = collision.gameObject.GetComponent<Ghost>();
        
        if (collision.gameObject.CompareTag("Ghost") && PowerupActive())
        {
            if (!ghostObject.Respawning)
            {
                collision.gameObject.GetComponent<Ghost>().KilledByFellow();
                powerupTime = 0.0f;
            }
        } else if (collision.gameObject.CompareTag("Ghost"))
        {
            if (!ghostObject.Respawning)
            {
                if (lives == 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
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
    }

    public int PelletsEaten()
    {
        return pelletsEaten;
    }

    public void SetPelletsEaten(int pellets)
    {
        pelletsEaten = pellets;
    }

    public int GetScore()
    {
        return score;
    }
}
