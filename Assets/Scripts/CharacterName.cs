using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterName : MonoBehaviour
{
    public GameObject txt;
    public void updateText(string dialouge)
    {
        txt.GetComponent<UnityEngine.UI.Text>().text = dialouge;
    }
}