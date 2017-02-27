using System.Collections.Generic;

namespace Engineering.Expressions
{
    public interface IClassifiable<T>
        where T : IExpressible
    {
        double GetExponent();
        double GetScale();
        IEnumerable<Expression<T>> GetNumerator();
        IEnumerable<Expression<T>> GetDenominator();
    }
}