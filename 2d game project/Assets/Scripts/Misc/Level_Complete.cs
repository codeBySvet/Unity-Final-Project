using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Complete : MonoBehaviour
{

    // private AudioSource finishSound;

    // private bool levelCompleted = false;

    // private void Start()
    // {
    //     //Getting component with audio
    //     finishSound = GetComponent<AudioSource>();
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.name == "Player" && !levelCompleted)
    //     {
    //         //Play the level complete audio
    //         finishSound.Play();
    //         levelCompleted = true;

    //         //Load in the next level
    //         Invoke("CompleteLevel", 3f);
    //     }
    // }

    // private void CompleteLevel()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }





    public int levelNumToLoad;
    public string levelNameToLoad;
    public bool useNumToLoad = false;

    void Start()
    {
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //If a colission is detected between the Player and the load level object, load a new level
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            LoadScene();
        }
    }


    void LoadScene()
    {   //Allowing the user to decide to load the level via the scene name or number
        if (useNumToLoad)
        {
            SceneManager.LoadScene(levelNumToLoad);
        }
        else
        {
            SceneManager.LoadScene(levelNameToLoad);
        }
    }

}
