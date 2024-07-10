using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string type;
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
            case "common":
                options = spawnables.commonEnemies;                
                break;
            case "ranged":
                options = spawnables.rangedEnemies;
                break;
            case "though":
                options = spawnables.thoughEnemies;
                break;
            case "special":
                options = spawnables.specialEnemies;
                break;
            case "all":
                options = spawnables.specialEnemies;
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
