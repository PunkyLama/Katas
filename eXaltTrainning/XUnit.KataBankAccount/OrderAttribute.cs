namespace XUnit.KataBankAccount
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public int Priority { get; private set; }

        public OrderAttribute(int priority) => Priority = priority;
    }
}
