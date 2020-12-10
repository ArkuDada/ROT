using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class MilitaryBar : MonoBehaviour
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
        dot.localScale = new Vector3(dotsize, dotsize);
    }

    public void resetBar()
    {
        barcontent = .5f;
        bar.localScale = new Vector3(.5f, 1f);
    }

    public void updateSize(int change)
    {
        barcontent = change / 20f;
        bar.localScale = new Vector3(barcontent, 1f);
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
            dot.localScale = new Vector3(.85f, .85f);
        }
        else if (change == 2)
        {
            dot.localScale = new Vector3(1.25f, 1.25f);
        }
        else if (change == 3)
        {
            dot.localScale = new Vector3( 1.75f, 1.75f);
        }
    }
}