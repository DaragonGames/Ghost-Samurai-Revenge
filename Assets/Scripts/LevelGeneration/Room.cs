
using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown, bambooTop, bambooSide, Tutorial;
    public Transform enemies, border;
    public Spawnables spawnables;

    private int ID;
    private bool leftOpen, rightOpen, upOpen, downOpen, isCleared, isStart, isEnd, isEntered, isBossRoom;

    public void createOpening(int indicator)
    {
        if (indicator == 1)
        {
            rightOpen = true;
        }
        if (indicator == -1)
        {
            leftOpen = true;
        }
        if (indicator > 1)
        {
            upOpen = true;
        }
        if (indicator < -1)
        {
            downOpen = true;
        }
    }

    public void SetID(int id) { ID = id; }
    public int GetID() { return ID; }
    public bool GetIsStart() { return isStart; }
    public bool GetIsEntered() {return isEntered;}
    public void SetBossRoom() { isBossRoom = true;}

    public void SetAsStart() {
        isStart = true;
    }

    public void SetAsEnd() {
        isEnd = true;
        upOpen = true;
    }

    void Start()
    {
        Tutorial.SetActive(isStart && GameManager.Instance.gameData.progression == 0);
        if (isStart)
        {
            isCleared = true;
            
        }
        if (isEnd)
        {
            if (GameManager.Instance.gameData.progression < 3)
            {
                Instantiate(spawnables.torii, transform.position+Vector3.up*3.25f, Quaternion.identity, transform);                
            }
            {
                upOpen = false;
            }            
        }  
        // Create Bamboo Walls
        for (int i = 0;i < 3; i++)
        {
            Instantiate(bambooSide,transform.position + new Vector3(7.5f,4-i,-1), Quaternion.identity, border);
            Instantiate(bambooSide,transform.position + new Vector3(7.5f,-4+i,-1), Quaternion.identity, border);
            Instantiate(bambooSide,transform.position + new Vector3(7.5f,1-i,-1), Quaternion.identity, doorRight.transform);
            Instantiate(bambooSide,transform.position + new Vector3(-7.5f,4-i,-1), Quaternion.identity, border);
            Instantiate(bambooSide,transform.position + new Vector3(-7.5f,-4+i,-1), Quaternion.identity, border);            
            Instantiate(bambooSide,transform.position + new Vector3(-7.5f,1-i,-1), Quaternion.identity, doorLeft.transform);
        }
        for (int i = 0;i < 14; i+=2)
        {
            if (i== 6)
            {
                Instantiate(bambooTop,transform.position + new Vector3(6f-i,4,-1), Quaternion.identity, doorUp.transform);
                Instantiate(bambooTop,transform.position + new Vector3(6f-i,-4,-1), Quaternion.identity, doorDown.transform);
            }
            else
            {
                Instantiate(bambooTop,transform.position + new Vector3(6f-i,4,-1), Quaternion.identity, border);
                Instantiate(bambooTop,transform.position + new Vector3(6f-i,-4,-1), Quaternion.identity, border);
            }            
        }
        

    }

    void Update()
    {
        if (enemies.childCount == 0 && isEntered)
        {
            if (!isCleared)
            {
                Player.Instance.RegainEnergy(0.3f);
            }
            isCleared = true;
        }   
        if (isBossRoom && ID == GameManager.Instance.currentRoomID)
        {
            doorLeft.SetActive(true);
            doorRight.SetActive(true);
            doorUp.SetActive(true);
            doorDown.SetActive(true);
            return;
        }
        if (isEntered && !isCleared)
        {
            doorLeft.SetActive(true);
            doorRight.SetActive(true);
            doorUp.SetActive(true);
            doorDown.SetActive(true);
        }
        else
        {
            doorLeft.SetActive(!leftOpen);
            doorRight.SetActive(!rightOpen);
            doorUp.SetActive(!upOpen);
            doorDown.SetActive(!downOpen);
        }   
    }

    public static int exploredRooms = 0;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
        {
            if (!isStart && !isEntered && !isEnd && !isBossRoom)
            {
                GetComponent<RoomInitializer>().fillRoom(
                col.transform.position, ID, leftOpen, rightOpen, upOpen, downOpen);
                exploredRooms++;
            }            
            GameManager.OnRoomEnterEvent(transform.position);            
            StartCoroutine(DelayRoomAction());
        }      
    }

    IEnumerator DelayRoomAction()
    {
        yield return new WaitForSeconds(0.75f); 
        isEntered = true;
        GameManager.Instance.currentRoomID = ID;
    }
    
}
