namespace Engineering.Expressions.Fluent
{
    internal static class SimplifyTool
    {

        /// <summary>
        ///     Creates a canonical form of the expression, replacing all prefix that don't prefix a constant expression with a scale.
        ///     The resulting expression is of the form Scale[Base^exp], with base: Prefix[Constant] or 1/[Prefix[Constant]].!--
        ///     Binary or sequence expressions will create canonical forms for each component
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<T> CanonicalForm<T>(this Expression<T> expression) where T : IExpressible
        {
            if (!expression.IsConstantOrUnaryExpression())
                return expression.Transform(CanonicalForm);

            if (expression is ConstantExpression<T>)
                return expression.Prefix(Prefix.none).Exponent(1d).Scale(1d);

            var prefixExpr = expression as PrefixExpression<T>;
            if (prefixExpr != null && prefixExpr.Content is ConstantExpression<T>)
                return expression.Exponent(1d).Scale(1d);

            var unExpr = expression as UnaryExpression<T>;
            if (unExpr == null)
                throw new System.NotImplementedException();

            var canonContent = unExpr.Content.CanonicalForm() as ScaleExpression<T>;

            if (prefixExpr != null) // replace with scale
                return canonContent.Content
                    .Scale(canonContent.Scale * prefixExpr.Prefix.Value());

            var scExpr = expression as ScaleExpression<T>;
            if (scExpr != null)
                return canonContent.Content
                    .Scale(canonContent.Scale * scExpr.Scale);

            var expExpr = expression as ExponentExpression<T>;
            if (expExpr != null)
            {
                var canonContentExp = canonContent.Content as ExponentExpression<T>;
                return canonContentExp.Content
                    .Exponent(canonContentExp.Exponent * expExpr.Exponent)
                    .Scale(System.Math.Pow(canonContent.Scale, expExpr.Exponent));
            }

            var invExpr = expression as InverseExpression<T>;
            if (invExpr != null)
            {
                var canonContentExp = canonContent.Content as ExponentExpression<T>;
                return canonContentExp.Content
                    .Invert()
                    .Exponent(canonContentExp.Exponent)
                    .Scale(1 / canonContent.Scale);
            }

            throw new System.NotImplementedException();
        }


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

            throw new System.NotImplementedException();

            // }

            // simplify divisions: eg. m³ / m²

            // drill down
            //return expression.Transform(Normal);
        }
    }
}