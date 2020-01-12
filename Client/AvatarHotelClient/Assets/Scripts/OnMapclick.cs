using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMapclick : MonoBehaviour
{
    public void onClick()
    {
        //to do: link na zewnątrz
        Application.OpenURL("https://www.google.pl/maps/place/Asseco+Poland+S.A.,+oddział+Gdynia/@54.5151701,18.5320634,17z/data=!3m1!4b1!4m5!3m4!1s0x46fda72f00b49173:0x59b9d7e9c6d9a798!8m2!3d54.5151701!4d18.5342521");
    }
}
