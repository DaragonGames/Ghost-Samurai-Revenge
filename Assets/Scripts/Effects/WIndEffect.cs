using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIndEffect : MonoBehaviour
{
    private float defaultCurrentMax = 0.1f;
    private float defaultCurrentMin = -0.1f;
    private float strongCurrentMax = 4f;
    private float strongCurrentMin = 2f;
    private float activeCurrent = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlowingWind());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right *activeCurrent * Time.deltaTime;
    }

    IEnumerator BlowingWind()
    {
        yield return new WaitForSeconds(Random.Range(1f,4f));
        activeCurrent = Random.Range(strongCurrentMin, strongCurrentMax);
        yield return new WaitForSeconds(Random.value);
        activeCurrent = Random.Range(defaultCurrentMin, defaultCurrentMax);
        StartCoroutine(BlowingWind());
    }
}
