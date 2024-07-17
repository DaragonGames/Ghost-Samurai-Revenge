using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float framesPerSecond;
    private float counter = 0;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        int frame = (int) (counter * framesPerSecond) % sprites.Length;
        GetComponent<SpriteRenderer>().sprite = sprites[frame];
    }
}
