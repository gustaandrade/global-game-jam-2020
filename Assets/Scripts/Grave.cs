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
    private int graveStatus = 0;
    private GraveState graveState;
    public Sprite[] graveSprites;
    private Event currentGraveEvent;

    public GameObject eventSpawnPoint;

    public GameObject graveTimer;

    private float timeToFix;
    private bool startClock;

    float waitTime = 1;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (startClock)
        {
            graveTimer.GetComponentInChildren<Image>().fillAmount += 1.0f / waitTime * Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        //if (!ToolsManager.instance.usingTool)
        if(TimerManager.Instance.IsNightTime())
            FixGrave();
    }

    private void FixGrave()
    {
        print("try to fix " + graveState + " status: " + graveStatus);
        if (graveState == GraveState.Idle && graveStatus >= 1 && graveStatus < 4)
        {

            StartCoroutine(RepairGrave());
        }
        else
        {
            print("cant repair");
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
        this.GetComponent<SpriteRenderer>().sprite = graveSprites[graveStatus];
    }

    private void ResetGraveSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = graveSprites[0];
    }

    public GraveState GetGraveState()
    {
        return graveState;
    }

    public void TakeDamage(int damage)
    {
        if (EventManager.Instance().canDamage)
        {
            graveStatus += damage;
            if (graveStatus >= 3)
            {
                graveStatus = 3;
                LifeManager.instance.lifes--;
                if (EventManager.Instance().DestroyedGravesCount() == 6)
                {
                    print("gameover");
                    PauseManager.instance.GameOverGame();
                }
            }
            UpdateGraveSprite();
            print("tomou " + damage + "e esta no estado " + graveStatus);
        }
    }

    public Event GetCurrentGraveEvent()
    {
        return currentGraveEvent;
    }

    public IEnumerator RepairGrave()
    {
        if (this.currentGraveEvent != null)
        {
            if (this.currentGraveEvent.toolToFix == ToolsManager.instance.selectedTool.GetToolType())
            {
                graveState = GraveState.Fixing;
                //wait for repaier
                graveTimer.SetActive(true);
                startClock = true;
                waitTime = 3 * graveStatus;
                ToolsManager.instance.usingTool = true;
                yield return new WaitForSeconds(waitTime);

                //repair
                graveStatus= 0;
                //ToolsManager.instance.ClearSelectedTool();
                graveTimer.SetActive(false);
                startClock = false;
                ToolsManager.instance.usingTool = false;
                ResetGrave();
                print("grave reparada para o status" + graveStatus);
            }
            else
            {
                print("wrong tool");
            }
        }

    }


    public int GetGraveStatus()
    {
        return graveStatus;
    }

    public void ResetGrave()
    {
        graveStatus = 0;
        graveState = GraveState.Idle;
        ResetGraveSprite();
    }
}
