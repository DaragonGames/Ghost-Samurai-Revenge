using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestimonyHandler
{
    public static Dictionary<int, TestimonyData> testimonyData = new Dictionary<int, TestimonyData>();
    public static List<int> seenTestimonies = new List<int>();
    public static List<int> avaibleTestimonies = new List<int>();
    public static List<int> selectedTestimonies = new List<int>();
    public static List<int> shownTestimonies = new List<int>();

    public static void LoadTestimonies(){
        string jsonString = Resources.Load<TextAsset>("Data").text;
        TestimonyList source = JsonUtility.FromJson<TestimonyList>(jsonString);
        foreach (TestimonyData testimony in source.list)
        {
            testimonyData.Add(testimony.id,testimony);
            avaibleTestimonies.Add(testimony.id);
        }
    }

    public static int GetTestimony()
    {
        if (avaibleTestimonies.Count > 0)
        {
            int id = avaibleTestimonies[Random.Range(0,avaibleTestimonies.Count)];
            avaibleTestimonies.Remove(id);
            shownTestimonies.Add(id);
            return id;
        }
        if (seenTestimonies.Count > 0)
        {
            int id = seenTestimonies[Random.Range(0,seenTestimonies.Count)];
            seenTestimonies.Remove(id);
            shownTestimonies.Add(id);
            return id;
        }
        else 
        {
            return selectedTestimonies[Random.Range(0,selectedTestimonies.Count)];
        }
    }

    public static void SelectTestimonies(int id)
    {
        foreach (int testimonyID in shownTestimonies)
        {
            seenTestimonies.Add(testimonyID);
        }
        shownTestimonies = new List<int>();
        selectedTestimonies.Add(id);
        seenTestimonies.Remove(id);
        
        switch (id)
        {
            case 0:
                GameManager.Instance.gameData.collectItem(6);
                break;
            case 1:
                GameManager.Instance.gameData.collectItem(5);
                break;
            case 2:
                GameManager.Instance.gameData.collectItem(2);
                break;
            case 3:
                GameManager.Instance.gameData.collectItem(7);
                break;
            case 4:
                GameManager.Instance.gameData.collectItem(4);
                break;
            case 5:
                GameManager.Instance.gameData.collectItem(3);
                break;
            case 6:
                GameManager.Instance.gameData.ghostWrath+=10;
                break;
            case 7:
                GameManager.Instance.gameData.ghostWrath-=10f;
                break;
            case 8:
                GameManager.Instance.gameData.ghostWrath=50;
                GameManager.Instance.gameData.LuckUpgradesCollected-=3;
                break;
        }
    }

}
