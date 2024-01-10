using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : Singleton<Fade>
{
    public Image fadeImage;
    Color curColor;
    float fadeDuration = 0.0f;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        curColor = fadeImage.color;
    }

    public void FadeIn(string sceneName)
    {
        fadeDuration = 0.0f;
        StartCoroutine(FadeInCo(sceneName));
    }

    public void FadeIn()
    {
        fadeDuration = 0.0f;
        StartCoroutine(FadeInCo());
    }

    public void FadeOut()
    {
        fadeDuration = 0.0f;
        StartCoroutine(FadeOutCo());
    }
    IEnumerator FadeInCo()
    {
        yield return StartCoroutine(FadeCo());
        FadeOut();
    }

    IEnumerator FadeCo()
    {
        while (fadeDuration <= 2.0f)
        {
            fadeDuration += Time.deltaTime;
            curColor.a += Time.deltaTime * 0.5f;
            fadeImage.color = curColor;
            yield return null;
        }
        yield return null;
    }
    IEnumerator FadeInCo(string sceneName)
    {
        yield return StartCoroutine(FadeCo());
        SceneManager.LoadScene(sceneName);
        FadeOut();
    }

    IEnumerator FadeOutCo()
    {

        while (fadeDuration <= 2.0f)
        {
            fadeDuration += Time.deltaTime;
            curColor.a -= Time.deltaTime * 0.5f;
            fadeImage.color = curColor;
            yield return null;
        }
        yield return null;
    }
}
