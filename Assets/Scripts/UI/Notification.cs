using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Sprite[] sprites;
    public Image icon;
    public TMP_Text tagline;
    public TMP_Text info;

    private float counter = 0;

    public void SetValues(int id){
        gameObject.SetActive(true);
        counter = 2;
        icon.sprite = sprites[id];
        switch (id){
            case 0:
                tagline.text = "Uncover the truth";
                info.text = "+1 Testimony available";
                break;
            case 1:
                tagline.text = "Rice is life";
                info.text = "Restored some HP";
                break;
            case 2:
                tagline.text = "Faster than your reflection";
                info.text = "Increased movement speed";
                break;
            case 3:
                tagline.text = "A swift fighter's memory";
                info.text = "Increased sword attack speed";
                break;
            case 4:
                tagline.text = "The strength of another";
                info.text = "Increased maximum HP";
                break;
            case 5:
                tagline.text = "The urge to kill";
                info.text = "Increased attack damage";
                break;
            case 6:
                tagline.text = "Protection against evil spirits";
                info.text = "Increased defense";
                break;
            case 7:
                tagline.text = "What a lucky sight";
                info.text = "Increased luck";
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
