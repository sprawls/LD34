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

    
    //List of parts
    [Header("Parts : ")]
    [SerializeField]
    private List<CreaturePart> creatureParts = new List<CreaturePart>();
    [SerializeField]
    private List<HingeJoint2D> creatureMotors = new List<HingeJoint2D>();

    //private var
    private Vector3 startPosition;


    void Awake() {
        _distTextMesh = gameObject.GetComponentInChildren<TextMesh>();
    }

	void Start () {
        RandomizeGeneticAttributes();
        Initialize();
        StartTrackingDistance();
	}
	

    /// <summary>
    /// Spawn the creature.
    /// </summary>
    void Initialize() {
        SpawnPart(0);
        for (int i = 1; i < genAmountParts; i++) {
            SpawnPart(i);
        }
        for (int i = 0; i < genAmountMotors; i++) {
            SpawnMotor(i, (int)genMotorPosition[i].x, (int)genMotorPosition[i].y);
        }
    }

    /// <summary>
    /// Spawns a creature part
    /// </summary>
    /// <param name="index"></param>
    void SpawnPart(int index) {
        creatureParts.Add(((GameObject)Instantiate(CreaturePart, transform.position, Quaternion.identity)).GetComponentInChildren<CreaturePart>());
        creatureParts[index].transform.parent = transform;
        creatureParts[index].transform.localPosition = genPositions[index];
        creatureParts[index].transform.localScale = new Vector3(genSizes[index].x,genSizes[index].y, Random.Range(0.9f,1.1f));

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
        jointAngleLimit.min = -genMaxRot[index];
        jointAngleLimit.max = genMaxRot[index];
        creatureMotors[index].limits = jointAngleLimit;

        //Motor Position
        creatureMotors[index].anchor = genMotorOffsets[index];
        creatureMotors[index].connectedAnchor = -genMotorOffsets[index];

        //Debug.Log("connetecBodyIndex :" + connetecBodyIndex + "  and index : " + index);
        //Debug.Log("connetecBody :" + creatureParts[connetecBodyIndex] + "  and indexMotor : " + creatureMotors[index]);
        //Motor's Connected rigidbody
        creatureMotors[index].connectedBody = creatureParts[connetecBodyIndex].GetComponentInChildren<Rigidbody2D>();
        creatureMotors[index].useMotor = true;
        VaryMotorSpeed varyMotorSpeedScript = creatureMotors[index].gameObject.AddComponent<VaryMotorSpeed>();
        varyMotorSpeedScript.incrementing = (Random.Range(0f, 1f) > 0.5f) ? varyMotorSpeedScript.incrementing = true : varyMotorSpeedScript.incrementing = false;
    }

    /// <summary>
    /// Randomize the genetic atttributes like it is the first generation.
    /// </summary>
    public void RandomizeGeneticAttributes() {
        //Part & Motors Amount
        genAmountParts = Random.Range(2,5);
        genAmountMotors = genAmountParts - 1;

        //Position of creature parts
        genPositions = new List<Vector2>();
        for (int i = 0; i < genAmountParts; i++) {
            float randX = Random.Range(-1, 1);
            float randY = Random.Range(-1, 1);
            genPositions.Add(new Vector2(randX, randY));
        }
        //Size of creature parts
        genSizes = new List<Vector2>();
        for (int i = 0; i < genAmountParts; i++) {
            float randX = Random.Range(0.5f, 3f);
            float randY = Random.Range(0.5f, 3f);
            genSizes.Add(new Vector2(randX, randY));
        }

        //Motors offsets 
        genMotorOffsets = new List<Vector2>();
        for (int i = 0; i < genAmountMotors; i++) {
            int pos = Random.Range(0, 4);
            switch (pos) {
                case 0:
                    genMotorOffsets.Add(new Vector2(0.5f, 0));
                    break;
                case 1:
                    genMotorOffsets.Add(new Vector2(-0.5f, 0));
                    break;
                case 2:
                    genMotorOffsets.Add(new Vector2(0, 0.5f));
                    break;
                case 3:
                    genMotorOffsets.Add(new Vector2(0, -0.5f));
                    break;
            }
        }
        //Motor's connection
        genMotorPosition = new List<Vector2>();
        for (int i = 0; i < genAmountMotors; i++) {
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
            genMotorPosition.Add(new Vector2(motorIndex, connectorIndex));
        }
        //Motor max rotation
        genMaxRot = new List<float>();
        for (int i = 0; i < genAmountMotors; i++) {
            float randomRot = Random.Range(250, 500);
            genMaxRot.Add(randomRot);
        }
        //Motor start rotation
        genStartRot = new List<float>();
        for (int i = 0; i < genAmountMotors; i++) {
            float randomStartRot = Random.Range(-genMaxRot[i], genMaxRot[i]);
            genStartRot.Add(randomStartRot);
        }

    }

    public void StartTrackingDistance() {
        StopCoroutine("CalculateDistance");
        startPosition = GetPosition();
        StartCoroutine("CalculateDistance");
    }
    
    /// <summary>
    /// Get Our Creature's position by calculating the median position of all creature's parts
    /// </summary>
    /// <returns> Vector3 reprensenting creature's position</returns>
    private Vector3 GetPosition() {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < genAmountParts; i++) {
            position += creatureParts[i].transform.position;
        }
        position /= genAmountParts;

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
