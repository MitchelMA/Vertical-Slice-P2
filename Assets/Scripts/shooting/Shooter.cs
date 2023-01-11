using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected float throwSpeed = 2f;
    [SerializeField] protected int ballCount = 2;
    [SerializeField] protected int targetIndex;
    [SerializeField] protected float chargeTimer;
    [SerializeField] protected TextMeshProUGUI counter;
    [SerializeField] protected Side side;
    [SerializeField] protected Dodgeball dodgeball;
    [SerializeField] protected Dodgeball chargedDodgeball;
    [SerializeField] protected KeyCode shootKey = KeyCode.E;

    protected Character[] _targets;
    protected BoxCollider _collider;
    protected bool _isCharging = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    protected virtual (float, Dodgeball) GetDodgeBall()
    {
        if (chargeTimer > 1f)
            return (2, chargedDodgeball);
        
        return (1, dodgeball);
    }

    protected virtual bool Shoot()
    {
        if (ballCount <= 0)
            return false;

        Vector3 dir = _targets[targetIndex].transform.position - transform.position;

        var (speedMult, ball) = GetDodgeBall();
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position + transform.right, throwSpeed * speedMult);

        ballCount -= 1;
        counter.SetText(ballCount.ToString());

        return true;
    }
}