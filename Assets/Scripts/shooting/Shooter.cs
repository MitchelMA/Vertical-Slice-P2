using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected float throwSpeed = 2f;
    [SerializeField] protected int ballCount = 2;
    [SerializeField] protected int targetIndex;
    [SerializeField] protected float chargedValue;
    [SerializeField] protected TextMeshProUGUI counter;
    [SerializeField] private GameObject counterContainer;
    [SerializeField] protected Side side;
    [SerializeField] protected Dodgeball dodgeball;
    [SerializeField] protected Dodgeball chargedDodgeball;
    [SerializeField] protected float throwTimeout = 0.5f;

    public UnityEvent shootStart = new UnityEvent();
    public UnityEvent shootEnd = new UnityEvent();
    
    protected Vector3 BallSpawnPosition => ((int) side * 2 - 1) *  Vector3.right;

    protected Character[] _targets;
    protected bool _isCharging = false;
    protected float _chargeTimer;
    public bool IsThrowing { get; private set; } = false;

    public int BallCount
    {
        get => ballCount;
        set
        {
            ballCount = value;
            counter.SetText(ballCount.ToString());
            counterContainer.SetActive(ballCount != 0);
        }
    }

    public int TargetIndex
    {
        get => targetIndex;
        set
        {
            int val = value;
            val %= _targets.Length;
            if (val < 0)
                val = 0;

            targetIndex = val;
        }
    }

    public Character CurrentTarget => _targets[targetIndex];

    // Start is called before the first frame update
    protected virtual void Start()
    {
        int other = (int) side;
        other++;
        other %= 2;
        _targets = TeamsData.Instance[(Side) other].Members.ToArray();
        counter.SetText(BallCount.ToString());
    }

    protected virtual (float, Dodgeball) GetDodgeBall()
    {
        if (_chargeTimer > chargedValue)
            return (2, chargedDodgeball);

        return (1, dodgeball);
    }

    protected virtual IEnumerator Shoot()
    {
        if (IsThrowing)
        {
            shootEnd.Invoke();
            yield break;
        }

        if (BallCount <= 0)
        {
            shootEnd.Invoke();
            yield break;
        }
        
        IsThrowing = true;
        
        Vector3 dir = CurrentTarget.transform.position - transform.position;

        var (speedMult, ball) = GetDodgeBall();
        
        shootStart.Invoke();
        yield return new WaitForSeconds(throwTimeout);
        
        BallCount -= 1;
        _chargeTimer = 0;
        var clone = Instantiate(ball);
        clone.Setup(dir, transform.position + BallSpawnPosition, throwSpeed * speedMult);
        
        shootEnd.Invoke();
        IsThrowing = false;
    }
}