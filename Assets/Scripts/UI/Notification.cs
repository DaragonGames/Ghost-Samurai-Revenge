using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Sprite[] sprites;
    public Image icon;
    public TMP_Text info;

    private float counter = 0;

    public void SetValues(int id){
        gameObject.SetActive(true);
        counter = 3;
        icon.sprite = sprites[id];
        switch (id){
            case 0:
                info.text = "Uncover the truth";
                break;
            case 1:
                info.text = "Rice is life";
                break;
            case 2:
                info.text = "Faster than your reflection";
                break;
            case 3:
                info.text = "A swift fighter's memory";
                break;
            case 4:
                info.text = "The strength of another";
                break;
            case 5:
                info.text = "The urge to kill";
                break;
            case 6:
                info.text = "Protection against evil spirits";
                break;
            case 7:
                info.text = "What a lucky sight";
                break;
        }
    }

    public void Update()
    {
        if (counter <= 0)
        {
            gameObject.SetActive(false);
        }
        counter -= Time.deltaTime;
    }

    void Awake()
    {
        GameManager.CollectItem += SetValues; 
    }

    void OnDestroy()
    {
        GameManager.CollectItem -= SetValues; 
    }
}
