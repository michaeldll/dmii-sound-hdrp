using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Navigation", menuName = "ScriptableObjects/Navigation", order = 2)]
public class Navigation : ScriptableObject
{
    public int[] order;

    public int active;
    public int previous;
    public int next;

    public int index = 0;

    // Public
    public void SetOrder(int[] newOrder)
    {
        order = newOrder;
    }

    public void Reset()
    {
        index = 0;
    }

    public void InitNavigation()
    {
        SetNavigation();
    }

    public void SetNavigation()
    {
        active = order[mod(index, order.Length)];
        next = order[mod(index + 1, order.Length)];
        previous = order[mod(index - 1, order.Length)];
    }

    public void Next()
    {
        index++;
        SetNavigation();
    }

    public void Previous()
    {
        index--;
        SetNavigation();
    }

    //Private
    public int mod(int n, int m)
    {
        return ((n % m) + m) % m;
    }
}
