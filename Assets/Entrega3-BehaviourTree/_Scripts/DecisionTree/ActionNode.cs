using System;

public class ActionNode : DecisionTreeNode
{
    public readonly string actionName;
    private readonly Action action;

    public ActionNode(string actionName, Action action)
    {
        this.actionName = actionName;
        this.action = action;
    }

    public override DecisionTreeNode Evaluate()
    {
        action?.Invoke();
        return this;
    }
}
