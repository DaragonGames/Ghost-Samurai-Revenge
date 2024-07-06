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
        Debug.Log("Avaible: " + avaibleTestimonies.Count + " Seen: " + seenTestimonies.Count + " Selected: "+ selectedTestimonies.Count); 
    }
}
