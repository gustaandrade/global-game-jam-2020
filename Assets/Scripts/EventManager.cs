using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Cat,
    Light,
    Skater,
    Dust,
    Pidgeon,
    Vandal
}

[System.Serializable]
public class Event
{
    public EventType eventType;

    [Range(1, 3)]
    public int damage;

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


    // Start is called before the first frame update
    void Start()
    {
        
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
        return eventsList[0];
     //   return eventsList[Random.Range(0, eventsList.Count)];
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
            if (pickedGrave.GetGraveState() == GraveState.Idle)
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
                GameObject catObj =  Instantiate(cat, g.eventSpawnPoint.transform.position, Quaternion.identity);
                CatEvent(e, g, catObj);
                break;
            case EventType.Dust:
                StartCoroutine(DustEvent());
                break;
            case EventType.Light:
                StartCoroutine(LightEvent());
                break;
            case EventType.Pidgeon:
                StartCoroutine(PidgeonEvent());
                break;
            case EventType.Skater:
                StartCoroutine(SkaterEvent());
                break;
            case EventType.Vandal:
                StartCoroutine(VandalEvent());
                break;

        }
    }


    public void CatEvent(Event e, Grave g, GameObject catObj)
    {
        catObj.GetComponent<Cat>().SetEvent(e);
        catObj.GetComponent<Cat>().MoveTowards(g.gameObject);
        
        print("cat");
    }

    public IEnumerator DustEvent()
    {
        yield return new WaitForSeconds(0.2f);
        print("DustEvent");
    }

    public IEnumerator LightEvent()
    {
        yield return new WaitForSeconds(0.2f);
        print("LightEvent");
    }

    public IEnumerator PidgeonEvent()
    {
        yield return new WaitForSeconds(0.2f);
        print("PidgeonEvent");
    }

    public IEnumerator SkaterEvent()
    {
        yield return new WaitForSeconds(0.2f);
        print("SkaterEvent");
    }

    public IEnumerator VandalEvent()
    {
        yield return new WaitForSeconds(0.2f);
        print("VandalEvent");
    }

}
