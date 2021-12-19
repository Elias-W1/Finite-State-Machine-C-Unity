using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FsmTransition
{
    protected Func<bool> condition;
    protected FsmState _targetState;
    protected Action _action;
    
    
    public FsmTransition(Func<bool> cond, FsmState tState, Action ac)
    {
        //Debug.Log("Setting cond");
        condition = cond;
        //Debug.Log("Setting tstate");
        targetState = tState;
        //Debug.Log("Setting ac");
        action = ac;
        
    }
    
    public FsmState targetState
    {
        get => _targetState;
        set => _targetState = value;
    }
    
    public Action action
    {
        get => _action;
        set => _action = value;
    }
    
    public virtual bool IsTriggered()
    {
        if (condition == null)
        {
            return true;
            
        }
        
        return condition();
        
    }
    
}
