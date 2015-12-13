using UnityEngine;
using System.Collections;

public class BreedingUI : MonoBehaviour {

    [SerializeField]
    private WaitingIndicator waitIndicator;

    [SerializeField]
    private CanvasGroup trainingUIRef;

    public void GenerateBreedingPartner() {
        waitIndicator.StartWaiting("Generating Partners");
        trainingUIRef.interactable = false;
        StartCoroutine(WaitForBreedingPartners());
    }

    public void GenerateChild() {
        trainingUIRef.interactable = false;
        waitIndicator.StartWaiting("Generating Child");
        StartCoroutine(WaitForChild());
    }

    IEnumerator WaitForChild() {
        yield return new WaitForSeconds(2.5f);
        waitIndicator.StopWaiting();
        trainingUIRef.interactable = true;
    }

    IEnumerator WaitForBreedingPartners() {
        yield return new WaitForSeconds(2.5f);
        waitIndicator.StopWaiting();
        trainingUIRef.interactable = true;
    }
}
