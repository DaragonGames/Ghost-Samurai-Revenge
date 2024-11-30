using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObstacle : MonoBehaviour
{
    private int toughness;
    public GameObject soundPrefab;
    public GameObject particlePrefab;
    public GameObject particleBurstPrefab;
    private bool shaking;
    private float shakeDirection = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        toughness = Random.Range(0, 4);
    }
    
    void Update()
    {
        if (shaking)
        {
            transform.position += Vector3.right * Time.deltaTime * shakeDirection;
        }        
    }

    public void GetAttacked()
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        Destroy(Instantiate(particlePrefab, transform.position, Quaternion.identity),5); 
        toughness--;
        if (toughness > 0)
        {
            StartCoroutine(HandleShaking(0.22f, 0.06f));
            return;
        }
        Destroy(Instantiate(particleBurstPrefab, transform.position, Quaternion.identity),5); 
        Destroy(gameObject);
    }

    IEnumerator HandleShaking(float duration, float interval)
    {
        shaking = true;
        yield return new WaitForSeconds(interval);
        duration -= interval;
        shakeDirection *= -1;

        if (duration > 0)
        {
            StartCoroutine(HandleShaking(duration, interval));
        }
        else
        {
            shaking = false;
        }    
    }

}
