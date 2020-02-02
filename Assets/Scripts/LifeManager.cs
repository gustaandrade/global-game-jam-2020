using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public Transform heartContainer;

    public int lifes = 6;

    public static LifeManager instance;
    List<GameObject> hearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        foreach (Transform child in heartContainer)
        {
            hearts.Add(child.gameObject);
        }
    }

    public void UpdateHeartContainer()
    {
        foreach(GameObject h in hearts)
        {
            h.SetActive(false);
        }
        
        for(int i = 0; i < lifes; i++)
        {
            hearts[i].SetActive(true);
        }

        if(lifes == 0)
        {
            PauseManager.instance.GameOverGame();
        }
    }
     
}
