/// <summary>
/// Base node of the binary Decision Tree (every internal node is a SI/NO question, every leaf is an action).
/// </summary>
public abstract class DecisionTreeNode
{
    public abstract DecisionTreeNode Evaluate();
}
