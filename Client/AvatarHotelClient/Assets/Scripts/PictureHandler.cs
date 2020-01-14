using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureHandler : MonoBehaviour
{
    public float transitionTime;
    public float orgZ;
    public float orgX;
    public float orgY;
    public float orgSizeX;
    public float orgSizeY;
    public float goToZ;
    public float goToX;
    public float goToY;
    public float goToSizeX;
    public float goToSizeY;
    private bool centered = false;

    public void handleClick()
    {
        if (centered)
            returnToOriginal();
        else
            centerImage();
    }
    public void returnToOriginal()
    {
        StartCoroutine(RepositionCoroutine(transitionTime, orgZ, orgX, orgY, orgSizeX, orgSizeY));
        centered = false;
    }

    public void centerImage()
    {
        StartCoroutine(RepositionCoroutine(transitionTime, goToZ, goToX, goToY, goToSizeX, goToSizeY));
        centered = true;
    }

    IEnumerator RepositionCoroutine(float transitionTime, float goToZ, float goToX, float goToY, float goToSizeX, float goToSizeY)
    {
        StartCoroutine(RepositionImage(transitionTime, goToZ, goToX, goToY, goToSizeX, goToSizeY));
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(RepositionImage(transitionTime, goToZ, goToX, goToY, goToSizeX, goToSizeY));
        yield return new WaitForSeconds(transitionTime);
        var vector = new Vector3(goToX, goToY, goToZ);
        this.gameObject.GetComponent<RectTransform>().localPosition = vector;
        var vectorScale = new Vector3(goToSizeX, goToSizeY, 1);
        this.gameObject.GetComponent<RectTransform>().localScale = vectorScale;
    }

    IEnumerator RepositionImage(float transitionTime, float goToZ, float goToX, float goToY, float goToSizeX, float goToSizeY)
    {
        this.gameObject.GetComponent<RectTransform>().SetAsLastSibling();
        var transform = this.gameObject.GetComponent<RectTransform>();
        float currentX = transform.localPosition.x;
        float currentY = transform.localPosition.y;
        float currentSizeX = transform.localScale.x;
        float currentSizey = transform.localScale.y;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / transitionTime)
        {
            var vector = new Vector3(Mathf.Lerp(currentX, goToX, t), Mathf.Lerp(currentY, goToY, t), goToZ);
            this.gameObject.GetComponent<RectTransform>().localPosition = vector;
            var vectorScale = new Vector3(Mathf.Lerp(currentSizeX, goToSizeX, t), Mathf.Lerp(currentSizey, goToSizeY, t), 1);
            this.gameObject.GetComponent<RectTransform>().localScale = vectorScale;
            yield return null;
        }

    }
}
