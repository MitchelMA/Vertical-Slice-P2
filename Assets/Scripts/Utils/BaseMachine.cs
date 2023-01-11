using System;
using System.Collections.Generic;
using System.Reflection;

public sealed class BaseMachine<T> where T : Enum
{
    private T _currentState;
    
    private Func<T, T> _fromEveryState ;
    
    private readonly Dictionary<T, Func<T>> _register = new Dictionary<T, Func<T>>();
    
    public T CurrentState
    {
        get => _currentState;
        private set
        {
            if (_currentState.Equals(value))
                return;
                
            T oldVal = _currentState;
            _currentState = value;

            if (!oldVal.Equals(_currentState))
               StateChanged?.Invoke(oldVal, _currentState); 
        }
    }
    
    public T StartState { get; }

    public Func<T, T> FromEveryState
    {
        get => _fromEveryState;

        set
        {
            if (_fromEveryState != null)
                return;

            _fromEveryState = value;
        }
    }

    public delegate void StateChangedDelegate(T previousState, T currentState);
    public event StateChangedDelegate StateChanged;

    public BaseMachine(T startState)
    {
        _currentState = startState;
        StartState = startState;
    }

    public BaseMachine(object invoker)
    {
        RegisterClass(invoker);
    }

    public BaseMachine(T startState, object invoker)
    {
        CurrentState = startState;
        StartState = startState;
        RegisterClass(invoker);
    }

    public bool Register(T fromState, Func<T> stateMethod)
    {
        bool ret = false;
        try
        {
            _register.Add(fromState, stateMethod);
            ret = true;
        }
        catch
        {
            // ignore 
        }

        return ret;
    }

    public bool Unregister(T fromState) => 
        _register.Remove(fromState);

    public void GetNext()
    {
        // call the from every state
        if (FromEveryState != null)
        {
           var nextState = FromEveryState.Invoke(CurrentState);
           CurrentState = nextState;
        }
        // after the everyState check invoke the specified state-handler
        CurrentState = _register[CurrentState].Invoke();
    }

    public void RegisterClass(object o)
    {
        Type type = o.GetType();

        var methodinfos =
            type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        int l = methodinfos.Length;
        for (int i = 0; i < l; i++)
        {
            var methodInfo = methodinfos[i];

            if (!Attribute.IsDefined(methodInfo, typeof(StateMethodAttribute)))
                continue;

            // get all the custom attributes of type StateMethodAttribute
            var attrs = (StateMethodAttribute[])
                methodInfo.GetCustomAttributes(typeof(StateMethodAttribute));

            int m = attrs.Length;
            for (int j = 0; j < m; j++)
            {
                var attr = attrs[j];

                // safely cast attr.State from Enum -> T
                var tAttr = (T) Enum.ToObject(typeof(T), attr.FromState);

                // cast the methodInfo to a delegate
                Delegate med = Delegate.CreateDelegate(typeof(Func<T>), o, methodInfo.Name);
                // cast the Delegate to Func<T> and add it into the register
                Register(tAttr, (Func<T>) med);
            }
        }
    }
}