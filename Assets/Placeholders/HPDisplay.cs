using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Player player = GameManager.Instance.player.GetComponent<Player>();
        PlayerStats stats = player.GetStats();
        TMP_Text[] all = GetComponentsInChildren<TMP_Text>();
        all[0].text = "HP: " + player.GetHealthValue() + "/" + stats.maxHealth;
        all[1].text = "Damage: "  + stats.sliceDamage;
        all[2].text = "Speed: "  + stats.movementSpeed;
        all[3].text = "AttackSpeed: "  + stats.attackSpeed;
        all[4].text = "Defense: "  + stats.defensePercentage;
        all[5].text = "Luck: "  + GameManager.Instance.gameData.LuckUpgradesCollected;
    }
}
