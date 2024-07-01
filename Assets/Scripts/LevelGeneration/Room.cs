
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public Transform enemies;
    public Spawnables spawnables;

    private int ID;
    private bool leftOpen, rightOpen, upOpen, downOpen, isCleared, isStart, isEnd, isEntered;

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

    public void SetAsStart() {
        isStart = true;
    }

    public void SetAsEnd() {
        isEnd = true;
        upOpen = true;
    }

    void Start()
    {
        Vector3 position = transform.position-Vector3.forward;

        if (isEnd)
        {
            Instantiate(spawnables.torii, position+Vector3.up*3.5f, Quaternion.identity, transform);
            return;
        }
        if (isStart)
        {
            return;
        }
        
        GameObject obj = Instantiate(spawnables.enemies[0], position, Quaternion.identity, enemies);
        obj.GetComponent<Enemy>().SetRoomID(ID);
    }

    void Update()
    {
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
        if (enemies.childCount == 0)
        {
            isCleared = true;
        }      
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
        {
            GameManager.OnRoomEnterEvent(transform.position);            
            StartCoroutine(DelayRoomAction());
        }        
    }

    IEnumerator DelayRoomAction()
    {
        yield return new WaitForSeconds(1f); 
        isEntered = true;
        GameManager.Instance.currentRoomID = ID;
    }

}
