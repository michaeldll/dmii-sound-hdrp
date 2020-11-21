using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private World[] _worlds = null;

    [SerializeField]
    private bool _shuffleOrder = false;

    [SerializeField]
    private PlayerMovement _playerMovement = null;

    private void ShuffleNavigationOrder()
    {
        List<int> order = new List<int>(_worldsNavigation.order);
        List<int> newOrder = new List<int>();

        int initialLength = order.Count;

        for (int i = 0; i < initialLength; i++)
        {
            int randomIndex = (int)Mathf.Round(UnityEngine.Random.Range(0f, (float)order.Count - 1));
            newOrder.Add(order[randomIndex]);
            order.RemoveAt(randomIndex);
        }

        _worldsNavigation.SetOrder(newOrder.ToArray());
    }

    private void SetNavigationOrder()
    {
        List<World> worlds = new List<World>(_worlds);
        List<int> order = new List<int>();

        for (int i = 0; i < worlds.Count; i++)
        {
            order.Add(i);
        }

        _worldsNavigation.SetOrder(order.ToArray());
    }

    // Hooks
    void Start()
    {
        SetNavigationOrder();

        if (_shuffleOrder)
        {
            ShuffleNavigationOrder();
        }

        _worldsNavigation.InitNavigation();
        //_playerMovement.InitPosition(_worlds[_worldsNavigation.active].transform.position);
        Debug.Log(_worlds[_worldsNavigation.active]);
        _worlds[_worldsNavigation.active].Enter();
    }
}
