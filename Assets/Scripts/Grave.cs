using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            graveState = GraveState.Fixing;
            RepairGrave();
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
       // this.GetComponent<SpriteRenderer>().sprite = graveSprites[life];
    }

    public GraveState GetGraveState()
    {
        return graveState;
    }

    public void TakeDamage(int damage)
    {
        graveStatus += damage;

        print("tomou " + damage + "e esta no estado " + graveStatus);
    }

    public Event GetCurrentGraveEvent()
    {
        return currentGraveEvent;
    }

    public void RepairGrave()
    { 
            if (this.currentGraveEvent.toolToFix == ToolsManager.instance.selectedTool.GetToolType())
            {
                graveStatus--;
                ToolsManager.instance.ClearSelectedTool();
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
