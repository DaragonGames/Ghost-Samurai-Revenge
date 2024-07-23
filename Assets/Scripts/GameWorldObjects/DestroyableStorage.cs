using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableStorage : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject soundPrefab;
    private int toughness;

    // Start is called before the first frame update
    void Start()
    {
        toughness = Random.Range(0, 4);
    }

    public void GetAttacked()
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        toughness--;
        if (toughness > 0)
        {
            return;
        }
        if (Random.value <= GameManager.GetLuck() * 0.025f + 0.10f)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);            
        }
        Destroy(gameObject);
    }

}
