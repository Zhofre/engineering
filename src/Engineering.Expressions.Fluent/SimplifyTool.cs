namespace Engineering.Expressions.Fluent
{
    internal static class SimplifyTool
    {
        public static Expression<T> Normal<T>(Expression<T> expression)
            where T : IExpressible
        {
            // simplify multiplications: ((a*b)*(c*d)) = (a*b*c*d) / m*m = m²
            // if (expression.IsMultiplication())
            // {
            //     var classifiedExpression = expression as IClassifiable<T>;
            //     var parts = classifiedExpression.GetNumerator();
            //     //var expandedMultiplication = ExpandMultiplication(parts);
                
            //     // todo: expressions should be equatable so we can simplify to exponents



            // }

            // simplify divisions: eg. m³ / m²
            
            // drill down
            return expression.Transform(Normal);
        }
    }
}