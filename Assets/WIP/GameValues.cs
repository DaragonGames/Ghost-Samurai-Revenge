using UnityEngine;

public static class GameValues
{
    // Combat - Knockback
    public static float knockbacktime = 0.2f;
    public static string[] kockbackBounceTags= { "wall", "Stone"};
    public static AnimationCurve kockbackAnimationCurve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(knockbacktime, 0.125f)); 
    public static float inputInfluenceOnKnockback = 0.75f;
    
    // Combat - Damage related
    public static float invincibleTime = 0.66f;
}
