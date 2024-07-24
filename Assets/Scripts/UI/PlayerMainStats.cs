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
        healthMask.sizeDelta = new Vector2 (hpPercentage*200, 16);
        amuntionMask.sizeDelta = new Vector2 (60*player.GetAmmunition(), 64); 

        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            gameObject.SetActive(false);
        }       
    }
}
