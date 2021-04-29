using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    private Transform location;
    private Vector3 startPos;
    NavMeshAgent agent;
    
    [SerializeField]
    Fellow player;
    
    [SerializeField]
    Material scaredMaterial;
    Material normalMaterial;
    [SerializeField] 
    Material respawnMaterial;

    bool hiding = false;
    bool respawning = false;

    float targetSpeed = 3.5f;

    // Start is called before the first frame update
    public void Start()
    {
        location = this.transform;
        startPos = location.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = PickRandomPosition();
        normalMaterial = GetComponent<Renderer>().material;
    }
    
    public void Reset(Boolean gameOver)
    {
        hiding = false;
        GetComponent<Renderer>().material = normalMaterial;
        agent.transform.position = startPos;
        agent.destination = PickRandomPosition();
        if (gameOver)
        {
            agent.speed = targetSpeed;
        }
        else
        {
            targetSpeed = targetSpeed + 0.25f;
            agent.speed = targetSpeed;
        }
    }

    Vector3 PickRandomPosition()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 8.0f;
        destination.x += randomDirection.x;
        destination.y += randomDirection.y;

        NavMeshHit navHit;
        NavMesh.SamplePosition(destination, out navHit, 8.0f, NavMesh.AllAreas);

        return navHit.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.PowerupActive())
        {
            //Debug.Log("Hiding from Player!");
            if (!hiding || agent.remainingDistance < 0.5)
            {
                hiding = true;
                agent.destination = PickHidingPlace();
                GetComponent<Renderer>().material = scaredMaterial;
            }
        }
        else
        {
            //Debug.Log("Chasing Player!");
            if (hiding)
            {
                GetComponent<Renderer>().material = normalMaterial;
                hiding = false;
            }

            if (CanSeePlayer() && !respawning)
            {
                agent.destination = player.transform.position;
            }
            else
            { 
                if (agent.remainingDistance < 0.5f)
                {
                    agent.destination = PickRandomPosition();
                    hiding = false;
                    respawning = false;
                    GetComponent<Renderer>().material = normalMaterial;
                }
            }
        }
    }

    bool CanSeePlayer()
    {
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit info;

        if (Physics.Raycast(rayPos, rayDir, out info))
        {
            if (info.transform.CompareTag("Fellow"))
            {
                return true;
            }
        }
        return false;
    }

    Vector3 PickHidingPlace()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        NavMeshHit navHit;

        NavMesh.SamplePosition(transform.position - (directionToPlayer * 8.0f), out navHit, 8.0f, NavMesh.AllAreas);

        return navHit.position;
    }

    public void KilledByFellow()
    {
        hiding = false;
        respawning = true;
        agent.destination = startPos;
        GetComponent<Renderer>().material = respawnMaterial;
    }

    public bool Respawning => respawning;
}
