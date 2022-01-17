using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FsmTransition
{
    protected Func<bool> condition;
    public FsmState targetState;
    public Action action;
    
    public FsmTransition(Func<bool> cond, FsmState tState, Action ac)
    {
        condition = cond;
        targetState = tState;
        action = ac;
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
