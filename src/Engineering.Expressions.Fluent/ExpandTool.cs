namespace Engineering.Expressions.Fluent
{
    internal static class ExpandTool
    {
        public static Expression<T> Aggresive<T>(Expression<T> expression)
            where T : IExpressible
        {
            throw new System.NotImplementedException();
        }

        public static Expression<T> Normal<T>(Expression<T> expression)
            where T : IExpressible
        {
            throw new System.NotImplementedException();
        }
    }
}