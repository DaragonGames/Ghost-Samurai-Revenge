using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TMP_Text>().text = "HP: " + GameManager.Instance.player.GetComponent<Player>().GetHealth();
    }
}
