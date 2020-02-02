using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed = 2;
    public float stayTime = 1;
    GameObject target;
    bool moving = false;
    bool returning = false;
    private Event dogEvent;
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
        if (moving)
        {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                moving = false;
                StartCoroutine(DogEvent());
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

    public IEnumerator DogEvent()
    {
        enemyAnim.SetBool("event", true);
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

        target.GetComponentInChildren<Grave>().TakeDamage(dogEvent.damage);

        enemyAnim.SetBool("event", false);
        GoBack();
    }

    public void SetEvent(Event e)
    {
        dogEvent = e;
        speed = e.eventSpeed;
        stayTime = e.stayTime;
    }

    public void GoBack()
    {
        moving = false;
        returning = true;
        target.GetComponent<Grave>().StopGraveEvent();
    }
}
