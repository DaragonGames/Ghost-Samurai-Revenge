using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState {Title, Reading, InGame, GameOver };
    public GameState gameState;
    public GameObject player;
    public int currentRoomID = -1;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static event Action<Vector3> EnterNewRoom;
    public static void OnRoomEnterEvent(Vector3 position)
    {
        EnterNewRoom?.Invoke( position);
    }

    
}
