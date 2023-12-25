using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace GetAttribute.Editor
{
    [CustomPropertyDrawer(typeof(GetInObjectAttribute))]
    public class GetInObjectAttributeDrawer : GetterAtributeDrawer
    {
        protected override List<Component> GetComponent(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component>();

            foreach (var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponent(type));
            }

            return result;
        }

        protected override List<Component[]> GetArrayOfComponents(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component[]>();

            foreach (var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponents(type));
            }

            return result;
        }
    }
}
