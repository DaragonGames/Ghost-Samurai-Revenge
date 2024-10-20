using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatDebuger : MonoBehaviour
{
    public float movementSpeed = 6;
    public float attackSpeed = 1;
    public float arrowDamage = 20;
    public float sliceDamage = 20;
    public float maxHealth = 100;
    public float defense = 0;

    void Update()
    {
        Player.Instance.GetStats().OverWriteByDebugger(movementSpeed,attackSpeed,arrowDamage,sliceDamage,maxHealth, defense);
    }
}
