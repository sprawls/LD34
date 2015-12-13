using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The GameObject requires a RigidBody component
[RequireComponent (typeof (CreatureMixer))]
public class GeneticLab : MonoBehaviour {

    public CreatureData father;
    public CreatureData mother;
    public CreatureData child;

    private CreatureMixer mixer;

    void Awake() {
        mixer = gameObject.GetComponent<CreatureMixer>();
    }

    /// <summary>
    /// Used for debug, fill father and mother from creature found in scene
    /// </summary>
    public void DEBUG_GetParentsFromScene() {
        GameObject[] sceneCreaturesGO = GameObject.FindGameObjectsWithTag("Creature");
        List<Creature> sceneCreatures = new List<Creature>();
        foreach (GameObject GO in sceneCreaturesGO) sceneCreatures.Add(GO.GetComponentInChildren<Creature>());

        if (sceneCreatures.Count > 1) {
            father = sceneCreatures[0].creatureData;
            mother = sceneCreatures[1].creatureData;
            Debug.Log("father: " + father + "   mother: " + mother);
        }
    }

    /// <summary>
    /// Mixes with assigned father and mother
    /// </summary>
    public void Mix() {
        Mix(father, mother);
    }

    /// <summary>
    /// Mixes with given father and mother
    /// </summary>
    /// <param name="father"></param>
    /// <param name="mother"></param>
    public void Mix(CreatureData father, CreatureData mother) {
        Mix(father, mother, this, 20);
    }
    public void Mix(CreatureData father, CreatureData mother, MonoBehaviour mono) {
        Mix(father, mother, mono, 20);
        Debug.Log("Reproduction Mixer Called !");
    }
    public void Mix(CreatureData father, CreatureData mother, MonoBehaviour mono, int amtChilds) {
        mixer.Reproduce(father, mother, mono, amtChilds);
        Debug.Log("Reproduction Mixer Called !");
    }

    /// <summary>
    /// Method called once Mixing is completed
    /// </summary>
    /// <param name="chosenChild"></param>
    public void BatchTestOver(CreatureData chosenChild) {
        Debug.Log("Reproduction callback received !");
        child = chosenChild;

    }
}
