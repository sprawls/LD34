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

	void Start () {

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
        varyMotorSpeedScript.variationPerSec = creatureData.genMotorVarPerSec[index];
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
        creatureData = new CreatureData();

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
            float randomRot = Random.Range(250, 500);
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
            float varPerSec = variationPerSec * 0.15f;
            creatureData.genMotorVarPerSec.Add(variationPerSec + Random.Range(-varPerSec, varPerSec));
        }

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
