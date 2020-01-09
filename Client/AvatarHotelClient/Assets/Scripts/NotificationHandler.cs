using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHandler : MonoBehaviour
{
    public float shakeSpeed = 1f;
    public float shakeStrength = 1f;
    public float shakeTime = 0.5f;
    public float repeatSendGetRequestTime = 60f;

    public WebRequestHandler requestHandler;

    private bool shake = false;
    public void displayNotification(int number)
    {
        StartCoroutine(notificationIEnumerator(number));
    }

    private void Start()
    {
        InvokeRepeating("sendGetRequest", repeatSendGetRequestTime / 2, repeatSendGetRequestTime);
    }

    private void Update()
    {
        if(shake)
            gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, Mathf.Sin(Time.time * shakeSpeed) * shakeStrength, gameObject.GetComponent<RectTransform>().localRotation.w);
    }

    private IEnumerator notificationIEnumerator(int number)
    {
        gameObject.GetComponentInChildren<Text>().text = number.ToString() + " reservations have just been made";
        var scroller = gameObject.GetComponent<ScrollButtonHandler>();
        scroller.scrollDown();
        yield return new WaitForSeconds(scroller.transitionTime+0.5f);
        shake = true;
        yield return new WaitForSeconds(shakeTime);
        shake = false;
        gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, gameObject.GetComponent<RectTransform>().localRotation.w);
        yield return new WaitForSeconds(0.5f);
        scroller.scrollUp();
    }

    private void sendGetRequest()
    {
        requestHandler.GetToBookingWebRequest();
    }
}

