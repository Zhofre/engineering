namespace Engineering.Expressions.Fluent
{
    internal static class ClassificationTool
    {

        public static bool IsBaseExpression<T>(this Expression<T> expression, bool expandPrefix) where T : IExpressible
            => expression is EmptyExpression<T>
            || expression is ConstantExpression<T>
            || (!expandPrefix && expression is PrefixExpression<T>);

        public static bool IsMultiplication<T>(this Expression<T> expression) where T : IExpressible
            => expression is MultiplicationExpression<T>
            || expression is MultiplicationSequenceExpression<T>;


    }
}