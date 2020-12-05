using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private PopularityBar popularityBar;
    [SerializeField] private MilitaryBar militaryBar;
    [SerializeField] private EconomyBar economyBar;

    public int[] Status = new int[3];

    private void Start()
    {
        Status = new int[] {10, 10, 10};
    }

    void updateTotal()
    {
        Status[0] += Manager.Instance.Status[0];
        Status[1] += Manager.Instance.Status[1];
        Status[2] += Manager.Instance.Status[2];
        if (Status[0] == 0)
        {
            
        }
        else if (Status[1] == 0)
        {
            
        }
        else if (Status[2] == 0)
        {
            
        }
    }

    public void resetBar()
    {
        popularityBar.resetBar();
        militaryBar.resetBar();
        economyBar.resetBar();
        Status = new int[] {10, 10, 10};
    }

    public void updateBar()
    {
        updateTotal();
        popularityBar.updateSize(Manager.Instance.Status[0]);
        militaryBar.updateSize(Manager.Instance.Status[1]);
        economyBar.updateSize(Manager.Instance.Status[2]);
    }

    public void updateThink()
    {
        popularityBar.updateDotSize(Manager.Instance.Status[0]);
        militaryBar.updateDotSize(Manager.Instance.Status[1]);
        economyBar.updateDotSize(Manager.Instance.Status[2]);
    }
}