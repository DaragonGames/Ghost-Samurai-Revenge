using UnityEngine;

public static class GameValues
{
    // Combat - Knockback
    public static float knockbacktime = 0.2f;
    public static string[] kockbackBounceTags= { "wall", "Stone"};
    public static AnimationCurve kockbackAnimationCurve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(knockbacktime, 0.125f)); 
    public static float inputInfluenceOnKnockback = 0.75f;
    
    // Combat - Damage related
    public static float invincibleTimePlayer = 0.66f;
    public static float invincibleTimeEnemy = 0.2f;

    // Game Juice
    public static float minDmgHitStop = 20; 
    public static float minDmgPercentageHitStop = 0.5f; 
    public static int stunlockFrames = 60;

}
