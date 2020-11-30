﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Navigation", menuName = "ScriptableObjects/Navigation", order = 2)]
public class Navigation : ScriptableObject
{
    public int[] order;

    public int active;
    public int previous;
    public int next;

    private int _index = 0;
    private bool _isComplete = false;

    // Getter
    public bool IsComplete
    {
        get { return _isComplete; }
    }

    // Public
    public void SetOrder(int[] newOrder)
    {
        order = newOrder;
        _isComplete = false;
    }

    public void InitNavigation()
    {
        _index = 0;
        SetNavigation();
    }

    public void SetNavigation()
    {
        active = order[mod(_index, order.Length)];
        next = order[mod(_index + 1, order.Length)];
        previous = order[mod(_index - 1, order.Length)];

        if (_index >= order.Length - 1)
        {
            // _isComplete = true;
        }
    }

    public void Next()
    {
        _index++;
        SetNavigation();
    }

    public void Previous()
    {
        _index--;
        SetNavigation();
    }

    //Private
    public int mod(int n, int m) {
        return ((n % m) + m) % m;
    }
}
