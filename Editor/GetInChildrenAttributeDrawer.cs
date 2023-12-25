using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace GetAttribute.Editor
{
    [CustomPropertyDrawer(typeof(GetInChildrenAttribute))]
    public class GetInChildrenAttributeDrawer : GetterAtributeDrawer
    {
        protected override List<Component> GetComponent(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component>();
            foreach(var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponentInChildren(type, includeInactive));
            }

            return result;
        }

        protected override List<Component[]> GetArrayOfComponents(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component[]>();

            foreach (var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponentsInChildren(type, includeInactive));
            }

            return result;
        }
    }
}
