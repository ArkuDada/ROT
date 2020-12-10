using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject txt;
    private int end;
    public void updateEnd(int end)
    {
        if (end != -1)
        {
            if (end == 0)
            {
                updateText("Normal Ending");
            }else if (end == 1)
            {
                updateText("People = 0");
            }else if (end == 2)
            {
                updateText("People = 20");
            }else if (end == 3)
            {
                updateText("Army = 0");
            }else if (end == 4)
            {
                updateText("Army = 20");
            }else if (end == 5)
            {
                updateText("Economic = 0");
            }else if (end == 6)
            {
                updateText("Economic = 20");
            }else if (end == 7)
            {
                updateText("You let people free!");
            }

            if (Input.GetMouseButtonDown(0))
            {
                updateText("Thank for playing\n ROT DEMO");
                Manager.Instance.endStatus = -99;
            }
        }
    }
    public void updateText(string dialouge)
    {
        txt.GetComponent<UnityEngine.UI.Text>().text = dialouge;
    }
}
