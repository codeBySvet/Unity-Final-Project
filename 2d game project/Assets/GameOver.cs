using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

}
