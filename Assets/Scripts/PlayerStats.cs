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
    public float defense = 0;
    public float maxDamage;
    public float piercingDamage = 0;
    public float critChance = 0;
    public float critDamageMultiplier = 2;
    public float maxAmmunition = 3;
    public float AmmunitionReloadRate = 0.33f;


    public PlayerStats() 
    {
        SetStats();
    }

    public void SetStats()
    {
        GameData gameData = GameManager.Instance.gameData;
        movementSpeed = 6 + gameData.MovementSpeedUpgradesCollected * 0.5f;
        attackSpeed = 0.375f - gameData.AttackSpeedUpgradesCollected * 0.1f;
        arrowDamage = 20 + gameData.DamageUpgradesCollected * 10;
        sliceDamage = 20 + gameData.DamageUpgradesCollected * 10;
        maxHealth = 100 + gameData.HealthUpgradesCollected * 20;
        defense = 0 + gameData.DefenseUpgradesCollected*5;
        maxDamage = maxHealth;
    }


}
