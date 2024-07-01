using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadTestimonies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static TestimonyList LoadTestimonies(){
        string jsonString = Resources.Load<TextAsset>("Data").text;
        Debug.Log(jsonString);
        return JsonUtility.FromJson<TestimonyList>(jsonString);
    }
}
