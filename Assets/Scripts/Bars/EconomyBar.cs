using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;



public class EconomyBar : MonoBehaviour
{
    private Transform bar;
    private Transform dot;
    private float barcontent;
    private float dotsize;
    private int decision;

    private void Start()
    {
        bar = transform.Find("Bar");
        barcontent = .5f;
        bar.localScale = new Vector3(barcontent, 1f);

        dot = transform.Find("Dot");
        dotsize = 0f;
        dot.localScale = new Vector3(dotsize,dotsize);

    }

    public void resetBar()
    {
        barcontent = .5f;
        bar.localScale = new Vector3(.5f, 1f);
    }

    public void updateSize(int change)
    {
        if (change != 0)
        {
            barcontent += change / 20f;
            bar.localScale = new Vector3(barcontent, 1f);
        }

    }
    public void updateDotSize(int change)
    {
        change = Math.Abs(change);
        if (change == 0)
        {
            dot.localScale = new Vector3(0f, 0f);
        }
        else if (change == 1)
        {
            dot.localScale = new Vector3(.4f, .4f);
        }
        else if (change == 2)
        {
            dot.localScale = new Vector3(.7f, .7f);
        }


    }
}
