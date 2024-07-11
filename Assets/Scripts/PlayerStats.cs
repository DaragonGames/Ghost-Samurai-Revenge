public class PlayerStats
{
    public float movementSpeed;
    public float attackSpeed;
    public float arrowDamage;
    public float sliceDamage;
    public float maxHealth;
    public float invisibleFramesDuration = 0.66f;
    public float knockbackStrength = 3;
    public float arrowSpeed = 5;
    public int arrowPiercing = 0;
    public int arrowFlightDuration = 3;
    public float trueDefense = 0;
    public float defensePercentage = 0;
    public float maxDamage;
    public float piercingDamage = 0;
    public float critChance = 0;
    public float critDamageMultiplier = 2;


    public PlayerStats() 
    {
        SetStats();
    }

    public void SetStats()
    {
        GameData gameData = GameManager.Instance.gameData;
        movementSpeed = 4 + gameData.MovementSpeedUpgradesCollected * 0.25f;
        attackSpeed = 0.6f - gameData.AttackSpeedUpgradesCollected * 0.05f;
        arrowDamage = 20 + gameData.DamageUpgradesCollected * 5;
        sliceDamage = 20 + gameData.DamageUpgradesCollected * 5;
        maxHealth = 100 + gameData.HealthUpgradesCollected * 10;
        maxDamage = maxHealth;
    }


}
