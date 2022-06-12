using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CanEditMultipleObjects]
[CustomEditor(typeof(PlaceController))]
public class PlaceControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(Selection.objects.Length==1)
        {
            if (GUILayout.Button("Copy transform form Object"))
            {
                PlaceController placeController = (PlaceController)target;
                Orientation item = new Orientation();
                item.position = placeController.transform.localPosition;
                item.rotation = placeController.transform.localRotation.eulerAngles;
                item.Scale = placeController.transform.localScale;
                if (placeController.Orientations == null) placeController.Orientations = new List<Orientation>();
                placeController.Orientations.Add(item);
            }
        }
    }
}
[AttributeUsage(AttributeTargets.Field)]
public class DrawIF : PropertyAttribute
{
    public string PropertyName;
    public DrawIF(string PropertyName)
    {     
        this.PropertyName = PropertyName;
    }
}
[CustomPropertyDrawer(typeof(DrawIF))]
public class RangeDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DrawIF drawIF = (DrawIF)attribute;
        SerializedProperty serializedProperty= property.serializedObject.FindProperty(drawIF.PropertyName);
        if (serializedProperty.boolValue) property.floatValue = EditorGUI.FloatField(position, label, property.floatValue);
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DrawIF drawIF = (DrawIF)attribute;
        SerializedProperty serializedProperty = property.serializedObject.FindProperty(drawIF.PropertyName);
        if(serializedProperty.boolValue)return EditorGUI.GetPropertyHeight(property, label);
        else return -EditorGUIUtility.standardVerticalSpacing;
    }



}


