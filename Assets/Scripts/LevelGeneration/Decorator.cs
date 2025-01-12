using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : MonoBehaviour
{
    public Sprite[] sprites;

    public GameObject puddle;
    public GameObject glowbug;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <4; i++)
        {
            float x = -6 + Random.value*3 + i*3;
            for (int n = 0; n <4; n++)
            {
                float y = -3 + Random.value*1.5f + n*1.5f;
                if (Random.value > 0.4f)
                {
                    GameObject g = new GameObject("Deco");
                    SpriteRenderer sr = g.AddComponent<SpriteRenderer>();
                    sr.sortingLayerName = "Decoration";
                    sr.sprite = sprites[Random.Range(0, sprites.Length)];
                    g.transform.parent = transform;
                    g.transform.localPosition = new Vector3(x, y, 0);
                }
            }
        }
    }

}
