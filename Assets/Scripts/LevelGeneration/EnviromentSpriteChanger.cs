using UnityEngine;

public class EnviromentSpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        int n = GameManager.Instance.gameData.progression;
        sr.sprite = sprites[Mathf.Min(n, 2)];
    }

}
