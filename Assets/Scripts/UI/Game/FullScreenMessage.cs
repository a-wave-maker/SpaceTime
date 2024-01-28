using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FullScreenMessage : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    public void SetMessage(string message)
    {
        SetMessage(message, Color.white);
    }

    public void SetMessage(string message, Color color)
    {
        text.text = message;
        text.color = color;
    }

    public void DisplayMessage(float duration)
    {
        DisplayMessage(duration / 3, duration / 3, duration / 3);
    }

    public void DisplayMessage(float fadeInDuration, float duration, float fadeOutDuration)
    {
        StartCoroutine(DisplayMessageCoroutine(fadeInDuration, duration, fadeOutDuration));
    }

    private IEnumerator DisplayMessageCoroutine(float fadeInDuration, float duration, float fadeOutDuration)
    {
        StartCoroutine(FadeInCoroutine(fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + duration);
        StartCoroutine(FadeOutCoroutine(fadeOutDuration));
        yield return new WaitForSeconds(fadeOutDuration);
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float elapsedTime = 0f;
        text.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 0.8f, elapsedTime / duration);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha * 1.2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        print("Fading out");
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0.8f, 0f, elapsedTime / duration);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}
