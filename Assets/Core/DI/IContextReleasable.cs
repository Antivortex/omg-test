namespace Core.DI
{
    public interface IContextReleasable
    {
        void ReleaseByContext(IContext context);
    }
}