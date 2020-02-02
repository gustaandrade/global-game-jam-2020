using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed = 2;
    GameObject target;
    bool moving = false;
    bool returning = false;
    private Event skeletonEvent;
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

            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                moving = false;
                StartCoroutine(CrowEvent());
            }
        }

        if (returning)
        {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, targetSpawnPoint.transform.position, step);

            if (Vector2.Distance(transform.position, targetSpawnPoint.transform.position) < 0.1f)
            {
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

    public IEnumerator CrowEvent()
    {
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

        target.GetComponentInChildren<Grave>().TakeDamage(skeletonEvent.damage);
        yield return new WaitForSeconds(1f);
        GoBack();
    }

    public void SetEvent(Event e)
    {
        skeletonEvent = e;
    }

    public void GoBack()
    {
        moving = false;
        returning = true;
        target.GetComponent<Grave>().StopGraveEvent();
    }
}
