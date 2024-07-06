using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState {Title, Reading, InGame, GameOver };
    public GameState gameState;
    public GameObject player;
    public int currentRoomID = -1;
    private int collectedSheets = 100;
    public GameData gameData;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            TestimonyHandler.LoadTestimonies();
            DontDestroyOnLoad(gameObject);
            gameData = new GameData();
        }
        else
        {
            Destroy(gameObject);
        }        
    }
    public static event Action<Vector3> EnterNewRoom;
    public static void OnRoomEnterEvent(Vector3 position)
    {
        EnterNewRoom?.Invoke( position);
    }

    public void LeaveGrove()
    {
        gameState = GameState.Reading;
        SceneManager.LoadScene("Reading");
    }

    public void EnterGrove()
    {
        gameState = GameState.InGame;
        SceneManager.LoadScene("Game");
    }

    public void SelectTestimony(int id)
    {
        TestimonyHandler.SelectTestimonies(id);
        collectedSheets--;
        if (collectedSheets > 0)
        {
            SceneManager.LoadScene("Reading");
        }    
        else
        {
            EnterGrove();
        }    
    }

    
}
