using Engineering.Quantities;

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
        
        public override bool Equals(IUnit other)
        {
            if (other == null)
                return false;
            var otherBase = other as BaseUnit;
            if (otherBase == null)
                return false;
            return Name.Equals(otherBase.Name)
                && Notation.Equals(otherBase.Notation)
                && Quantity.Equals(otherBase.Quantity);
        }

        protected sealed override int GetHashCodeImpl()
            => 17*Name.GetHashCode() + Notation.GetHashCode() + 21*Quantity.GetHashCode();
    }
}