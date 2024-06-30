using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int height=4;
    private int width=4;
    private Dictionary<int, Room> rooms= new Dictionary<int, Room>();
    public GameObject roomPrefab;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int start = Random.Range(0,width);
        int end = Random.Range(0,width) + width*(height-1);
        int current = start;
        rooms.Add(start, CreateNewRoom(start));

        while (current != end)
        {
            List<int> possibilities = new List<int>{};

            foreach (int id in PossibleNeighbours(current))
            {
                if (!rooms.ContainsKey(id))
                {
                    possibilities.Add(id);
                }
            }

            if (possibilities.Count == 0)
            {
                List<int> roomKeys = rooms.Keys.ToList();
                current = roomKeys[Random.Range(0,roomKeys.Count)];
            }
            else
            {
                int next=possibilities[Random.Range(0,possibilities.Count)];
                rooms.Add(next, CreateNewRoom(next));
                rooms[current].createOpening(next -current);
                rooms[next].createOpening(current -next);
                current=next;
            }          
        }     
        BetterStartChoices(start);
        BetterExit(end);  
        MoreConnections(0.05f);
    }

    public List<int> PossibleNeighbours(int toCheck)
    {
        List<int> possibilities = new List<int>{};

        if (toCheck%width > 0)
        {
            possibilities.Add(toCheck-1);
        }
        if (toCheck%width < width-1)
        {
            possibilities.Add(toCheck+1);
        }
        if (toCheck >= width)
        {
            possibilities.Add(toCheck-width);
        }
        if (toCheck < width*(height-1))
        {
            possibilities.Add(toCheck+width);
        }

        return possibilities;
    }

    public Room CreateNewRoom(int id) 
    {
        Vector3 position = new Vector3(id%width,(int)(id/height),0);
        GameObject obj = Instantiate(roomPrefab, position, Quaternion.identity);
        Room room= obj.GetComponent<Room>();
        room.SetID(id);
        return room;
    }

    public void BetterStartChoices(int startID)
    {
        int next = startID-1;
        if (startID > 0)
        {
            if (!rooms.ContainsKey(next))
            {
                rooms.Add(next, CreateNewRoom(next));                
            }
            rooms[startID].createOpening(next -startID);
            rooms[next].createOpening(startID -next);
        }
        next = startID+1;
        if (startID < width-1)
        {

            if (!rooms.ContainsKey(next))
            {
                rooms.Add(next, CreateNewRoom(next));                
            }
            rooms[startID].createOpening(next -startID);
            rooms[next].createOpening(startID -next);
        }
        next = startID+width;
        if (!rooms.ContainsKey(next))
        {
            rooms.Add(next, CreateNewRoom(next));                
        }
        rooms[startID].createOpening(next -startID);
        rooms[next].createOpening(startID -next);
    }

    private void BetterExit(int endID)
    {
        int next = endID-1;
        if (endID > width*(height-1) && rooms.ContainsKey(next))
        {
            rooms[endID].createOpening(next -endID);
            rooms[next].createOpening(endID -next);           
        }

        next = endID+1;
        if (endID < width*height-1 && rooms.ContainsKey(next))
        {
            rooms[endID].createOpening(next -endID);
            rooms[next].createOpening(endID -next);           
        }

        next = endID-width;
        if (rooms.ContainsKey(next))
        {
            rooms[endID].createOpening(next -endID);
            rooms[next].createOpening(endID -next);           
        }
    }

    private void MoreConnections(float chance)
    {
        foreach (int id1 in rooms.Keys)
        {
            foreach (int id2 in PossibleNeighbours(id1))
            {
                if (rooms.ContainsKey(id2))
                {
                    if (Random.value < chance/2)
                    {
                        rooms[id1].createOpening(id2 -id1);
                        rooms[id2].createOpening(id1 -id2);
                    }                    
                }
            }
        }        
    }

    private void CreateExtraRoom() 
    {
        if (rooms.Count == width*height)
        {
            return;
        }
        int random =  Random.Range(0,width*height);
        while (rooms.ContainsKey(random))
        {
            Random.Range(0,width*height);
        }

        rooms.Add(random, CreateNewRoom(random));

        List<int> possibilities = new List<int>{};

        foreach (int id in PossibleNeighbours(random))
        {      
            if (rooms.ContainsKey(id))
            {
                possibilities.Add(id);                 
            }
        }

        if (possibilities.Count > 0)
        {
            int id = possibilities[Random.Range(0,possibilities.Count)];
            rooms[random].createOpening(id -random);
            rooms[id].createOpening(random -id);
        }
        else
        {
            CreateExtraRoom();
        }
    }
}