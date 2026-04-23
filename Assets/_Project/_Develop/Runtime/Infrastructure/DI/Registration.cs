using System;

namespace Assets.Project._Develop.Runtime.Infrastructure.DI
{
    public class Registration : IRegistrationOptions
    {
        private Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public bool IsNonLazy { get; private set; }

        public Registration(Func<DIContainer, object> creator) => _creator = creator;

        public object CreateInstanceFrom(DIContainer conteiner)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Creator or Instance is not exist!");

            _cachedInstance = _creator.Invoke(conteiner);

            return _cachedInstance;
        }

        public void NonLazy() => IsNonLazy = true;
    }
}