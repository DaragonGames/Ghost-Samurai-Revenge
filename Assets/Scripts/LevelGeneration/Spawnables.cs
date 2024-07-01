using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Spawnables", menuName = "ScriptableObjects/Spawnables", order = 1)]
public class Spawnables : ScriptableObject
{
    public List<GameObject> enemies;
    public List<GameObject> obstacles;
    public GameObject torii;
}
