
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

    public bool Shoot(float val)
    {
        if (ballCount <= 0)
            return false;

        Vector3 dir = CurrentTarget.transform.position - transform.position;

        var (speedMult, ball) = GetDodgeBall(val);
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position + BallSpawnPosition, throwSpeed * speedMult);

        BallCount -= 1;
        shootStart.Invoke();
        return true;
    }
}