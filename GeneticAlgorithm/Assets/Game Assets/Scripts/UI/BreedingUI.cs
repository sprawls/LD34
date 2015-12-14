using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BreedingUI : MonoBehaviour {

    [Header("Component")]
    [SerializeField]
    private WaitingIndicator waitIndicator;

    [SerializeField]
    private CanvasGroup trainingUIRef;

    [SerializeField]
    private Button confirmButton;

    [SerializeField]
    private CanvasGroup breedingPartnerRef;

    [SerializeField]
    private Button generateChildrenButton;

    [SerializeField]
    private GeneticLab[] geneticLab;

    private int clickedPartnerIndex = -1;
    private CreatureData chosenPartnerData;
    private Button[] buttons;

    //Keep track of Breeding wait :
    private int partnersLeft = 0;
    private bool waitingForPlayerChild = false;

    void Start() {
        buttons = breedingPartnerRef.GetComponentsInChildren<Button>();
    }
    

    public void GenerateBreedingPartner() { 
        waitIndicator.StartWaiting("Generating Partners");
        trainingUIRef.interactable = false;
        partnersLeft = 5;
        StartCoroutine(WaitForBreedingPartners());
    }

    private void GenerateChild() { 
        trainingUIRef.interactable = false;
        waitIndicator.StartWaiting("Generating Child");
        waitingForPlayerChild = true;
        geneticLab[0].Mix(GameManager.Instance.player.currentMonster.data, chosenPartnerData, this, 30);
        StartCoroutine(WaitForChild());
    }

    public void BatchTestOver(CreatureData chosenChild) {
        if (partnersLeft > 0) {
            partnersLeft--;
            GameManager.Instance.genesPool[partnersLeft] = chosenChild;
        } else if (waitingForPlayerChild) {
            waitingForPlayerChild = false;
            GameManager.Instance.player.ChangeCreature(chosenChild, chosenPartnerData);
            //Updates all player model 
            SpawnPlayerCreature[] spawnCreatures = (SpawnPlayerCreature[])FindObjectsOfType(typeof(SpawnPlayerCreature));
            foreach (SpawnPlayerCreature spc in spawnCreatures) {
                spc.RespawnPlayerCreature();
            }

        }
        
    }

    IEnumerator WaitForChild() {
        while (waitingForPlayerChild) {
            yield return null;
        }

        waitIndicator.StopWaiting();
        trainingUIRef.interactable = true;
    }

    IEnumerator WaitForBreedingPartners() {
        for (int i = 0; i < 5; i++) {
            int otherI = (int)Random.Range(0, 5);
            geneticLab[i].Mix(GameManager.Instance.genesPool[i], GameManager.Instance.genesPool[i], this, 10);
            Debug.Log("Mixing " + i + " :  " + GameManager.Instance.genesPool[i] + " and " + GameManager.Instance.genesPool[i]);
            yield return new WaitForSeconds(1f);
        }

        while (partnersLeft > 0) {
            yield return null;
        }
       
        waitIndicator.StopWaiting();
        trainingUIRef.interactable = true;
        breedingPartnerRef.interactable = true;
    }

    public void OnClick_Confirm() {
        confirmButton.interactable = false;
        clickedPartnerIndex = -1;
        breedingPartnerRef.interactable = false;
        foreach (Button button in breedingPartnerRef.GetComponentsInChildren<Button>()) {
            button.interactable = true;
        }
        //TODO tell the genetic lab that the clickedPartnerIndex partner is the right one
        GenerateChild();
    }

    public void OnClick_Partner(int index) {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = i != index;
        }
        clickedPartnerIndex = index;
        chosenPartnerData = GameManager.Instance.genesPool[index];
        confirmButton.interactable = true;
    }
}
