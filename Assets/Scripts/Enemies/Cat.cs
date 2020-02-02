using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed = 1;
    private float stayTime = 1;
    GameObject target;
    bool moving = false;
    bool returning = false;
    private Event catEvent;
    private GameObject targetSpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
          
            if(Vector2.Distance(transform.position, target.transform.position)< 0.1f)
            {
                moving = false;
                StartCoroutine(CatEvent());
            }
        }

        if (returning)
        {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, targetSpawnPoint.transform.position, step);

            if (Vector2.Distance(transform.position, targetSpawnPoint.transform.position) < 0.1f)
            {                
                returning = false;
                target.GetComponent<Grave>().StopGraveEvent();

                Destroy(this.gameObject);
            }
        }

    }

    public void MoveTowards(GameObject target)
    {
        this.target = target;
        targetSpawnPoint = target.GetComponent<Grave>().eventSpawnPoint;
        moving = true;
    }

    public IEnumerator CatEvent( )
    {
        float blinkTime = stayTime / 7f;
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
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(blinkTime);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(blinkTime);
        this.GetComponentInChildren<SpriteRenderer>().enabled = true; 

        target.GetComponentInChildren<Grave>().TakeDamage(catEvent.damage);
        
        GoBack(); 
    }

    public void SetEvent(Event e)
    {
        catEvent = e;
        speed = e.eventSpeed;
        stayTime = e.stayTime;
    }

    public void GoBack()
    { 
        moving = false;
        returning = true;
    }
}
