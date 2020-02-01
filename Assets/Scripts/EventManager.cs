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
            if (pickedGrave.GetGraveState() == GraveState.Idle)
            {
                loopCounter = 0;
                pickedGrave.StartGraveEvent(GetRandomEvent());
                pickedGrave.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                //print("em evento");
                loopCounter++;
                StartEventInRndGrave();
            }
        }
    }




}
