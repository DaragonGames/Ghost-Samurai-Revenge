using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : MonoBehaviour
{
    public Sprite[] spritesWeeds;
    public Sprite[] spritesFlowers;

    public GameObject glowbug;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <4; i++)
        {            
            for (int n = 0; n <4; n++)
            {
                float x = Random.Range(-1.1f, 1.1f) + (i-1.5f)*3.1f;
                float y = Random.Range(-1.1f, 1.1f) + (n-1.5f)*1.5f;
                if (Random.value > 0.4f)
                {
                    Sprite sprite = spritesWeeds[Random.Range(0, spritesWeeds.Length)];
                    if (Random.value > 0.5f && GameManager.Instance.gameData.progression < 1)
                    {
                        sprite = spritesFlowers[Random.Range(0, spritesFlowers.Length)];
                    }                    
                    CreateDecoObject(sprite, x, y);
                }
            }
        }
        if (Random.value > 0.9f)
        {
            Instantiate(glowbug, transform);
        }
    }

    private void CreateDecoObject(Sprite sprite, float x, float y)
    {
        GameObject g = new GameObject("Deco");
        SpriteRenderer sr = g.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Decoration";
        sr.sprite = sprite;
        g.transform.parent = transform;
        g.transform.localPosition = new Vector3(x, y, 0);
    }

}
