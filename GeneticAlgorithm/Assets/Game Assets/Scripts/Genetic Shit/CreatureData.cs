using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class Containing the data representing a Creature
/// </summary>
public class CreatureData {

    public float genAmountParts; //Amount of Limbs
    public float genAmountMotors; //Amount of motors
    //Parts
    public List<Vector2> genPositions; //position of each limbs
    public List<Vector2> genSizes; //Size of each limbs
    //Motors
    public List<Vector2> genMotorOffsets; //Offset of each motors
    public List<Vector2> genMotorPosition; //Limbs attached to each motors
    public List<float> genMaxRot; //Max Rotation of each motors
    public List<float> genStartRot; //Start Rotation of each motors
    //Randomized Tweaks
    public List<float> genMotorVarPerSec; //Variation per seconds of each motors (speed)
    public List<bool> genStartRotDirection; //Start direction of each motors


    public static CreatureData GetRandom() {
        CreatureData creatureData = new CreatureData();

        //Part & Motors Amount
        creatureData.genAmountParts = Random.Range(2, 5);
        creatureData.genAmountMotors = creatureData.genAmountParts - 1;

        //Position of creature parts
        creatureData.genPositions = new List<Vector2>();
        for (int i = 0; i < creatureData.genAmountParts; i++) {
            float randX = Random.Range(-1, 1);
            float randY = Random.Range(-1, 1);
            creatureData.genPositions.Add(new Vector2(randX, randY));
        }
        //Size of creature parts
        creatureData.genSizes = new List<Vector2>();
        for (int i = 0; i < creatureData.genAmountParts; i++) {
            float randX = Random.Range(0.5f, 3f);
            float randY = Random.Range(0.5f, 3f);
            creatureData.genSizes.Add(new Vector2(randX, randY));
        }

        //Motors offsets 
        creatureData.genMotorOffsets = new List<Vector2>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            int pos = Random.Range(0, 4);
            switch (pos) {
                case 0:
                    creatureData.genMotorOffsets.Add(new Vector2(0.5f, 0));
                    break;
                case 1:
                    creatureData.genMotorOffsets.Add(new Vector2(-0.5f, 0));
                    break;
                case 2:
                    creatureData.genMotorOffsets.Add(new Vector2(0, 0.5f));
                    break;
                case 3:
                    creatureData.genMotorOffsets.Add(new Vector2(0, -0.5f));
                    break;
            }
        }
        //Motor's connection
        creatureData.genMotorPosition = new List<Vector2>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            //Randomizes connections
            /*
            int motorIndex = (int)Random.Range(0, genAmountParts);
            int connectorIndex = (Random.Range(0f,1f) > 0.5f) ? connectorIndex = motorIndex + 1 : connectorIndex = motorIndex -1;
            connectorIndex = (int)Mathf.Clamp(connectorIndex, 0, genAmountParts-1);
            while (connectorIndex == motorIndex) {
                connectorIndex = (Random.Range(0f, 1f) > 0.5f) ? connectorIndex = motorIndex + 1 : connectorIndex = motorIndex - 1;
                connectorIndex = (int)Mathf.Clamp(connectorIndex, 0, genAmountParts-1);
            }
            */
            //Linear Connections
            int motorIndex = i;
            int connectorIndex = motorIndex + 1;
            creatureData.genMotorPosition.Add(new Vector2(motorIndex, connectorIndex));
        }
        //Motor max rotation
        creatureData.genMaxRot = new List<float>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            float randomRot = Random.Range(360, 500);
            creatureData.genMaxRot.Add(randomRot);
        }
        //Motor start rotation
        creatureData.genStartRot = new List<float>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            float randomStartRot = Random.Range(-creatureData.genMaxRot[i], creatureData.genMaxRot[i]);
            creatureData.genStartRot.Add(randomStartRot);
        }
        //Motor rotation start direction
        creatureData.genStartRotDirection = new List<bool>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            bool increments = (Random.Range(0f, 1f) > 0.5f) ? true : false;
            creatureData.genStartRotDirection.Add(increments);
        }
        //Motor variation per seconds
        creatureData.genMotorVarPerSec = new List<float>();
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            float varPerSec = 1 * 0.15f;
            creatureData.genMotorVarPerSec.Add(1 + Random.Range(-varPerSec, varPerSec));
        }

        return creatureData;
    }
}
