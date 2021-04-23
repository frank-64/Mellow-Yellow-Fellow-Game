using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTeleporter : MonoBehaviour
{
    [SerializeField]
    LeftTeleporter leftTeleporter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Vector3 position = leftTeleporter.gameObject.transform.position;
        
        // This ensures that the gameObject won't stay in a teleporting
        // loop as it teleports slighlty to the right of the left teleporter
        // this means it's outside the hit box and doesn't teleport again
        position.x += 0.5f;
        other.gameObject.transform.position = position;
    }
}
