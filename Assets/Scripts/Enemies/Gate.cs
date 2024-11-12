using UnityEngine;

public class Gate : Enemy
{
    public GameObject spawnAble;
    private float counter = 0;
    private float actionTime = 3f;

    protected override void OnUpdate()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = actionTime;
            GameObject minion = Instantiate(spawnAble, transform.position, Quaternion.identity);
            minion.GetComponent<Brawler>().SetRoomID(roomID);
            minion.GetComponent<Brawler>().DeclareAsMinion();
        }
    }

}
