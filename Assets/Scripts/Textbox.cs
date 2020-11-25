using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class Textbox : MonoBehaviour
{
    public GameObject txt;
    public Font[] fonts;

    public void Start() { 
        txt.GetComponent<UnityEngine.UI.Text>().font = fonts[0];
        txt.GetComponent<UnityEngine.UI.Text>().fontSize = 64;
    }

    public void newGame() 
    {
        txt.GetComponent<UnityEngine.UI.Text>().fontSize = 12;
        txt.GetComponent<UnityEngine.UI.Text>().font = fonts[1];
    }

    public void updateText(string dialouge)
    {
        txt.GetComponent<UnityEngine.UI.Text>().text = dialouge;
    }
}
