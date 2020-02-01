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
    private int life = 3;
    private GraveState graveState;
    public Sprite[] graveSprites;  

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

    public void StartGraveEvent()
    {
        graveState = GraveState.Event;
    }

    public void StopGraveEvent()
    {
        graveState = GraveState.Idle;
    }

    private void UpdateGraveSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = graveSprites[life];
    }



}
