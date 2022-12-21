using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : MonoBehaviour
{
    private BaseMachine<EnemyStates> _baseMachine;
    public UnityEvent<EnemyStates, EnemyStates> stateChanged = new UnityEvent<EnemyStates, EnemyStates>();

    private Vector3 _target;
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        _baseMachine = new BaseMachine<EnemyStates>(EnemyStates.Idle, this);
        _baseMachine.StateChanged += StateChangedListener;
        _baseMachine.StateChanged += stateChanged.Invoke;
        _baseMachine.FromEveryState = EveryStateHandler;

        _target = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        _baseMachine.GetNext();
    }

    private EnemyStates EveryStateHandler(EnemyStates currentState)
    {
        // check for thrown balls to evade

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
        transform.position = newPos;
        return EnemyStates.Moving;
    }

    private void StateChangedListener(EnemyStates old, EnemyStates current)
    {
        Debug.Log($"State-Changed: {old} -> {current}");

        if (current == EnemyStates.Moving)
        {
            Vector3 curPos = transform.position;
            _target = new Vector3(curPos.x + (Random.value - 0.5f) * 5, curPos.y,
                curPos.z + (Random.value - 0.5f) * 5);
        }
    }
}