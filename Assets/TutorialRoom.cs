using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom : MonoBehaviour
{
    public int id;
    public int[] Openings;
    void Awake()
    {
        Room room = GetComponent<Room>();
        room.SetAsStart();
        foreach (int i in Openings) {room.createOpening(i);}
        room.SetID(id);

        Enemy[] all = transform.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in all) 
        {
            enemy.SetRoomID(id);
        }
    }
}
