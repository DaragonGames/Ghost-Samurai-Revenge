using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int ID;
    private bool leftOpen, rightOpen, upOpen, downOpen;

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

    public void Update()
    {
        transform.GetChild(0).gameObject.SetActive(leftOpen);
        transform.GetChild(1).gameObject.SetActive(rightOpen);
        transform.GetChild(2).gameObject.SetActive(upOpen);
        transform.GetChild(3).gameObject.SetActive(downOpen);
    }

}
