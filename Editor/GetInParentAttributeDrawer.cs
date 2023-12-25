using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace GetAttribute.Editor
{
    [CustomPropertyDrawer(typeof(GetInParentAttribute))]
    public class GetInParentAttributeDrawer : GetterAtributeDrawer
    {
        protected override List<Component> GetComponent(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component>();

            foreach (var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponentInParent(type, includeInactive));
            }

            return result;
        }

        protected override List<Component[]> GetArrayOfComponents(Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive)
        {
            var result = new List<Component[]>();

            foreach (var obj in selectedMonoBehaviours)
            {
                result.Add(obj.GetComponentsInParent(type, includeInactive));
            }

            return result;
        }
    }
}
