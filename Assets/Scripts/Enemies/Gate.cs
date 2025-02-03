using System.Collections;
using UnityEngine;

public class Gate : Enemy
{
    public GameObject spawnAble;
    private float counter = 0;
    private float actionTime = 3.5f;

    protected override void OnUpdate()
    {
        animator.SetBool("spawning", false);
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = actionTime;
            counter += -0.25f * Random.Range(-1, 1);
            ongoingAction = StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        animator.SetBool("spawning", true);
        yield return new WaitForSeconds(1.35f);
        animator.SetBool("spawning", false);
        GameObject minion = Instantiate(spawnAble, transform.position +Vector3.up*0.5f, Quaternion.identity);
        minion.GetComponent<Brawler>().SetRoomID(roomID);
        minion.GetComponent<Brawler>().DeclareAsMinion();
    }

    private Coroutine ongoingAction;

    protected override void StopOngoingAction() 
    {
        if (ongoingAction != null)
        {
            StopCoroutine(ongoingAction);
        }
        GetComponent<Animator>().SetBool("spawning", false);  
    }


}
