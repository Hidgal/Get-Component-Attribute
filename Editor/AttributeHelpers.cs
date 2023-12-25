using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;
#endif

namespace GetAttribute.Helpers
{
    public static class AttributeHelpers
    {
#if UNITY_EDITOR
        public static Type GetFieldType(this SerializedProperty property)
        {
            if(property.isArray)
            {
                return Type.GetType(property.arrayElementType);
            }
            else
            {
                System.Type parentType = property.serializedObject.targetObject.GetType();
                FieldInfo fi = parentType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                
                if (fi == null) return null;

                return fi.FieldType;
            }

        }

        public static void SetObjectValue(this SerializedProperty property, IEnumerable<UnityEngine.Object> values)
        {
            if (values == null) return;

            //supports multi-selected objects with current cycle
            for (int i = 0; i < values.Count() && i < property.serializedObject.targetObjects.Length; i++)
            {
                if (!values.ElementAt(i)) continue;

                var serializedObject = new SerializedObject(property.serializedObject.targetObjects[i]);
                var realProperty = serializedObject.FindProperty(property.propertyPath);

                if (realProperty != null)
                {
                    realProperty.objectReferenceValue = values.ElementAt(i);
                    realProperty.serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(realProperty.serializedObject.targetObject);
                }
            }
        }


        public static void SetArrayObjectValue(this SerializedProperty property, IEnumerable<UnityEngine.Object[]> values)
        {
            if (values == null) return;

            //supports multi-selected objects with current cycle
            for (int i = 0; i < values.Count() && i < property.serializedObject.targetObjects.Length; i++)
            {
                var array = values.ElementAt(i);
                if (array == null) continue;

                var serializedObject = new SerializedObject(property.serializedObject.targetObjects[i]);
                var realProperty = serializedObject.FindProperty(property.propertyPath);

                if (realProperty != null)
                {
                    realProperty.ClearArray();
                    realProperty.arraySize = array.Length;
                    for (int j = 0; j < array.Length; j++)
                    {
                        var element = realProperty.GetArrayElementAtIndex(j);
                        element.objectReferenceValue = array[j];
                    }

                    realProperty.serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(realProperty.serializedObject.targetObject);
                }
            }
        }

        public static IEnumerable<MonoBehaviour> GetMonoBehaviours(this SerializedProperty property)
        {
            return property.serializedObject.targetObjects.Where(obj => obj is MonoBehaviour).Select(obj => obj as MonoBehaviour);
        }
#endif
    } 
}

