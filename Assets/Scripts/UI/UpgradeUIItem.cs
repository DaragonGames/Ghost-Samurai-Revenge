using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeUIItem : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    public void SetSprite(int id)
    {
        if (id > 1)
        {
            image.sprite = sprites[id-2];
        }

    }
}
