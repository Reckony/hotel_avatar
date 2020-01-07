using System.Collections;
using UnityEngine;

public class ScrollButtonHandler : MonoBehaviour
{
    public float additionalYValue;
    public float transitionTime;
    public float maxUpYValue;
    public float maxDownYValue;
    public float maxUpYBeforeDisappearing;
    public float maxUpYBeforeAppearing;
    public TransitionAlpha childTransition;

    public void scrollUp()
    {
        StartCoroutine(scrollIEnumerator(additionalYValue));
    }

    public void scrollDown()
    {
        StartCoroutine(scrollIEnumerator(-additionalYValue));
    }

    public IEnumerator scrollIEnumerator(float additionalYValue)
    {
        var position = this.gameObject.GetComponent<Transform>().localPosition;
        float currentY = position.y;
        float endValueY = currentY + additionalYValue;
        if (endValueY > maxUpYValue)
            endValueY = maxUpYValue;
        if (endValueY < maxDownYValue)
            endValueY = maxDownYValue;
        StartCoroutine(ScrollCoroutine(endValueY, transitionTime));
        var transition = this.gameObject.GetComponent<TransitionAlpha>();
        if(transition != null && maxUpYBeforeDisappearing != 0 && endValueY > maxUpYBeforeDisappearing)
        {
            transition.makeInvisible();
            if(childTransition != null)
                childTransition.makeInvisible();
        }
        if (transition != null && maxUpYBeforeAppearing != 0 && endValueY < maxUpYBeforeAppearing)
        {
            transition.makeVisible();
            if (childTransition != null)
                childTransition.makeVisible();
        }
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(ScrollCoroutine(endValueY, transitionTime));
        yield return new WaitForSeconds(transitionTime);
        scrollSetInstant(endValueY);
    }

    private IEnumerator ScrollCoroutine(float endValueY, float time)
    {
        var position = this.gameObject.GetComponent<Transform>().localPosition;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Vector3 vector = new Vector3(position.x, Mathf.Lerp(position.y, endValueY, t), position.z);
            this.gameObject.GetComponent<Transform>().localPosition = vector;
            yield return null;
        }
    }

    private void scrollSetInstant(float endValueY)
    {
        var position = this.gameObject.GetComponent<Transform>().localPosition;
        Vector3 vector = new Vector3(position.x, endValueY, position.z);
        this.gameObject.GetComponent<Transform>().localPosition = vector;
    }
}
