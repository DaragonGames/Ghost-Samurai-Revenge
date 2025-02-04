using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType type = EnemyType.all;
    public enum EnemyType {common, ranged, though, special, all};
    public float spawnChance;
    public Spawnables spawnables;

    // Start is called before the first frame update
    void Awake()
    {
        if (spawnChance < Random.value)
        {
            Destroy(gameObject);
            return;
        }
        List<GameObject> options;
        GameObject prefab;
        switch (type)
        {
            case EnemyType.common:
                options = spawnables.commonEnemies;                
                break;
            case EnemyType.ranged:
                options = spawnables.rangedEnemies;
                break;
            case EnemyType.though:
                options = spawnables.thoughEnemies;
                break;
            case EnemyType.special:
                options = spawnables.specialEnemies;
                break;
            case EnemyType.all:
                options = spawnables.allEnemies;
                break;
            default:
                options = spawnables.allEnemies;  
                break;
        }
        prefab = options[Random.Range(0, options.Count)];
        Instantiate(prefab,transform.position, Quaternion.identity, transform.parent.transform);
        Destroy(gameObject);
    }

}
