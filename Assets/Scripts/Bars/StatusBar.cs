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
        for (int i = 0; i < 3; i++)
        {
            Status[i] = updateZero(Status[i],Manager.Instance.Status[i]);
        }
        
        if (Status[0] <= 0)
        {
            Manager.Instance.endStatus = 1;
        }
        else if (Status[1] <= 0)
        {
            Manager.Instance.endStatus = 2;
        }
        else if (Status[2] <= 0)
        {
            Manager.Instance.endStatus = 3;
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
        popularityBar.updateSize(Status[0]);
        militaryBar.updateSize(Status[1]);
        economyBar.updateSize(Status[2]);
    }

    public void updateThink()
    {
        popularityBar.updateDotSize(Manager.Instance.Status[0]);
        militaryBar.updateDotSize(Manager.Instance.Status[1]);
        economyBar.updateDotSize(Manager.Instance.Status[2]);
    }

    private int updateZero(int input ,int output)
    {
        if (input + output < 0)
        {
            return 0;
        }
        else
        {
            return (input + output);
        }
    }
}