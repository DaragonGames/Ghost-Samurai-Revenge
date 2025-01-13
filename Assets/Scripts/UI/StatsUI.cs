using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    private int progress = 0;
    public GameObject prefab;
    public GameObject startPart;
    public GameObject endPart;

    // Start is called before the first frame update
    void Start()
    {
        List<int> upgrades = GameManager.Instance.gameData.dataAsList();
        foreach (int item in upgrades)
        {
            CreateIcon(item);
        }
    }

    void Awake()
    {
        GameManager.CollectItem += CreateIcon; 
    }

    void OnDestroy()
    {
        GameManager.CollectItem -= CreateIcon; 
    }

    private void CreateIcon(int id)
    {
        id-=2;
        if (id < 0) { return; } 
        // Activate in the first time
        startPart.SetActive(true);
        endPart.SetActive(true);
        // Create and adjust the Icon
        GameObject obj = Instantiate(prefab, transform);
        obj.GetComponentInChildren<UpgradeUIItem>().SetSprite(id);
        obj.GetComponent<RectTransform>().localPosition = Vector3.right * 60 * progress;
        // Move the end part
        progress++;
        endPart.GetComponent<RectTransform>().localPosition = Vector3.right * (60 * progress -25) ;
    }

}
