
using UnityEngine;

[RequireComponent(typeof(EnemyAi))]
public class EnemyShooter : Shooter
{
    private EnemyAi _ai;

    protected override void Start()
    {
       _ai = GetComponent<EnemyAi>();
       base.Start();
    }

    private (float, Dodgeball) GetDodgeBall(float val)
    {
        if (val > chargedValue)
            return (2, chargedDodgeball);

        return (1, dodgeball);
    }

    public void Shoot(float val)
    {
        _chargeTimer = val;
        StartCoroutine(base.Shoot());
    }
}