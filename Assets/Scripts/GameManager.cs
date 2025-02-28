using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState {Title, Reading, InGame, Boss, GameOver };
    public GameState gameState;
    public int currentRoomID = -1;
    public GameData gameData;
    public int stunframes = 0;

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

    public static event Action<int> CollectItem;
    public static void OnCollectItemEvent(int id)
    {
        Instance.gameData.collectItem(id);
        CollectItem?.Invoke(id);
    }

    public static event Action<bool> GameOver;
    public static void OnGameOverEvent(bool victory)
    {
        Instance.gameState = GameState.GameOver;
        GameOver?.Invoke(victory);
    }

    public void LeaveGrove()
    {
        gameData.progression++;
        gameData.ghostWrath +=10;
        gameState = GameState.Reading;
        Item.uncollectedItems = new int[8];
        SceneManager.LoadScene("Reading");
    }

    public void EnterGrove()
    {
        gameState = GameState.InGame;
        SceneManager.LoadScene("Game");                
    }

    public static event Action SelectTestimony;

    public void OnSelectTestimony(int id)
    {
        TestimonyHandler.SelectTestimonies(id);
        gameData.collectedSheets--;
        if (gameData.collectedSheets > 0)
        {
            SelectTestimony?.Invoke();
        }    
        else
        {
            EnterGrove();
        }    
    }

    public void CollectSheet() 
    {
        gameData.collectedSheets++;
    }

    public static int GetLuck()
    {
        return GameManager.Instance.gameData.LuckUpgradesCollected;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Title;
            SceneManager.LoadScene("Title");    
            Reset();
        }
        if (stunframes > 0)
        {
            stunframes--;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Reset()
    {
        gameData = new GameData();
        Room.exploredRooms = 0;
        TestimonyHandler.ResetTestimonies();
    }

    
}
