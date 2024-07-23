using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainStats : MonoBehaviour
{
    public RectTransform healthMask;
    public RectTransform amuntionMask;

    // Update is called once per frame
    void Update()
    {
        Player player = GameManager.Instance.player.GetComponent<Player>();
        float hpPercentage = player.GetHealthPercentage();
        healthMask.sizeDelta = new Vector2 (hpPercentage*200, 40);
        amuntionMask.sizeDelta = new Vector2 (100, 80*player.GetAmmunition());        
    }
}
