using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    [Space(10), Header("Objects")]
    public RectTransform MovingObjects;
    public Animator Fade;

    [Space(10), Header("Values")] 
    public float MovingMenuEndPosX = 1280f;
    public float MovingMenuEndPosYUp = -800f;
    public float MovingMenuEndPosYDown = 800f;
    public float MovingMenuDurationX = 4f;
    public float MovingMenuDurationY = 2f;
    public float WaitToPlayTime = 6f;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnPlay()
    {
        MovingObjects.DOLocalMoveX(MovingMenuEndPosX, MovingMenuDurationX).SetEase(Ease.Linear);
        StartCoroutine(Wait(WaitToPlayTime));
    }

    public void OnAbout()
    {
        MovingObjects.DOLocalMoveY(MovingMenuEndPosYUp, MovingMenuDurationY).SetEase(Ease.Linear);
    }

    public void OnBackAbout()
    {
        MovingObjects.DOLocalMoveY(0f, MovingMenuDurationY).SetEase(Ease.Linear);
    }

    public void OnExit()
    {
        MovingObjects.DOLocalMoveY(MovingMenuEndPosYDown, MovingMenuDurationY).SetEase(Ease.Linear);
    }

    public void OnBackExit()
    {
        MovingObjects.DOLocalMoveY(0f, MovingMenuDurationY).SetEase(Ease.Linear);
    }

    public void OnClose()
    {
        Application.Quit();
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Fade.SetTrigger("DoFadeIn");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
