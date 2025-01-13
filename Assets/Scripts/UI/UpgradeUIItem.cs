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
        image.sprite = sprites[id];
    }
}
