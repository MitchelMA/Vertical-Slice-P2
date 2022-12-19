using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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

    [StateMethod((int)EnemyStates.Idle)]
    private EnemyStates HandleIdle()
    {
        print("HandleIdle() was called");
        int v = (int)(Random.value * 100f);
        if (v > 90)
            return EnemyStates.Moving;
        return EnemyStates.Idle;
    }

    [StateMethod((int) EnemyStates.Moving)]
    private EnemyStates HandleMoving()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(currentPos, _target) < 0.2f)
        {
            // transform.Translate(currentPos, speed * Time.deltaTime);
        }

        return EnemyStates.Moving;
    }

    private void StateChangedListener(EnemyStates old, EnemyStates current)
    {
       Debug.Log($"State-Changed: {old} -> {current}");

       if (current == EnemyStates.Moving)
       {
           Vector3 curPos = transform.position;
           _target = new Vector3(curPos.x + (Random.value - 0.5f) * 2, curPos.y, curPos.z + (Random.value - 0.5f) * 2);
       }
    }
}
