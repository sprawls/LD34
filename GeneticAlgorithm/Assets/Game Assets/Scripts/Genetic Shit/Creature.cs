using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature : MonoBehaviour {

    [Header("References")]
    public GameObject CreaturePart;

    [Header("Genetic Attributres")]
    public float genAmountParts;
    public float genAmountMotors;
    //Parts
    public List<Vector2> genOffsets;
    public List<Vector2> genPositions;
    public List<Vector2> genSizes;
    //Motors
    public List<int> genMotorPosition;
    public List<float> genMaxRot;
    public List<float> genStartRot;

    
    private List<CreaturePart> creatureParts = new List<CreaturePart>();
    private List<HingeJoint2D> creatureMotors = new List<HingeJoint2D>();


	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Initialize() {
        SpawnPart(0);
        for (int i = 1; i < genAmountParts; i++) {
            SpawnPart(i);
        }
        for (int i = 0; i < genAmountMotors; i++) {
            SpawnMotor(i);
        }
    }

    void SpawnPart(int index) {
        creatureParts.Add(((GameObject)Instantiate(CreaturePart, transform.position, Quaternion.identity)).GetComponentInChildren<CreaturePart>());
        creatureParts[index].transform.parent = transform;
        creatureParts[index].transform.localPosition = genPositions[index];
        creatureParts[index].transform.localScale = genSizes[index];

    }

    void SpawnMotor(int index) {
        creatureMotors.Add((HingeJoint2D)creatureParts[index].gameObject.AddComponent<HingeJoint2D>());
        //Joint Angle Limits
        JointAngleLimits2D jointAngleLimit = new JointAngleLimits2D();
        jointAngleLimit.min = -genMaxRot[index];
        jointAngleLimit.max = genMaxRot[index];
        creatureMotors[index].limits = jointAngleLimit;

        //Motor Position
        creatureMotors[index].anchor = genOffsets[index];
        creatureMotors[index].connectedAnchor = -genOffsets[index];
    }


}
