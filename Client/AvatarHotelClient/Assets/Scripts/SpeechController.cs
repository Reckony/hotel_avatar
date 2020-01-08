using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject textObject;
    public TextMeshProUGUI textMesh;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void greetWithoutAnswer()
    {
        textMesh.text = "Hello, how can I help you?";
        textMesh.fontSize = 36;
        speechBubble.SetActive(true);
        textObject.SetActive(true);
    }

    public void greetWithAnswer()
    {
        textMesh.text = "Hello, I'm great! How can I help you?";
        textMesh.fontSize = 29;
        speechBubble.SetActive(true);
        textObject.SetActive(true);
    }

    public void onlyAnswer()
    {
        textMesh.text = "I'm great! What can I do for you?";
        textMesh.fontSize = 36;
        speechBubble.SetActive(true);
        textObject.SetActive(true);
    }

    public void disableTextAndBubble()
    {
        speechBubble.SetActive(false);
        textObject.SetActive(false);
    }

    public void offerHelp()
    {
        textMesh.text = "What can I do for you?";
        textMesh.fontSize = 36;
        speechBubble.SetActive(true);
        textObject.SetActive(true);
    }
}
