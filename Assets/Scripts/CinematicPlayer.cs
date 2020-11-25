using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayer : MonoBehaviour
{
    private Animator _animator;
    private bool _isStanding = false;
    private bool _hasLookedAround = false;

    public void StandUp()
    {
        _isStanding = true;
        _animator.SetBool("isStandingUp", true);
    }

    public void LookAround()
    {
        _hasLookedAround = true;
        _animator.SetBool("isLookingAround", true);
    }

    // Hooks
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        StandUp();
    }
}
