namespace Domain
{
    /// <summary>
    /// Represents a Tree-node.
    /// </summary>
    public class DecisionNode<T>
    {
        public T Data { get; set; }
        public DecisionNode<T> Yes { get; set; }
        public DecisionNode<T> No { get; set; }
    }
}
