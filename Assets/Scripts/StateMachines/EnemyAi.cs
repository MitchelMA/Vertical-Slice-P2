using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    private BaseMachine<EnemyStates> _baseMachine;
    public UnityEvent<EnemyStates, EnemyStates> stateChanged = new UnityEvent<EnemyStates, EnemyStates>();

    private Vector3 _target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minEvadeDist = 1f;
    [SerializeField] private float ballDetectRadius = 4f;
    [SerializeField] private Side boundsSide;
    [SerializeField] private string DodgeballLayer;

    private Vector4 _bounds;

    private void Awake()
    {
        _baseMachine = new BaseMachine<EnemyStates>(EnemyStates.Idle, this);
        _baseMachine.StateChanged += StateChangedListener;
        _baseMachine.StateChanged += stateChanged.Invoke;
        _baseMachine.FromEveryState = EveryStateHandler;

        _target = transform.position;
        _bounds = Bounds.Instance[boundsSide];
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Bounds.Instance[Side.Right]);
    }

    private void FixedUpdate()
    {
        _baseMachine.GetNext();
    }

    private EnemyStates EveryStateHandler(EnemyStates currentState)
    {
        // check for thrown balls to evade
        if (ShouldEvade())
            return EnemyStates.Evading;

        Vector3? val = ShouldPickup();
        if (val != null)
        {
            _target = (Vector3) val;
            return EnemyStates.Pickup;
        }

        return currentState;
    }

    [StateMethod((int) EnemyStates.Idle)]
    private EnemyStates HandleIdle()
    {
        int v = (int) (Random.value * 100f);
        return v > 98 ? EnemyStates.Moving : EnemyStates.Idle;
    }

    [StateMethod((int) EnemyStates.Moving)]
    private EnemyStates HandleMoving()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(currentPos, _target) < 0.2f)
        {
            return EnemyStates.Idle;
        }

        Vector3 newPos = Vector3.MoveTowards(currentPos, _target, speed * Time.deltaTime);
        if (!newPos.IsInBounds(_bounds))
            return EnemyStates.Idle;

        transform.position = newPos;
        return EnemyStates.Moving;
    }

    [StateMethod((int) EnemyStates.Evading)]
    private EnemyStates HandleEvading()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(currentPos, _target) < 0.2f)
            return EnemyStates.Idle;

        Vector3 newPos = Vector3.MoveTowards(currentPos, _target, speed * Time.deltaTime);
        if (!newPos.IsInBounds(_bounds))
            return EnemyStates.Idle;

        transform.position = newPos;
        return EnemyStates.Evading;
    }

    [StateMethod((int) EnemyStates.Pickup)]
    private EnemyStates HandlePickup()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(currentPos, _target) < 0.2f)
        {
            return EnemyStates.Idle;
        }

        Vector3 tar = new Vector3(_target.x, transform.position.y, _target.z);
        Vector3 newPos = Vector3.MoveTowards(currentPos, tar, speed * Time.deltaTime);
        if (!newPos.IsInBounds(_bounds))
            return EnemyStates.Idle;

        transform.position = newPos;
        return EnemyStates.Pickup;
    }
    
    private void StateChangedListener(EnemyStates old, EnemyStates current)
    {
        Debug.Log($"State-Changed: {old} -> {current}");
        Vector3 curPos = transform.position;

        // preparations for switching to the states moving and evading
        switch (current)
        {
            case EnemyStates.Moving:
                _target = GetRandomNextTarget(curPos, 5);
                break;
            case EnemyStates.Evading:
                _target = GetRandomNextTarget(curPos, 10);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, ballDetectRadius);
    }

    private bool ShouldEvade()
    {
        Dodgeball dodgeball = GetClosestBall();
        if (dodgeball == null)
            return false;
        
        Vector3 ballPos = dodgeball.transform.position;
        if (dodgeball.Movement.magnitude == 0 || dodgeball.WasDropped)
            return false;

        // path prediction with linear formula
        PqrForm pred = new PqrForm(new Vector2(ballPos.x, ballPos.z),
            new Vector2(ballPos.x + dodgeball.Direction.x, ballPos.z + dodgeball.Direction.z));

        float yPos = pred.GetY(transform.position.x);
        if (Mathf.Abs(yPos - transform.position.z) < minEvadeDist)
            return true;

        return false;
    }

    private Vector3? ShouldPickup()
    {
        Dodgeball dodgeball = GetClosestBall();
        if (dodgeball == null)
            return null;

        Vector3 ballPos = dodgeball.transform.position;
        if (!dodgeball.WasDropped)
            return null;

        return ballPos;
    }

    private Dodgeball GetClosestBall()
    {
        int layerMask = LayerMask.GetMask(DodgeballLayer);

        Collider[] colliders = new Collider[10];
        var size = Physics.OverlapSphereNonAlloc(transform.position, ballDetectRadius, colliders, layerMask);
        if (size == 0)
            return null;

        Collider cc = GetClosest(colliders);
        return cc.GetComponent<Dodgeball>();
    }

    private Collider GetClosest(params Collider[] colliders)
    {
        int l = colliders.Length;

        if (l == 0)
            throw new ArgumentException("Length of colliders is 0");

        Collider currentClosest = colliders[0];

        if (l == 1)
            return currentClosest;

        for (int i = 1; i < l; i++)
        {
            Collider current = colliders[i];
            if (current == null)
                break;

            if (Vector3.Distance(current.transform.position, transform.position) <
                Vector3.Distance(currentClosest.transform.position, transform.position))
            {
                currentClosest = current;
            }
        }

        return currentClosest;
    }

    private Vector3 GetRandomNextTarget(Vector3 fromPos, float multiplier)
    {
        Vector3 target = new Vector3(fromPos.x + (Random.value - 0.5f) * multiplier, fromPos.y,
            fromPos.z + (Random.value - 0.5f) * multiplier);

        while (!target.IsInBounds(_bounds))
        {
            target = new Vector3(fromPos.x + (Random.value - 0.5f) * multiplier, fromPos.y,
                fromPos.z + (Random.value - 0.5f) * multiplier);
        }

        return target;
    }
}