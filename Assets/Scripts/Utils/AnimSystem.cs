using System;
using UnityEngine;


[RequireComponent(typeof(Character), typeof(Animator), typeof(Shooter))]
public abstract class AnimSystem : MonoBehaviour
{
    protected Character character;
    protected Animator animator;
    protected Shooter shooter;

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
        shooter = GetComponent<Shooter>();
    }
}