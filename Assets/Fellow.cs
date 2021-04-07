using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fellow : MonoBehaviour
{

    [SerializeField]
    float speed = 0.05f;

    int score = 0;
    int pelletsEaten = 0;
    [SerializeField]
    int pointsPerPellet = 100;

    [SerializeField]
    float powerupDuration = 10.0f; //How long the powerups should last

    float powerupTime = 0.0f; // How long i left on the current powerup

    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("The score is " + score);
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
            Debug.Log("You died!");
            gameObject.SetActive(false);
        }
    }

    public int PelletsEaten()
    {
        return pelletsEaten;
    }

}
