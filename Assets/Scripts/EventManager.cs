using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Dog,
    Crow,
    Skeleton,
    Graffiti, 
    Lightning,
    Cat
}

[System.Serializable]
public class Event
{
    public EventType eventType;

    [Range(1, 3)]
    public int damage;

    public ToolType toolToFix;

    private Grave eventGrave;

    public void SetEventGrave(Grave g)
    {
        eventGrave = g;
    }
}

public class EventManager : MonoBehaviour
{
    public List<Event> eventsList;
    public List<Grave> gravesList;

    private static EventManager instance;

    public static EventManager Instance()
    {
        return instance;
    }


    //event objects
    public GameObject cat;
    public GameObject crow;
    public GameObject skeleton;
    public GameObject graffiti;
    public GameObject lightning;
    public GameObject dog;
     

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartEventInRndGrave();
        }
    }

    public Event GetRandomEvent()
    {
       // return eventsList[1];
        return eventsList[Random.Range(0, eventsList.Count)];
    }

    public Event GetEventOfType(EventType eType)
    {
        foreach(Event e in eventsList)
        {
            if (e.eventType == eType)
            {
                return e;
            }
        }
        return null;
    }

    int loopCounter = 0;
    public void StartEventInRndGrave()
    {
        if(loopCounter > 10)
        {
            loopCounter = 0;
        }
        else
        {
            Grave pickedGrave = gravesList[Random.Range(0, gravesList.Count)];
            if (pickedGrave.GetGraveState() == GraveState.Idle && pickedGrave.GetGraveStatus() < 4)
            {
                loopCounter = 0;
                Event rndEvent = GetRandomEvent();
                pickedGrave.SetGraveEvent(rndEvent);
                StartEvent(rndEvent, pickedGrave);
                 
            }
            else
            {
                //print("em evento");
                loopCounter++;
                StartEventInRndGrave();
            }
        }
    }


    public void StartEvent(Event e, Grave g)
    {
        switch (e.eventType)
        {
            case EventType.Cat:
                GameObject catObj = Instantiate(cat, g.eventSpawnPoint.transform.position, Quaternion.identity);
                CatEvent(e, g, catObj);
                break;
            case EventType.Crow:
               GameObject crowObj = Instantiate(crow, g.eventSpawnPoint.transform.position, Quaternion.identity);
                CrowEvent(e, g, crowObj); 
                break;
            case EventType.Lightning:
                GameObject lightningObj = Instantiate(lightning, g.eventSpawnPoint.transform.position, Quaternion.identity);
                LightningEvent(e, g, lightningObj);
                break;
            case EventType.Graffiti:
                GameObject graffitiObj = Instantiate(graffiti, g.eventSpawnPoint.transform.position, Quaternion.identity);
                GraffitiEvent(e, g, graffitiObj);
                break;
            case EventType.Skeleton:
                GameObject skeletonObj = Instantiate(skeleton, g.eventSpawnPoint.transform.position, Quaternion.identity);
                SkeletonEvent(e, g, skeletonObj);
                break;
            case EventType.Dog:
                GameObject dogObj = Instantiate(dog, g.eventSpawnPoint.transform.position, Quaternion.identity);
                DogEvent(e, g, dogObj);
                break;

        }
    }


    private void CatEvent(Event e, Grave g, GameObject catObj)
    {
        catObj.GetComponent<Cat>().SetEvent(e);
        catObj.GetComponent<Cat>().MoveTowards(g.gameObject);
    }

    private void CrowEvent(Event e, Grave g, GameObject crowObj)
    {
        crowObj.GetComponent<Crow>().SetEvent(e);
        crowObj.GetComponent<Crow>().MoveTowards(g.gameObject);        
    }

    private void LightningEvent(Event e, Grave g, GameObject l)
    {
        l.GetComponent<Lightning>().SetEvent(e);
        l.GetComponent<Lightning>().MoveTowards(g.gameObject);
    }
    private void GraffitiEvent(Event e, Grave g, GameObject gra)
    {
        gra.GetComponent<Graffiti>().SetEvent(e);
        gra.GetComponent<Graffiti>().MoveTowards(g.gameObject);
    }
    private void SkeletonEvent(Event e, Grave g, GameObject skel)
    {
        skel.GetComponent<Skeleton>().SetEvent(e);
        skel.GetComponent<Skeleton>().MoveTowards(g.gameObject);
    }
    private void DogEvent(Event e, Grave g, GameObject dog)
    {
        dog.GetComponent<Dog>().SetEvent(e);
        dog.GetComponent<Dog>().MoveTowards(g.gameObject);
    }

}
