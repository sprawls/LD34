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

    private int clickedPartnerIndex = -1;
    private CreatureData[] partnersData;
    

    public void GenerateBreedingPartner() { //TODO start partners generation
        waitIndicator.StartWaiting("Generating Partners");
        trainingUIRef.interactable = false;
        StartCoroutine(WaitForBreedingPartners());
    }

    private void GenerateChild() { //TODO start child generation
        trainingUIRef.interactable = false;
        waitIndicator.StartWaiting("Generating Child");
        StartCoroutine(WaitForChild());
    }

    IEnumerator WaitForChild() {
        yield return new WaitForSeconds(2.5f); //TODO Replace with waiting for generation to complete
        waitIndicator.StopWaiting();
        //GameManager.Instance.player.ChangeCreature(child, partner); TODO
        trainingUIRef.interactable = true;
    }

    IEnumerator WaitForBreedingPartners() {
        yield return new WaitForSeconds(2.5f); //TODO Replace with waiting for generation to complete
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
        Button[] buttons = breedingPartnerRef.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = i != index;
        }
        clickedPartnerIndex = index;
        confirmButton.interactable = true;
    }
}
