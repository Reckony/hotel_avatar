using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelContainer : MonoBehaviour
{
    public TransitionAlpha whiteBackgroundTransition;
    public TransitionAlpha darkBackgroundTransition;

    public List<TransitionAlpha> locationTransitions;
    public List<TransitionAlpha> offertFirstPageTransitions;
    public List<TransitionAlpha> offertSecondPageTransitions;
    public List<TransitionAlpha> reviewsTransitions;
    public List<TransitionAlpha> menuFirstPageTransitions;
    public List<TransitionAlpha> menuSecondPageTransitions;
    public List<TransitionAlpha> roomsTransitions;
}
