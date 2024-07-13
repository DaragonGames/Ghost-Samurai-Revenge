using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableStorage : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject itemPrefab;
    private int toughness;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        toughness = Random.Range(0, 4);
    }

    public void GetAttacked()
    {
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
