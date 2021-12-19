using System.Collections.Generic;
using System;

public class FiniteStateMachine
{
    protected List<FsmState> states = new List<FsmState>();
    protected FsmState initialState;
    protected FsmState currentState;
    
    protected  FsmTransition triggeredTrans;
    protected List<Action> actions = new List<Action>();
    
    
    private bool firstTick = true;
    
    public FiniteStateMachine(FsmState inital)
    {
        initialState = inital;
        currentState = initialState;
    }

    public void AddState(FsmState s)
    {
        states.Add(s);
    }
    
    public void AddStates(params FsmState[] stateArray)
    {
        for (int i = 0; i < stateArray.Length; i++) AddState(stateArray[i]);
    }
    
    public List<Action> Tick()
    {
        triggeredTrans = null;
        actions = new List<Action>();

        // Execute entry action of initial state.
        if (firstTick)
        {
            if(initialState.entryAction != null) actions.Add(initialState.entryAction);
            firstTick = false;
        }
        
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
            actions.Add(a);
        }
    }
    
    public void ExecuteActions(List<Action> actions)
    {
        if (actions == null) return;

        foreach (Action a in actions)
        {
            a();
        }
    }
}
