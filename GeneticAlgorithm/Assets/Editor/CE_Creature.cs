using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Creature))]
public class CE_Creature : Editor {

    public class ObjectBuilderEditor : Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            Creature creature = (Creature)target;

            if (GUILayout.Button("Randomize Genetic Values")) {
                creature.RandomizeGeneticAttributes();
            }
            if (GUILayout.Button("Track Distance")) {
                creature.StartTrackingDistance();
            }

        }
    }

}
