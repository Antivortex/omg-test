using System;

namespace Core.DI
{
    public class ServiceKey
    {
        public readonly Type Type;
        public readonly string Tag;

        public ServiceKey(Type type, string tag)
        {
            Type = type;
            Tag = tag;
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceKey key && key.Type == Type && key.Tag == Tag;
        }

        public override int GetHashCode()
        {
            return 17 * Type.GetHashCode() + (Tag == null ? 0 : Tag.GetHashCode());
        }
    }
}