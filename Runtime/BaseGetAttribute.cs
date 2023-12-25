using UnityEngine;

namespace GetAttribute
{
    public abstract class BaseGetAttribute : PropertyAttribute
    {
        public readonly bool IncludeInactive;

        public BaseGetAttribute(bool includeInactive = false)
        {
            IncludeInactive = includeInactive;
        }
    }
}
