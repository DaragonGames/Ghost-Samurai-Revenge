public class PlayerStats
{
    public float movementSpeed;
    public float attackSpeed;
    public float attackDamage;
    public float maxHealth;
    public float defense;
    public float maxEnergy = 1;
    public float EnergyReloadRate = 0.15f;

    public PlayerStats() 
    {
        SetStats();
    }

    private float defaultSpeed = 8;
    private float defaultAttackSpeed = 0.5f;
    private float defaultAttackDamage = 1;
    private float defaultHealth = 100;
    private float defaultDefense = 0;
    private float upgradeSpeed = 0.1f;
    private float upgradeAttackSpeed = .05f;
    private float upgradeAttackDamage = 0.2f;
    private float upgradeHealth = 20;
    private float upgradeDefense = 2;


    public void SetStats()
    {
        GameData gameData = GameManager.Instance.gameData;
        movementSpeed = defaultSpeed * (1 + gameData.MovementSpeedUpgradesCollected * upgradeSpeed);
        attackSpeed = defaultAttackSpeed - gameData.AttackSpeedUpgradesCollected * upgradeAttackSpeed;
        attackDamage = defaultAttackDamage + gameData.DamageUpgradesCollected * upgradeAttackDamage;
        maxHealth = defaultHealth + gameData.HealthUpgradesCollected * upgradeHealth;
        defense = defaultDefense + gameData.DefenseUpgradesCollected * upgradeDefense;
    }


}
