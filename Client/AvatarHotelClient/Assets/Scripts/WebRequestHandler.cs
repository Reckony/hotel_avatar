using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestHandler : MonoBehaviour
{
    public AvatarController avatarController;
    public SpeechController speechController;
    public NotificationHandler notificationHandler;

    readonly string urlAvatar = "https://avatarprototyprestapi.azurewebsites.net/api/Avatar";
    readonly string urlBooking = "https://avatarprototyprestapi.azurewebsites.net/api/Booking";
    //readonly string urlAvatar = "http://localhost:56130/api/Avatar";
    //readonly string urlBooking = "http://localhost:56130/api/Booking";

    public void PostToAvatarWebRequest(string message)
    {
        StartCoroutine(PostToAvatar(message));
    }

    private IEnumerator PostToAvatar(string message)
    {
        string jstr = "{ \"Message\":\"" + message + "\" }";
        var request = new UnityWebRequest(urlAvatar, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jstr);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        handleReaction(request.downloadHandler.text);
    }

    private void handleReaction(string result)
    {
        speechController.disableTextAndBubble();
        switch (result)
        {
            case "0":
                avatarController.Rest();
                break;
            case "1":
                avatarController.Greet(false);
                break;
            case "2":
                avatarController.Greet(true);
                break;
            case "3":
                avatarController.Answer();
                break;
            case "4":
                avatarController.Jump();
                break;
            case "5":
                avatarController.offerHelp();
                break;
            case "6":
                avatarController.showMap();
                break;
            case "7":
                avatarController.reset();
                break;
            case "8":
                avatarController.showOffert();
                break;
            case "9":
                avatarController.showGallery();
                break;
            case "10":
                avatarController.showRooms();
                break;
            case "11":
                avatarController.showReviews();
                break;
            case "12":
                avatarController.showMenu();
                break;
        }
    }

    public void PostToBookingWebRequest()
    {
        StartCoroutine(PostToBooking());
    }

    private IEnumerator PostToBooking()
    {
        string jstr = "{}";
        var request = new UnityWebRequest(urlBooking, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jstr);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }

    public void GetToBookingWebRequest()
    {
        StartCoroutine(GetToBooking());
    }

    private IEnumerator GetToBooking()
    {
        string jstr = "{}";
        var request = new UnityWebRequest(urlBooking, "GET");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jstr);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        int result;
        Int32.TryParse(request.downloadHandler.text, out result);
        if (result > 0)
            notificationHandler.displayNotification(result);
    }
}
