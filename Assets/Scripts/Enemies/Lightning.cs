using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float speed = 2;
    private float stayTime = 1;
    GameObject target;
    bool moving = false;
    bool returning = false;
    private Event lightningEvent;
    private GameObject targetSpawnPoint;
    private Animator enemyAnim;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    public void MoveTowards(GameObject target)
    {
        this.target = target;
        targetSpawnPoint = target.GetComponent<Grave>().eventSpawnPoint;
        StartCoroutine(LightningEvent());
    }

    public IEnumerator LightningEvent()
    {
        this.transform.position = target.transform.position;
      //  enemyAnim.SetBool("event", true);
     //   float blinkTime = stayTime / 7f;
        /*  this.GetComponentInChildren<SpriteRenderer>().enabled = false;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = true;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = false;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = true;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = false;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = true;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = false;
          yield return new WaitForSeconds(blinkTime);
          this.GetComponentInChildren<SpriteRenderer>().enabled = true;*/
        yield return new WaitForSeconds(stayTime);
        target.GetComponentInChildren<Grave>().TakeDamage(lightningEvent.damage);
        //enemyAnim.SetBool("event", false);
        GoBack();
    }

    public void SetEvent(Event e)
    {
        lightningEvent = e;
        speed = lightningEvent.eventSpeed;        
        stayTime = e.stayTime;

    }

    public void GoBack()
    { 
        target.GetComponent<Grave>().StopGraveEvent();
        Destroy(this.gameObject);
    }
}
