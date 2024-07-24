using TMPro;
using UnityEngine;
public class PlayerSecondaryStats : MonoBehaviour
{
    public TMP_Text[] statTexts;

    void Start()
    {
        GameManager.CollectItem += SetValues; 
        SetValues(0);
    }

    void OnDestroy()
    {
        GameManager.CollectItem -= SetValues; 
    }

    void SetValues(int lazy)
    {
        GameData data = GameManager.Instance.gameData;

        statTexts[0].text = GetTally(data.MovementSpeedUpgradesCollected);
        statTexts[1].text = GetTally(data.DamageUpgradesCollected);
        statTexts[2].text = GetTally(data.AttackSpeedUpgradesCollected);
        statTexts[3].text = GetTally(data.DefenseUpgradesCollected);
        statTexts[4].text = GetTally(data.LuckUpgradesCollected);
        statTexts[5].text = GetTally(data.HealthUpgradesCollected);
    }

    public string GetTally(int amount)
    {
        string output = "";
        for (int i = 0; i < amount; i++)
        {
            output += "|";
        }
        return output;
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            gameObject.SetActive(false);
        }
    }

}
