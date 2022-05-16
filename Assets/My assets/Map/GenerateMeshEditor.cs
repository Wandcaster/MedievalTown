//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(GenerateMesh))]
//public class GenerateMeshEditor : Editor
//{
//    float scale = 0;
//    float lastScale = 0;
//    private bool mapIsGenerated = false;
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        GenerateMesh myScript = (GenerateMesh)target;

//        if (GUILayout.Button("GenerateMapFromData"))
//        {
//            myScript.GenerateMapFromData();
//        }
//        if (GUILayout.Button("SetPlayerPosition"))
//        {
//            //myScript.SetObjectPosition();
//        }
//        if (GUILayout.Button("GenerateMap"))
//        {
//            myScript.FinalGenerateMap();
//        }
//        if (GUILayout.Button("MapIsGenerated"))
//        {
//            mapIsGenerated = true;
//        }


//        scale = EditorGUILayout.Slider(scale, 2, 40);

//        if (myScript.mapIsGenerated & lastScale != scale)
//        {
//            myScript.SetDataOnIndex((int)scale);
//            lastScale = scale;
//        }
//    }
//}
