using System;

public class QuestionNode : DecisionTreeNode
{
    private readonly Func<bool> condition;
    private readonly DecisionTreeNode siNode;
    private readonly DecisionTreeNode noNode;

    public QuestionNode(Func<bool> condition, DecisionTreeNode siNode, DecisionTreeNode noNode)
    {
        this.condition = condition;
        this.siNode = siNode;
        this.noNode = noNode;
    }

    public override DecisionTreeNode Evaluate()
    {
        return condition() ? siNode.Evaluate() : noNode.Evaluate();
    }
}
