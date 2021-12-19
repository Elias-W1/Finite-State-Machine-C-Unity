using System.Collections.Generic;
using System;

public class FsmState
{
    public List<FsmTransition> transitions = new List<FsmTransition>();
    public Action entryAction;
    public Action exitAction;
    public Action action;
    
    public FsmState(Action entAction, Action mainAction, Action exAction)
    {
        entryAction = entAction;
        action = mainAction;
        exitAction = exAction;
    }
    
    public void When(Func<bool> cond, FsmState tState, Action ac)
    {
        FsmTransition t = new FsmTransition(cond, tState, ac);
        transitions.Add(t);
    }
}
