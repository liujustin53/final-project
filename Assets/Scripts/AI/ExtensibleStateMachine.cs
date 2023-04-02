using System.Collections.Generic;
using System;
using UnityEngine;

public class ExtensibleStateMachine
{
    private State currentState;
    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> anyTransitions = new List<Transition>();

    public void SetState(State state) {
        if (currentState != null) {
            currentState.OnExit();
        }
        currentState = state;
        state.OnEnter();
    }

    public void AddTransition(State from, State to, Func<bool> predicate) {
        Type fromType = from.GetType();
        if (!transitions.ContainsKey(fromType)) {
            transitions.Add(fromType, new List<Transition>());
        }
        transitions[fromType].Add(new Transition {To = to, Condition = predicate});
    }

    public void AddTransition(State to, Func<bool> predicate) {
        anyTransitions.Add(new Transition {To = to, Condition = predicate });
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (var transition in anyTransitions) {
            if (transition.Condition()) {
                SetState(transition.To);
                return;
            }
        }
        foreach (var transition in transitions[currentState.GetType()]) {
            if (transition.Condition()) {
                SetState(transition.To);
                return;
            }
        }
        currentState.Update();
    }

    public abstract class State {
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public abstract void Update();
    }

    protected class Transition {
        public Func<bool> Condition;
        public State To;
    }
}
