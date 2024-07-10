using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Spawnables", menuName = "ScriptableObjects/Spawnables", order = 1)]
public class Spawnables : ScriptableObject
{   
    public List<GameObject> commonEnemies;
    public List<GameObject> rangedEnemies;
    public List<GameObject> thoughEnemies;
    public List<GameObject> specialEnemies;
    public List<GameObject> allEnemies;
    public GameObject torii;
    public List<GameObject> roomLayoutsLeft;
    public List<GameObject> roomLayoutsTop;
}
