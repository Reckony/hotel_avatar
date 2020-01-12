﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAlpha : MonoBehaviour
{
    public float transitionTime = 1f;
    public float waitForMakingVisible = 0f;
    public float waitForMakingInvisible = 0f;
    public bool makeInActive = true;
    public void makeVisible()
    {
        this.gameObject.SetActive(true);
        Invoke("startEnumeratorVisible", waitForMakingVisible);
    }

    private void startEnumeratorVisible()
    {
        StartCoroutine(startCoroutineVisible());
    }

    private IEnumerator startCoroutineVisible()
    {
        yield return StartCoroutine(FadeToInTime(1, transitionTime));
        FadeToInstant(1, transitionTime);
    }

    public void makeInvisible()
    {
        Invoke("startEnumeratorInvisible", waitForMakingInvisible);
    }

    private void startEnumeratorInvisible()
    {
        StartCoroutine(startCoroutineInvisible());
    }

    private IEnumerator startCoroutineInvisible()
    {
        yield return StartCoroutine(FadeToInTime(0, transitionTime));
        FadeToInstant(0, transitionTime);
        if(makeInActive)
            this.gameObject.SetActive(false);
    }

    

    IEnumerator FadeToInTime(float endValue, float transitionTime)
    {
        var component = this.gameObject.GetComponent<SpriteRenderer>();
        if(component != null)
        {
            float alpha = component.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / transitionTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, endValue, t));
                this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }
        else
        {
            var componentImage = this.gameObject.GetComponent<Image>();
            float alpha = componentImage.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / transitionTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, endValue, t));
                this.gameObject.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    void FadeToInstant(float endValue, float transitionTime)
    {
        var component = this.gameObject.GetComponent<SpriteRenderer>();
        if (component != null)
        {
            Color newColor = new Color(1, 1, 1, endValue);
            this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }
        else
        {
            var componentImage = this.gameObject.GetComponent<Image>();
            Color newColor = new Color(1, 1, 1, endValue);
            this.gameObject.GetComponent<Image>().color = newColor;
        }
    }
}
