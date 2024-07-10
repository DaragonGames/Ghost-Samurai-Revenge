using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObstacle : MonoBehaviour
{
    public Sprite[] sprites;
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
        Destroy(gameObject);
    }

}
