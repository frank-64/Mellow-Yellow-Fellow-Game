using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Fellow player;

    [SerializeField]
    Material scaredMaterial;
    Material normalMaterial;

    bool hiding = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = PickRandomPosition();
        normalMaterial = GetComponent<Renderer>().material;
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

            if (CanSeePlayer())
            {
                agent.destination = player.transform.position;
            }
            else
            { 
                if (agent.remainingDistance < 0.5f)
                {
                    agent.destination = PickRandomPosition();
                    hiding = false;
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

}
