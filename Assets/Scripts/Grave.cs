using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GraveState
{
    Idle,
    Event,
    Fixing
}

public class Grave : MonoBehaviour
{
    private int graveStatus = 1;
    private GraveState graveState;
    public Sprite[] graveSprites;
    private Event currentGraveEvent;

    public GameObject eventSpawnPoint;

    public GameObject graveTimer;

    private float timeToFix;
    private bool startClock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startClock)
        {
          //  graveTimer.GetComponentInChildren<Image>().fillAmount += Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        FixGrave();
    }

    private void FixGrave()
    {
        print("try to fix " + graveState + " status: " + graveStatus);
        if(graveState == GraveState.Idle && graveStatus > 1 && graveStatus < 4)
        {
           
            StartCoroutine(RepairGrave());
        }
    }

    public void SetGraveEvent(Event _currentEvent)
    {
        currentGraveEvent = _currentEvent;
        graveState = GraveState.Event;
        currentGraveEvent.SetEventGrave(this);
    }

    public void StopGraveEvent()
    {
        graveState = GraveState.Idle;
    }

    private void UpdateGraveSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = graveSprites[graveStatus-1];
    }

    public GraveState GetGraveState()
    {
        return graveState;
    }

    public void TakeDamage(int damage)
    {
        graveStatus += damage;
        UpdateGraveSprite();
        print("tomou " + damage + "e esta no estado " + graveStatus);
    }

    public Event GetCurrentGraveEvent()
    {
        return currentGraveEvent;
    }

    public IEnumerator RepairGrave()
    { 
        if (this.currentGraveEvent.toolToFix == ToolsManager.instance.selectedTool.GetToolType())
        {
            graveState = GraveState.Fixing;
            //wait for repaier
            graveTimer.SetActive(true);
            startClock = true;
            yield return new WaitForSeconds(2 * graveStatus);

            //repair
            graveStatus--;
            ToolsManager.instance.ClearSelectedTool();
            graveTimer.SetActive(false);
            startClock = false;
            graveState = GraveState.Idle;

            print("grave reparada para o status" + graveStatus);
        }
        else
        {
            print("wrong tool");
        }
             
    }


    public int GetGraveStatus()
    {
        return graveStatus;
    }
}
