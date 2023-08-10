using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(int sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.CrossFadeAlpha(0f, 0.2f, false);
        yield return new WaitForSeconds(0.2f);
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(int sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.CrossFadeAlpha(1f, 0.3f, false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}