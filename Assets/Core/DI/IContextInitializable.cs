namespace Core.DI
{
    public interface IContextInitializable
    {
        void InitializeByContext(IContext context);
    }
}