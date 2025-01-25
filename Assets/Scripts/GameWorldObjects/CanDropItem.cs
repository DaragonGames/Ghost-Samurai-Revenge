using UnityEngine;

public class CanDropItem : MonoBehaviour
{
    public GameObject itemPrefab;

    void Start()
    {
        GetComponent<Damageable>().DeathEvent += Die;
    }

    void Die()
    {
        if (Random.value <= GameManager.GetLuck() * 0.025f + 0.10f)
        {            
            Instantiate(itemPrefab, transform.position, Quaternion.identity);            
        }
    }

}
