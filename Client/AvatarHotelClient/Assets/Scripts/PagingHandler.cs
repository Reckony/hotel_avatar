using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagingHandler : MonoBehaviour
{
    public List<TransitionAlpha> firstPageObjects;
    public List<TransitionAlpha> secondPageObjects;

    public void showFirstPage()
    {
        foreach (TransitionAlpha gameObject in secondPageObjects)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (TransitionAlpha gameObject in firstPageObjects)
        {
            gameObject.makeVisible();
        }
    }

    public void showSecondPage()
    {
        foreach (TransitionAlpha gameObject in firstPageObjects)
        {
            if (gameObject.gameObject.activeInHierarchy)
                gameObject.makeInvisible();
        }
        foreach (TransitionAlpha gameObject in secondPageObjects)
        {
            gameObject.makeVisible();
        }
    }
}
