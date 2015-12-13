using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script that contains all the methods to mix two creatures to obtain a new one
/// </summary>
public class CreatureMixer : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject TestFloorPrefab;
    public GameObject CreaturePrefab;

    [Header("References")]
    public GameObject TestingArea;

    [SerializeField]
    private List<CreatureData> batchToTest = new List<CreatureData>();

    private int amountOfChilds = 15;
    private float fatherInfluence = 0.75f; //Father's influence out of 1. FatherInfluence + MotherInfluence = 1;
    private float motherInfluence; //Mother's influence out of 1. MotherInfluence = (1-fatherInfluence)
    private float randomizationFactor = 0.2f; //Randomization added out of 1 on the found values for most parameters. (0.5 = 50% randomization);
    private float simulationTime = 10f;

    void Awake() {
        if (TestingArea == null) TestingArea = GameObject.FindGameObjectWithTag("TestingArea");
    }

    void Start() {
        motherInfluence = 1f - fatherInfluence;
    }

    /// <summary>
    /// Rerpoduce two CreatureData to get a new one by simulating their children and picking out the best one
    /// </summary>
    /// <param name="father">Player's creature data</param>
    /// <param name="mother">Other creature data</param>
    /// <param name="monoToCallback">Monobehaviour that will be called back after reproduction is over</param>
    /// <returns>Chosen child creature data</returns>
    public void Reproduce(CreatureData father, CreatureData mother, MonoBehaviour monoToCallback) {
        CreatureData chosenChild = new CreatureData();

        for (int childI = 0; childI < amountOfChilds; childI++) {
            CreatureData child = Mix(father, mother);
            batchToTest.Add(child);
        }

        StartCoroutine(StartBatchTest(simulationTime, monoToCallback));
    }

    /// <summary>
    /// Mix two Creature's data together to form a new Creature Data
    /// </summary>
    /// <param name="father">Player's creature data</param>
    /// <param name="mother">Other creature data</param>
    /// <returns>Randomly generated child from parents </returns>
    private CreatureData Mix(CreatureData father, CreatureData mother) {
        CreatureData chosenChild = new CreatureData();

        //Amount Parts
        float n_amountParts = (father.genAmountParts+mother.genAmountParts)/2f;
        chosenChild.genAmountParts = (int) (n_amountParts + 0.5f + Random.Range(-1f,1f));
        chosenChild.genAmountMotors = chosenChild.genAmountParts - 1;
        //Part position
        chosenChild.genPositions = new List<Vector2>();
        for (int i = 0; i < chosenChild.genAmountParts; i++) {
            float randX = Random.Range(-1, 1);
            float randY = Random.Range(-1, 1);
            chosenChild.genPositions.Add(new Vector2(randX, randY));
        }
        //Size of creature parts
        chosenChild.genSizes = new List<Vector2>();
        for (int i = 0; i < chosenChild.genAmountParts; i++) {
            Vector2 partSize = Vector3.one;
            int randomFatherIndex = Random.Range(0, father.genSizes.Count); //random Index used if father's index does not exist
            int randomMotherIndex = Random.Range(0, mother.genSizes.Count); //random Index used if mother's index does not exist

            if(father.genSizes.Count > i && mother.genSizes.Count > i ){
                partSize = (((father.genSizes[i] * fatherInfluence) + (mother.genSizes[i] * motherInfluence)) / 2f);
            } else if (father.genSizes.Count > i) {
                partSize = (((father.genSizes[i] * fatherInfluence) + (mother.genSizes[randomMotherIndex] * motherInfluence)) / 2f);
            } else if (mother.genSizes.Count > i) {
                partSize = (((father.genSizes[randomFatherIndex] * fatherInfluence) + (mother.genSizes[i] * motherInfluence)) / 2f);
            } else {
                partSize = (((father.genSizes[randomFatherIndex] * fatherInfluence) + (mother.genSizes[randomMotherIndex] * motherInfluence)) / 2f);
            }

            partSize += new Vector2(Random.Range(-partSize.x * randomizationFactor, partSize.x * randomizationFactor),
                                    Random.Range(-partSize.y * randomizationFactor, partSize.y * randomizationFactor));
            chosenChild.genSizes.Add(partSize);
        }

        //Motors offsets 
        chosenChild.genMotorOffsets = new List<Vector2>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {

            if(father.genMotorOffsets.Count > i && mother.genMotorOffsets.Count > i ){
                bool useFather = Random.Range(0f, 1f) < fatherInfluence ? true : false;
                if(useFather) chosenChild.genMotorOffsets.Add(father.genMotorOffsets[i]);
                else chosenChild.genMotorOffsets.Add(mother.genMotorOffsets[i]);
            } else if (father.genMotorOffsets.Count > i) {
                chosenChild.genMotorOffsets.Add(father.genMotorOffsets[i]);
            } else if (mother.genMotorOffsets.Count > i) {
                chosenChild.genMotorOffsets.Add(mother.genMotorOffsets[i]);
            } else {
                int pos = Random.Range(0, 4);
                switch (pos) {
                case 0:
                    chosenChild.genMotorOffsets.Add(new Vector2(0.5f, 0));
                    break;
                case 1:
                    chosenChild.genMotorOffsets.Add(new Vector2(-0.5f, 0));
                    break;
                case 2:
                    chosenChild.genMotorOffsets.Add(new Vector2(0, 0.5f));
                    break;
                case 3:
                    chosenChild.genMotorOffsets.Add(new Vector2(0, -0.5f));
                    break;
                }
            }

            
        }
        //Motor's connection
        chosenChild.genMotorPosition = new List<Vector2>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {
            //Linear Connections
            int motorIndex = i;
            int connectorIndex = motorIndex + 1;
            chosenChild.genMotorPosition.Add(new Vector2(motorIndex, connectorIndex));
        }
        //Motor max rotation
        chosenChild.genMaxRot = new List<float>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {
            float maxRot = 360f;
            int randomFatherIndex = Random.Range(0, father.genMaxRot.Count); //random Index used if father's index does not exist
            int randomMotherIndex = Random.Range(0, mother.genMaxRot.Count); //random Index used if mother's index does not exist

            if (father.genMaxRot.Count > i && mother.genMaxRot.Count > i) {
                maxRot = (((father.genMaxRot[i] * fatherInfluence) + (mother.genMaxRot[i] * motherInfluence)) / 2f);
            } else if (father.genMaxRot.Count > i) {
                maxRot = (((father.genMaxRot[i] * fatherInfluence) + (mother.genMaxRot[randomMotherIndex] * motherInfluence)) / 2f);
            } else if (mother.genMaxRot.Count > i) {
                maxRot = (((father.genMaxRot[randomFatherIndex] * fatherInfluence) + (mother.genMaxRot[i] * motherInfluence)) / 2f);
            } else {
                maxRot = (((father.genMaxRot[randomFatherIndex] * fatherInfluence) + (mother.genMaxRot[randomMotherIndex] * motherInfluence)) / 2f);
            }

            maxRot += Random.Range(-maxRot * randomizationFactor, maxRot * randomizationFactor);
            chosenChild.genMaxRot.Add(maxRot);
        }
        //Motor start rotation
        chosenChild.genStartRot = new List<float>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {
            float startRot = 360f;
            int randomFatherIndex = Random.Range(0, father.genStartRot.Count); //random Index used if father's index does not exist
            int randomMotherIndex = Random.Range(0, mother.genStartRot.Count); //random Index used if mother's index does not exist

            if (father.genStartRot.Count > i && mother.genStartRot.Count > i) {
                startRot = (((father.genStartRot[i] * fatherInfluence) + (mother.genStartRot[i] * motherInfluence)) / 2f);
            } else if (father.genStartRot.Count > i) {
                startRot = (((father.genStartRot[i] * fatherInfluence) + (mother.genStartRot[randomMotherIndex] * motherInfluence)) / 2f);
            } else if (mother.genStartRot.Count > i) {
                startRot = (((father.genStartRot[randomFatherIndex] * fatherInfluence) + (mother.genStartRot[i] * motherInfluence)) / 2f);
            } else {
                startRot = (((father.genStartRot[randomFatherIndex] * fatherInfluence) + (mother.genStartRot[randomMotherIndex] * motherInfluence)) / 2f);
            }

            startRot += Random.Range(-startRot * randomizationFactor, startRot * randomizationFactor);
            chosenChild.genStartRot.Add(startRot);
        }

        //Motor rotation start direction
        chosenChild.genStartRotDirection = new List<bool>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {
            if (father.genStartRotDirection.Count > i && mother.genStartRotDirection.Count > i) {
                bool useFather = Random.Range(0f, 1f) < fatherInfluence ? true : false;
                if (useFather) chosenChild.genStartRotDirection.Add(father.genStartRotDirection[i]);
                else chosenChild.genStartRotDirection.Add(mother.genStartRotDirection[i]);
            } else if (father.genStartRotDirection.Count > i) {
                chosenChild.genStartRotDirection.Add(father.genStartRotDirection[i]);
            } else if (mother.genStartRotDirection.Count > i) {
                chosenChild.genStartRotDirection.Add(mother.genStartRotDirection[i]);
            } else {
                bool randomStartRot = Random.Range(0f, 1f) < 0.5f ? true : false;
                chosenChild.genStartRotDirection.Add(randomStartRot);
            }
        }

        //Motor variation per seconds
        chosenChild.genMotorVarPerSec = new List<float>();
        for (int i = 0; i < chosenChild.genAmountMotors; i++) {
            float vps = 360f;
            int randomFatherIndex = Random.Range(0, father.genMotorVarPerSec.Count); //random Index used if father's index does not exist
            int randomMotherIndex = Random.Range(0, mother.genMotorVarPerSec.Count); //random Index used if mother's index does not exist

            if (father.genMotorVarPerSec.Count > i && mother.genMotorVarPerSec.Count > i) {
                vps = (((father.genMotorVarPerSec[i] * fatherInfluence) + (mother.genMotorVarPerSec[i] * motherInfluence)) / 2f);
            } else if (father.genMotorVarPerSec.Count > i) {
                vps = (((father.genMotorVarPerSec[i] * fatherInfluence) + (mother.genMotorVarPerSec[randomMotherIndex] * motherInfluence)) / 2f);
            } else if (mother.genMotorVarPerSec.Count > i) {
                vps = (((father.genMotorVarPerSec[randomFatherIndex] * fatherInfluence) + (mother.genMotorVarPerSec[i] * motherInfluence)) / 2f);
            } else {
                vps = (((father.genMotorVarPerSec[randomFatherIndex] * fatherInfluence) + (mother.genMotorVarPerSec[randomMotherIndex] * motherInfluence)) / 2f);
            }

            vps += Random.Range(-vps * randomizationFactor, vps * randomizationFactor);
            chosenChild.genMotorVarPerSec.Add(vps);
        }

        return chosenChild;
    }

    private IEnumerator StartBatchTest(float testTime, MonoBehaviour MonoToCallback) {
        float BestPerformance = 0;
        int BestCreatureIndex = 0;
        CreatureData bestData;
        List<Creature> testingCreatures = new List<Creature>();
        List<GameObject> testingFloors = new List<GameObject>();

        //instantiate creatures to test
        for (int i = 0; i < batchToTest.Count; i++) {
            GameObject floorGO = (GameObject)Instantiate(TestFloorPrefab);
            floorGO.transform.parent = TestingArea.transform;
            floorGO.transform.localPosition = new Vector3(0, 0, i*2.5f + 5f);
            testingFloors.Add(floorGO);

            Creature creatureGO = ((GameObject)Instantiate(CreaturePrefab,testingFloors[i].transform.position + new Vector3(0,2.5f,0), Quaternion.identity)).GetComponent<Creature>();
            creatureGO.GenerateFromData(batchToTest[i]);
            testingCreatures.Add(creatureGO);
        }


        
        yield return new WaitForSeconds(1f);
        foreach (Creature c in testingCreatures) {
            c.StartTrackingDistance();
        }

        yield return new WaitForSeconds(testTime); //Let Them test out a little
        //Get Best Creature and return its data
        for(int i =0 ; i<testingCreatures.Count; i++) {
            float performance = testingCreatures[i].maxTravelledDistance;
            if (performance > BestPerformance) {
                BestPerformance = performance;
                BestCreatureIndex = i;
            }
            Destroy(testingCreatures[i].gameObject);
        }
        bestData = batchToTest[BestCreatureIndex];
        testingCreatures = new List<Creature>();
        batchToTest = new List<CreatureData>();

        MonoToCallback.SendMessage("BatchTestOver", bestData);
      
    }


}
