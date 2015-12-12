using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Creature))]
public class CE_Creature : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Creature creature = (Creature)target;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        

        // UTILITY BUTTONS 
        EditorGUILayout.Separator();
        GUILayout.Label("Utility Buttons");
        EditorGUILayout.Separator();
        if (GUILayout.Button("Randomize Genetic Values")) {
            creature.GenerateNewGenerationOne();
        }
        if (GUILayout.Button("Track Distance")) {
            creature.StartTrackingDistance();
        }

    }

}
