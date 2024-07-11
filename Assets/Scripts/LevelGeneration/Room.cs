
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown, bamboo;
    public Transform enemies, border;
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
        if (isStart)
        {
            isCleared = true;
        }
        if (isEnd)
        {
            Instantiate(spawnables.torii, transform.position+Vector3.up*3.5f, Quaternion.identity, transform);
        }  
        // Create Bamboo Walls
        for (int i = 0;i < 3; i++)
        {
            Instantiate(bamboo,transform.position + new Vector3(7.5f,4-i,-1), Quaternion.identity, border);
            Instantiate(bamboo,transform.position + new Vector3(7.5f,-4+i,-1), Quaternion.identity, border);
            Instantiate(bamboo,transform.position + new Vector3(-7.5f,4-i,-1), Quaternion.identity, border);
            Instantiate(bamboo,transform.position + new Vector3(-7.5f,-4+i,-1), Quaternion.identity, border);
            Instantiate(bamboo,transform.position + new Vector3(7.5f,1-i,-1), Quaternion.identity, doorRight.transform);
            Instantiate(bamboo,transform.position + new Vector3(-7.5f,1-i,-1), Quaternion.identity, doorLeft.transform);
        }
        for (int i = 0;i < 14; i++)
        {
            if (i== 6 || i== 7)
            {
                Instantiate(bamboo,transform.position + new Vector3(6.5f-i,4,-1), Quaternion.identity, doorUp.transform);
                Instantiate(bamboo,transform.position + new Vector3(6.5f-i,-4,-1), Quaternion.identity, doorDown.transform);
            }
            else
            {
                Instantiate(bamboo,transform.position + new Vector3(6.5f-i,4,-1), Quaternion.identity, border);
                Instantiate(bamboo,transform.position + new Vector3(6.5f-i,-4,-1), Quaternion.identity, border);
            }            
        }
        

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
        if (enemies.childCount == 0 && isEntered)
        {
            isCleared = true;
        }      
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
        {
            if (!isStart && !isEntered && !isEnd)
            {
                fillRoom(col.transform.position);
            }            
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

    public void fillRoom(Vector3 playerPosition)
    {
        float xdif = transform.position.x - playerPosition.x;
        float ydif = transform.position.y - playerPosition.y;
        bool flipX = false, flipY = false;
        GameObject prefab;

        if (Mathf.Abs(xdif) > Mathf.Abs(ydif))
        {
            prefab =  spawnables.roomLayoutsLeft[Random.Range(0,spawnables.roomLayoutsLeft.Count)];
            flipX = xdif>0;
        }
        else
        {
            prefab =  spawnables.roomLayoutsTop[Random.Range(0,spawnables.roomLayoutsTop.Count)];
            flipY = ydif<0;
        }
        GameObject roomLayout = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        for (int i = 0;i<roomLayout.transform.childCount;i++)
        {
            Vector3 pos = roomLayout.transform.GetChild(i).localPosition;
            roomLayout.transform.GetChild(i).localPosition = new Vector3(pos.x *(flipX?-1:1),pos.y * (flipY?-1:1),pos.z);
        }
        

        List<Transform> enemyList = new List<Transform>();
        for (int i =0; i < roomLayout.transform.childCount; i++)
        {            
            Transform check = roomLayout.transform.GetChild(i);
            if (check.tag == "Enemy")
            {
                enemyList.Add(check);
                check.GetComponent<Enemy>().SetRoomID(ID);
            }
        }
        foreach (Transform enemy in enemyList)
        {
            enemy.parent = enemies;
        }

    }

}
