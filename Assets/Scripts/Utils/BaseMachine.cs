using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public sealed class BaseMachine<T> where T : Enum
{
    public T CurrentState { get; private set; }
    public T StartState { get; }

    private readonly Dictionary<T, Func<T>> _register = new Dictionary<T, Func<T>>();

    public delegate void StateChangedDelegate(T previousState, T currentState);

    public event StateChangedDelegate StateChanged;

    public BaseMachine(T startState)
    {
        CurrentState = startState;
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

    public bool Unregister(T fromState)
    {
        bool ret = false;
        try
        {
            _register.Remove(fromState);
            ret = true;
        }
        catch
        {
            // ignore
        }

        return ret;
    }

    public void GetNext()
    {
        T oldState = CurrentState;

        try
        {
            CurrentState = _register[CurrentState].Invoke();
            if (!oldState.Equals(CurrentState))
                StateChanged?.Invoke(oldState, CurrentState);
        }
        catch
        {
            // ignore
        }
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
                var tAttr = (T) Enum.ToObject(typeof(T), attr.State);

                // cast the methodInfo to a delegate
                Delegate med = Delegate.CreateDelegate(typeof(Func<T>), o, methodInfo.Name);
                // cast the Delegate to Func<T> and add it into the register
                Register(tAttr, (Func<T>) med);
            }
        }
    }
}