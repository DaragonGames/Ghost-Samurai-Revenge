using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHPDisplay : MonoBehaviour
{
    public GameObject boss;


    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TMP_Text>().text = (int)boss.GetComponent<Ghost>().health + "";
    }
}
