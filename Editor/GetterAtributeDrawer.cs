using GetAttribute.Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GetAttribute.Editor
{
    public abstract class GetterAtributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (Application.isPlaying) return;
            if (property.objectReferenceValue) return;

            var type = property.GetFieldType();
            if (type == null) return;
            
            var includeInactive = (attribute as BaseGetAttribute).IncludeInactive;

            if (!type.IsSubclassOf(typeof(Component))) return;

            //arrays not yet supported :(
            if (property.isArray)
            {
                var result = GetArrayOfComponents(type, property.GetMonoBehaviours(), includeInactive);
                property.SetArrayObjectValue(result);
            }
            else
            {
                var result = GetComponent(type, property.GetMonoBehaviours(), includeInactive);
                property.SetObjectValue(result);
            }
        }

        protected abstract List<Component> GetComponent(System.Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive);
        protected abstract List<Component[]> GetArrayOfComponents(System.Type type, IEnumerable<MonoBehaviour> selectedMonoBehaviours, bool includeInactive);
    } 
}

