using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GeneticLab))]
public class CE_GeneticLab : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GeneticLab lab = (GeneticLab)target;

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        // UTILITY BUTTONS 
        EditorGUILayout.Separator();
        GUILayout.Label("Utility Buttons");
        EditorGUILayout.Separator();
        if (GUILayout.Button("Generate from parents")) {
            lab.Mix();
        }
        if (GUILayout.Button("Get parents from scene")) {
            lab.DEBUG_GetParentsFromScene();
        }
        


    }
}
