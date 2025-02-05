using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public GameObject steam;

    public void ClickTryAgain()
    {
        GameManager.Instance.gameState = GameManager.GameState.Title;        
        SceneManager.LoadScene("Title");    
        GameManager.Instance.Reset();
        
    }

    public void ToSteam()
    {
        steam.SetActive(true);
    }
}
