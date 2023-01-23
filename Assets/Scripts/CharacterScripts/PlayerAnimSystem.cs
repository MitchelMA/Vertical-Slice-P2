using UnityEngine;

[RequireComponent(typeof(PlayerShooter))]
public class PlayerAnimSystem : AnimSystem
{
    [SerializeField] private GameObject idleAnimation;
    [SerializeField] private GameObject runAnimation;
    [SerializeField] private GameObject throwAnimation;

    private PlayerShooter _ps;

    private static readonly int IdleTrigger = Animator.StringToHash("IdleTrigger");
    private static readonly int ThrowTrigger = Animator.StringToHash("ThrowTrigger");
    private static readonly int RunTrigger = Animator.StringToHash("RunTrigger");

    private void Start()
    {
        _ps = GetComponent<PlayerShooter>();
        runAnimation.SetActive(false);
        throwAnimation.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (character.FacingDirection.magnitude <= 0.1f)
        {
            animator.SetTrigger(IdleTrigger);
            return;
        }


        if (_ps.IsThrowing) return;
        
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