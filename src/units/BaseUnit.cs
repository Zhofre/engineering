namespace Engineering.Units
{
    public abstract class BaseUnit : Unit
    {
        protected BaseUnit(string name, string notation, IQuantity quantity)
            : base(name, quantity)
        {
            Notation = notation;
        }
        public sealed override string Notation { get; }
    }
}