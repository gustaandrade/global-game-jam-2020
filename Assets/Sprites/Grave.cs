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
        if(graveState == GraveState.Event)
        {
            graveState = GraveState.Fixing;
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
        currentGraveEvent = null;
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
        print("tomou " + damage);
    }



}
