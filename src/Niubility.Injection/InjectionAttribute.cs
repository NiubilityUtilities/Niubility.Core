using System;

namespace Niubility.Injection
{
    public class InjectionAttribute : Attribute
    {
        public InjectionLifetimeTypes Lifetime { get; set; }
        public Type ServiceType { get; }

        public InjectionAttribute(Type serviceType, InjectionLifetimeTypes lifetime)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }
        public InjectionAttribute(Type serviceType) : this(serviceType, InjectionLifetimeTypes.Transient) { }
        public InjectionAttribute(InjectionLifetimeTypes lifetime) : this(null, lifetime) { }
        public InjectionAttribute() : this(null, InjectionLifetimeTypes.Transient) { }
    }
}