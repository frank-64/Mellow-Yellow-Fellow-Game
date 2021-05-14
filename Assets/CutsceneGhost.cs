using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneGhost : MonoBehaviour
{
    private Boolean stop = false;

    [SerializeField] private GameObject score;
    
    [SerializeField]
    AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        score.GetComponent<Text>().text = "Score: "+ GlobalVariables.score;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Vector3 position = gameObject.transform.position;
            gameObject.transform.position = new Vector3(position.x+0.012f, position.y, position.z);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fellow"))
        {
            deathSound.Play();
            collision.gameObject.SetActive(false);
            stop = true;
        }

        StartCoroutine(cutscene());
    }
    
        
    IEnumerator cutscene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SampleScene");
    }
}
