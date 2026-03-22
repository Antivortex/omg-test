
using System;

namespace Core.DI
{
    public interface IContext
    {
        void AddServiceToRegister<T>(T service, string tag = null);
        void Register(IContext parentContext);
        void Release();
        T Resolve<T>(string tag = null);
    }
}