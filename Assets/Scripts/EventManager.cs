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

    [Range(1, 5)]
    public float eventSpeed;

    [Range(0, 5)]
    public float stayTime = 1;

    public AudioClip eventAudio;

    public void SetEventGrave(Grave g)
    {
        eventGrave = g;
    }
}

public class EventManager : MonoBehaviour
{
    public List<Event> eventsList;
    public List<Grave> gravesList;

    public List<GameObject> enemies = new List<GameObject>();

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


    public bool canDamage = true;

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
        int wave = TimerManager.Instance.GetWaveCount();
        // return eventsList[1];
        if (wave <= 3)
        {
            return eventsList[Random.Range(0, 2)];
        }
        if (wave > 3 && wave <= 5)
        {
            return eventsList[Random.Range(0, 4)];
        }
        else
        {
            return eventsList[Random.Range(0, eventsList.Count)];
        }
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
                enemies.Add(catObj);
                CatEvent(e, g, catObj);
                break;
            case EventType.Crow:
               GameObject crowObj = Instantiate(crow, g.eventSpawnPoint.transform.position, Quaternion.identity);
                enemies.Add(crowObj);
                CrowEvent(e, g, crowObj); 
                break;
            case EventType.Lightning:
                GameObject lightningObj = Instantiate(lightning, g.eventSpawnPoint.transform.position, Quaternion.identity);
                enemies.Add(lightningObj);
                LightningEvent(e, g, lightningObj);
                break;
            case EventType.Graffiti:
                GameObject graffitiObj = Instantiate(graffiti, g.eventSpawnPoint.transform.position, Quaternion.identity);
                enemies.Add(graffitiObj);
                GraffitiEvent(e, g, graffitiObj);
                break;
            case EventType.Skeleton:
                GameObject skeletonObj = Instantiate(skeleton, g.eventSpawnPoint.transform.position, Quaternion.identity);
                enemies.Add(skeletonObj);
                SkeletonEvent(e, g, skeletonObj);
                break;
            case EventType.Dog:
                GameObject dogObj = Instantiate(dog, g.eventSpawnPoint.transform.position, Quaternion.identity);
                if(dogObj.transform.position.y < 1)
                {
                    dogObj.transform.localScale = new Vector3(1, -1, 1);
                }
                enemies.Add(dogObj);
                DogEvent(e, g, dogObj);
                break;
        }
    }

    public void DestroyAllEnemies()
    {
        foreach(GameObject e in enemies)
        {
            Destroy(e);
        }
        enemies.Clear();
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

    public void ResetAllGraves()
    {
        canDamage = true;
        foreach(Grave g in gravesList)
        {
            g.ResetGrave();
        }
    }

    public int DestroyedGravesCount()
    {
        int count = 0;
        foreach (Grave g in gravesList)
        {
            if (g.GetGraveStatus() == 4){
                count++;
            }
        }

        return count;
    }

}
