using UnityEngine;
using System.Collections;

public class UI_Switcher : MonoBehaviour {

    [Header("Component")]
    [SerializeField]
    private CameraPositionChanger camera;

    [SerializeField]
    private CanvasGroup menuUI;

    [SerializeField]
    private CanvasGroup trainingUI;

    public void Switch(bool toTraining) {
        camera.SwitchPosition(0.0001f);
        StartCoroutine(toTraining ? OpenTrainingUI() : OpenMenuUI());
    }

    public void InstantOpenMenuUI() {

    }

    public void InstantOpenTrainingUI() { 
        
    }

    IEnumerator OpenMenuUI(float timeTaken = 0.0001f) {
        float time = 0;

        trainingUI.interactable = false;
        trainingUI.blocksRaycasts = false;
        while (menuUI.alpha != 1) {
            time += Time.deltaTime;
            if (time <= timeTaken / 2 + 0.5f) trainingUI.alpha = Mathf.Lerp(1, 0, time / timeTaken/2);
            else menuUI.alpha = Mathf.Lerp(0, 1, (time - 0.5f) / timeTaken);
            yield return new WaitForEndOfFrame();
        }
        menuUI.interactable = true;
        menuUI.blocksRaycasts = true;
    }

    IEnumerator OpenTrainingUI(float timeTaken = 0.0001f) {
        float time = 0;

        menuUI.interactable = false;
        menuUI.blocksRaycasts = false;
        while (trainingUI.alpha != 1) {
            time += Time.deltaTime;
            if (time <= timeTaken / 2 + 0.5f) menuUI.alpha = Mathf.Lerp(1, 0, time / timeTaken / 2);
            else trainingUI.alpha = Mathf.Lerp(0, 1, (time - 0.5f) / timeTaken);
            yield return new WaitForEndOfFrame();
        }
        trainingUI.interactable = true;
        trainingUI.blocksRaycasts = true;
    }
}
