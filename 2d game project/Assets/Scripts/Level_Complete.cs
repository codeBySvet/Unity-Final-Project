using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Complete : MonoBehaviour
{

    private AudioSource finishSound;

    private bool levelCompleted = false;

    private void Start()
    {
        //Getting component with audio
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            //Play the level complete audio
            finishSound.Play();
            levelCompleted = true;

            //Load in the next level
            Invoke("CompleteLevel", 3f);
        }
    }

    private void CompleteLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
