using System;
using UnityEngine;

[RequireComponent(typeof(EnemyShooter))]
public class EnemyAnimSystem : AnimSystem
{
    [SerializeField] private GameObject idleAnimation;
    [SerializeField] private GameObject runAnimation;
    [SerializeField] private GameObject throwAnimation;

    private EnemyShooter _es;

    private static readonly int IdleTrigger = Animator.StringToHash("IdleTrigger");
    private static readonly int ThrowTrigger = Animator.StringToHash("ThrowTrigger");
    private static readonly int RunTrigger = Animator.StringToHash("RunTrigger");

    private Vector3 _curPos;
    private Vector3 _diff = Vector3.zero;

    private void Start()
    {
        _es = GetComponent<EnemyShooter>();
        runAnimation.SetActive(false);
        throwAnimation.SetActive(false);
        _curPos = transform.position;
    }

    private void FixedUpdate()
    {
        var lastPos = _curPos;
        _curPos = transform.position;
        _diff = _curPos - lastPos;
        
        if (_es.IsThrowing) return;
        
        if (_diff.magnitude / Time.fixedDeltaTime <= 3f)
        {
            animator.SetTrigger(IdleTrigger);
            return;
        }
        
        animator.SetTrigger(RunTrigger);
        Vector3 oldScale = runAnimation.transform.localScale;
        runAnimation.transform.localScale =
            new Vector3(character.FacingDirection.x > 0 ? 1 : -1, oldScale.y, oldScale.z);
        
        
    }

    public void ShootStart()
    {
        animator.SetTrigger(ThrowTrigger);
    }
}