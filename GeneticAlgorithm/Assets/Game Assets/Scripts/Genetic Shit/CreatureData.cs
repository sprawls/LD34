using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class Containing the data representing a Creature
/// </summary>
public class CreatureData {

    public float genAmountParts;
    public float genAmountMotors;
    //Parts
    public List<Vector2> genPositions;
    public List<Vector2> genSizes;
    //Motors
    public List<Vector2> genMotorOffsets;
    public List<Vector2> genMotorPosition;
    public List<float> genMaxRot;
    public List<float> genStartRot;
    //Randomized Tweaks
    public List<float> genMotorVarPerSec;
    public List<bool> genStartRotDirection;

}
