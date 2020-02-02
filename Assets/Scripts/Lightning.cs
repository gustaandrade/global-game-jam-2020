using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float speed = 2;
    GameObject target;
    bool moving = false;
    bool returning = false;
    private Event lightningEvent;
    private GameObject targetSpawnPoint;


    // Start is called before the first frame update
    void Start()
    {

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

        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);

        target.GetComponentInChildren<Grave>().TakeDamage(lightningEvent.damage);
        
        GoBack();
    }

    public void SetEvent(Event e)
    {
        lightningEvent = e;
    }

    public void GoBack()
    { 
        target.GetComponent<Grave>().StopGraveEvent();
        Destroy(this.gameObject);
    }
}
