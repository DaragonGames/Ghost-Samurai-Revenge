using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RoomInitializer : MonoBehaviour
{
    public RoomsCollection dataset;
    public Transform enemies;

    public void fillRoom(Vector3 playerPosition, int ID, bool leftOpen, bool rightOpen,bool upOpen,bool downOpen)
    {
        // try without parameters
        GameObject layoutPrefab = FigureOutLayout(leftOpen, rightOpen, upOpen, downOpen);
        Transform roomLayout = Instantiate(layoutPrefab, transform).transform;
        FlipRoom(playerPosition, roomLayout);
        

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

    public void FlipRoom(Vector3 playerPosition, Transform roomLayout)
    {
        float xdif = transform.position.x - playerPosition.x;
        float ydif = transform.position.y - playerPosition.y;
        bool flipX = false, flipY = false;

        if (Mathf.Abs(xdif) > Mathf.Abs(ydif))
        {
            flipX = xdif>0;
        }
        else
        {
            flipY = ydif<0;
        }
        for (int i = 0;i<roomLayout.childCount;i++)
        {
            Vector3 pos = roomLayout.GetChild(i).localPosition;
            roomLayout.GetChild(i).localPosition = new Vector3(pos.x *(flipX?-1:1),pos.y * (flipY?-1:1),pos.z);
        }
    }

    public GameObject FigureOutLayout(bool leftOpen, bool rightOpen,bool upOpen,bool downOpen)
    {
        List<GameObject> available = new List<GameObject>(dataset.open);
        int openings = (leftOpen ? 1 : 0) + (rightOpen ? 1 : 0) + (upOpen ? 1 : 0) + (downOpen ? 1 : 0);
        
        if (openings == 1)
        {
            if (leftOpen || rightOpen)
            {
                available.AddRange(dataset.deadEndSide);
            }
            else
            {
                available.AddRange(dataset.deadEndTop);
            }            
        }
        if (openings <= 2)
        {
            if (leftOpen || rightOpen)
            {
                if (leftOpen && rightOpen)
                {
                    available.AddRange(dataset.passageSide);
                }
                else
                {
                    available.AddRange(dataset.passageCorner);
                }
            }
            else
            {
                available.AddRange(dataset.passageTop);
            }           
        }
        float totalWeight = 0;
        foreach (GameObject item in available)
        {
            totalWeight += item.GetComponent<RoomProperties>().GetAdjustedWeight();
        }
        float random = totalWeight * Random.value;
        float counter = 0;
        foreach (GameObject item in available)
        {
            counter += item.GetComponent<RoomProperties>().GetAdjustedWeight();
            if (counter > random)
            {
                return item;
            }
        }     
        return available[Random.Range(0,available.Count)];
    }
}
