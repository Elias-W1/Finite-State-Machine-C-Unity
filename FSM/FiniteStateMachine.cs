using System.Collections.Generic;
using System;

public class FiniteStateMachine
{
    protected List<FsmState> states = new List<FsmState>();
    protected FsmState initialState;
    protected FsmState currentState;
    
    protected  FsmTransition triggeredTrans;
    protected Queue<Action> actions = new Queue<Action>();

    public FiniteStateMachine(FsmState initial)
    {
        initialState = initial;
        currentState = initialState;
        
        // Entry action of initial state
        AddAction(initial.entryAction);
    }

    public void AddState(FsmState s)
    {
        states.Add(s);
    }
    
    public void AddStates(params FsmState[] stateArray)
    {
        for (int i = 0; i < stateArray.Length; i++) AddState(stateArray[i]);
    }
    
    
    public Queue<Action> Tick()
    {
        triggeredTrans = null;

        // Find first triggered transition
        foreach (FsmTransition transition in  currentState.transitions)
        {
            if (transition.IsTriggered())
            {
                triggeredTrans = transition;
                break;
            }
        }
        
        
        if (triggeredTrans != null)
        {
            FsmState targetState = triggeredTrans.targetState;

            AddAction(currentState.exitAction);
            AddAction(triggeredTrans.action);
            AddAction(targetState.entryAction);
            
            currentState = targetState;
        }
        else
        {
            AddAction(currentState.action);
        }
        
        return actions;
        
    }

    private void AddAction(Action a)
    {
        if (a != null)
        {
            actions.Enqueue(a);
        }
    }
    
    public void ExecuteActions(Queue<Action> actions)
    {
        if (actions == null) return;
        
        Action a;
        while (actions.Count > 0)
        {
            a = actions.Dequeue();
            a();
        }
    }
    
}
