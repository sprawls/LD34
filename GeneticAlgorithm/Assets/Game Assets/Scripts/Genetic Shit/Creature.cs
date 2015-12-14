using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature : MonoBehaviour {

    [Header("Public Attributes")]
    public float travelledDistance;
    public float maxTravelledDistance;

    [Header("References & Components")]
    public GameObject CreaturePart;
    private TextMesh _distTextMesh;


    [Header("Genetic Attributres")]
    public CreatureData creatureData;


    
    //List of parts
    [Header("Parts : ")]
    [SerializeField]
    private List<CreaturePart> creatureParts = new List<CreaturePart>();
    [SerializeField]
    private List<HingeJoint2D> creatureMotors = new List<HingeJoint2D>();

    //private var
    private Vector3 startPosition;
    private float maxForce = 50f;
    private float variationPerSec = 15f;


    void Awake() {
        _distTextMesh = gameObject.GetComponentInChildren<TextMesh>();
        gameObject.tag = "Creature";
    }

	

    /// <summary>
    /// Spawn the creature.
    /// </summary>
    void Initialize() {
        SpawnPart(0);
        for (int i = 1; i < creatureData.genAmountParts; i++) {
            SpawnPart(i);
        }
        for (int i = 0; i < creatureData.genAmountMotors; i++) {
            SpawnMotor(i, (int)creatureData.genMotorPosition[i].x, (int)creatureData.genMotorPosition[i].y);
        }
    }

    /// <summary>
    /// Spawns a creature part
    /// </summary>
    /// <param name="index"></param>
    void SpawnPart(int index) {
        creatureParts.Add(((GameObject)Instantiate(CreaturePart, transform.position, Quaternion.identity)).GetComponentInChildren<CreaturePart>());
        creatureParts[index].transform.parent = transform;
        creatureParts[index].transform.localPosition = creatureData.genPositions[index];
        creatureParts[index].transform.localScale = new Vector3(creatureData.genSizes[index].x, creatureData.genSizes[index].y, Random.Range(0.9f, 1.1f));

        //Get Random Color
        creatureParts[index].GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    /// <summary>
    /// Spawns a Motor
    /// </summary>
    /// <param name="index"></param>
    void SpawnMotor(int index, int motorBodyIndex, int connetecBodyIndex) {
        creatureMotors.Add((HingeJoint2D)creatureParts[motorBodyIndex].gameObject.AddComponent<HingeJoint2D>());
        //Joint Angle Limits
        JointAngleLimits2D jointAngleLimit = new JointAngleLimits2D();
        jointAngleLimit.min = -creatureData.genMaxRot[index];
        jointAngleLimit.max = creatureData.genMaxRot[index];
        creatureMotors[index].limits = jointAngleLimit;

        //Motor Position
        creatureMotors[index].anchor = creatureData.genMotorOffsets[index];
        creatureMotors[index].connectedAnchor = -creatureData.genMotorOffsets[index];

        //Debug.Log("connetecBodyIndex :" + connetecBodyIndex + "  and index : " + index);
        //Debug.Log("connetecBody :" + creatureParts[connetecBodyIndex] + "  and indexMotor : " + creatureMotors[index]);
        //Motor's Connected rigidbody
        creatureMotors[index].connectedBody = creatureParts[connetecBodyIndex].GetComponentInChildren<Rigidbody2D>();
        creatureMotors[index].useMotor = true;
        VaryMotorSpeed varyMotorSpeedScript = creatureMotors[index].gameObject.AddComponent<VaryMotorSpeed>();
        varyMotorSpeedScript.incrementing = creatureData.genStartRotDirection[index];
        //Calculate Variation per seconds based on str
        float currentVariationPerSec = Mathf.Lerp(GameManager.Instance.player.StrVariationLimits[0], GameManager.Instance.player.StrVariationLimits[1], (float)GameManager.Instance.player.Strength / 100f);
        varyMotorSpeedScript.variationPerSec = creatureData.genMotorVarPerSec[index] * currentVariationPerSec;
    }


    /// <summary>
    /// Create a new creature of Generation 1
    /// </summary>
    public void GenerateNewGenerationOne() {
        ResetCreature();
        RandomizeGeneticAttributes();
        Initialize();
    }

    /// <summary>
    /// Regenerate creature with the same data
    /// </summary>
    public void GenerateWithSameData() {
        ResetCreature();
        Initialize();
    }

    /// <summary>
    /// Creates a new creature from Data
    /// </summary>
    /// <param name="nData">Data to create creature with</param>
    public void GenerateFromData(CreatureData nData){
        ResetCreature();
        creatureData = nData;
        Initialize();
    }

    /// <summary>
    /// Reset The Creature
    /// </summary>
    private void ResetCreature() {
        for (int i = 0; i < creatureParts.Count; i++) {
            Destroy(creatureParts[i].gameObject);
        }
        creatureParts = new List<CreaturePart>();
        creatureMotors = new List<HingeJoint2D>();
    }


    /// <summary>
    /// Randomize the genetic atttributes like it is the first generation.
    /// </summary>
    void RandomizeGeneticAttributes() {
        creatureData = CreatureData.GetRandom();
    }

    public void StartTrackingDistance() {
        StopCoroutine("CalculateDistance");
        maxTravelledDistance = 0;
        startPosition = GetPosition();
        StartCoroutine("CalculateDistance");
    }
    
    /// <summary>
    /// Get Our Creature's position by calculating the median position of all creature's parts
    /// </summary>
    /// <returns> Vector3 reprensenting creature's position</returns>
    private Vector3 GetPosition() {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < creatureData.genAmountParts; i++) {
            position += creatureParts[i].transform.position;
        }
        position /= creatureData.genAmountParts;

        return position;
    }

    /// <summary>
    /// Deactivates the motors
    /// </summary>
    public void DeactivateMotors() {
        foreach (HingeJoint2D motor in creatureMotors) {
            motor.useMotor = false;
            VaryMotorSpeed varyMotor = motor.GetComponent<VaryMotorSpeed>();
            if (varyMotor != null) varyMotor.isOn = false;
        }
    }
    /// <summary>
    /// Activates the motors
    /// </summary>
    public void ActivateMotors() {
        foreach (HingeJoint2D motor in creatureMotors) {
            motor.useMotor = false;
            VaryMotorSpeed varyMotor = motor.GetComponent<VaryMotorSpeed>();
            if (varyMotor != null) varyMotor.isOn = true;
        }
    }

    IEnumerator CalculateDistance() {
        while (true) {
            //Get position
            Vector3 curPos = GetPosition();
            travelledDistance = Mathf.Abs((curPos - startPosition).magnitude);
            //Check max
            if (travelledDistance > maxTravelledDistance) maxTravelledDistance = travelledDistance;
            //Position Text Mesh
            if (_distTextMesh != null) {
                _distTextMesh.transform.position = curPos + new Vector3(0, 3, 0);
                _distTextMesh.text = "Current : " + travelledDistance.ToString("F2") + "   Max : " + maxTravelledDistance.ToString("F2");
            } else Debug.Log("WARNING : TEXT MESH IS NULL IN CREATURE");
            
            yield return new WaitForSeconds(0.05f);
        }
    }


}
