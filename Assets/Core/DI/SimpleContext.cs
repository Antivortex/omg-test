using System;
using System.Collections.Generic;

namespace Core.DI
{
    public class SimpleContext : IContext
    {
        private readonly Dictionary<ServiceKey, object> _servicesDict = new Dictionary<ServiceKey, object>();
        private readonly List<Tuple<ServiceKey, object>> _servicesList = new List<Tuple<ServiceKey, object>>();
        private SimpleContext _parentContext;

        private HashSet<IContextInitializable> _initializedServices = new();

        public void AddServiceToRegister<T>(T service, string tag = null)
        {
            if (service == null)
                throw new ArgumentNullException("Argument can not be null");

            var key = new ServiceKey(typeof(T), tag);
            var tuple = new Tuple<ServiceKey, object>(key, service);
            _servicesList.Add(tuple);
        }

        public void Register(IContext parentContext = null)
        {
            foreach (var tuple in _servicesList)
            {
                var service = tuple.Item2;
                _servicesDict.Add(tuple.Item1, service);
            }

            if (parentContext != null)
            {
                if (!(parentContext is SimpleContext))
                    throw new Exception("Parent context is not SimpleContext");

                var parentSimpleContext = (SimpleContext) parentContext;

                foreach (var kvpFromParent in parentSimpleContext._servicesDict)
                    if (!_servicesDict.ContainsKey(kvpFromParent.Key))
                        _servicesDict.Add(kvpFromParent.Key, kvpFromParent.Value);

                _parentContext = parentSimpleContext;
            }

            foreach (var tuple in _servicesList)
                if (tuple.Item2 is IContextInitializable initializable)
                {
                    if (!_initializedServices.Contains(initializable))
                    {
                        initializable.InitializeByContext(this);
                        _initializedServices.Add(initializable);
                    }
                }
        }

        public void Release()
        {
            foreach (var tuple in _servicesList)
                if (tuple.Item2 is IContextReleasable releasable)
                    releasable.ReleaseByContext(this);

            _servicesList.Clear();
            _servicesDict.Clear();

            _parentContext = null;
        }

        public bool Contains<T>(string tag = null)
        {
            var type = typeof(T);
            var serviceKey = new ServiceKey(type, tag);
            return _servicesDict.ContainsKey(serviceKey);
        }

        public T Resolve<T>(string tag = null)
        {
            var type = typeof(T);
            var serviceKey = new ServiceKey(type, tag);

            if (_servicesDict.TryGetValue(serviceKey, out var service))
                return (T) service;

            throw new Exception($"Service of type {type.Name} with tag {tag} not found in the context");
        }
    }
}