using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateMachine
{
    public State CurrentState { get; private set; }
    public GameObject Owner { get; private set; }

    private bool m_isFirstTime = true;

    public void Init(GameObject owner, State initState)
    {
        Assert.IsNotNull(owner);
        Assert.IsNotNull(initState);

        Owner = owner;
        CurrentState = initState;

        CurrentState.Init(this);
        CurrentState.OnEntrance(null);
    }

    public void Update()
    {
        State state = CurrentState.OnRun();

        if (state != null)
        {
            CurrentState.OnExit(state);
            State lastState = CurrentState;
            CurrentState = state;
            CurrentState.Init(this);
            CurrentState.OnEntrance(lastState);
        }
    }

    public void CleanUp()
    {
        CurrentState.OnExit(null);
    }
}

public abstract class State
{
    public StateMachine Parent { get; private set; }

    public void Init(StateMachine parent)
    {
        Parent = parent;
    }
    public virtual void OnEntrance(State lastState) { }
    public abstract State OnRun();
    public virtual void OnExit(State nextState) { }
}
