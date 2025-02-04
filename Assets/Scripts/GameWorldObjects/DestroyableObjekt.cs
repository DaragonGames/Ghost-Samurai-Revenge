using System.Collections;
using UnityEngine;

public class DestroyableObjekt : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject particleBurstPrefab;
    public Sprite secondState;
    private bool shaking;
    private float shakeDirection = 0.8f;
    private Damageable damageable;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
        damageable.SetValues(Random.Range(1, 4));
        damageable.DamageEvent +=GetAttacked;
        damageable.DeathEvent += Die;
    }
    
    void Update()
    {
        if (shaking)
        {
            transform.position += Vector3.right * Time.deltaTime * shakeDirection;
        }
        if (secondState != null && damageable.GetHealthPercentage() <= 0.51f) 
        {
            GetComponent<SpriteRenderer>().sprite = secondState;
            secondState = null;
        }        
    }

    public void GetAttacked()
    {
        Destroy(Instantiate(particlePrefab, transform.position, Quaternion.identity),5); 
        StartCoroutine(HandleShaking(0.22f, 0.06f));
    }

    void Die()
    {
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
