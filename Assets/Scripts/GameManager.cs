using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private State _readyState = null;

    [SerializeField]
    private CinematicController _introCinematicController;

    [SerializeField]
    private World[] _worlds = null;

    [SerializeField]
    private bool _shuffleOrder = false;

    [SerializeField]
    private PlayerMovement _playerMovement = null;

    private delegate void TimeoutCallback();

    private void PlayIntroCinematic()
    {
        _introCinematicController.PlayCinematic();
        float duration = (float)_introCinematicController.duration;
        TimeoutCallback onCompleteCallback = OnIntroCompleted;
        StartCoroutine(SetTimeout(duration, OnIntroCompleted));
        _introCinematicController.Reset();
    }

    private void OnIntroCompleted()
    {
        _worlds[_worldsNavigation.active].Enter();
        _readyState.SetState(true);
    }

    private void ShuffleNavigationOrder()
    {
        List<int> order = new List<int>(_worldsNavigation.order);
        List<int> newOrder = new List<int>();

        // Starting world is always 0
        newOrder.Add(0);

        int initialLength = order.Count;

        for (int i = 1; i < initialLength; i++)
        {
            int randomIndex = (int)Mathf.Round(UnityEngine.Random.Range(1f, (float)order.Count - 1));
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

        PlayIntroCinematic();

        _worldsNavigation.InitNavigation();
        _readyState.SetState(false);
        _playerMovement.InitPosition(_worlds[_worldsNavigation.active].transform.position);
    }

    // Utils
    IEnumerator SetTimeout(float time, TimeoutCallback Method)
    {
        yield return new WaitForSeconds(time);

        Method();
    }
}
