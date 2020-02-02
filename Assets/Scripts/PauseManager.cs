using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseFrame;
    public Animator Fade;

    public void PauseGame()
    {
        PauseFrame.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseFrame.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Fade.SetTrigger("DoFadeIn");
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
