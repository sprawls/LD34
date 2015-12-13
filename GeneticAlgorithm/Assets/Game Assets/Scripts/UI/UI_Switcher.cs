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
        camera.SwitchPosition(1.5f);
        StartCoroutine(toTraining ? OpenTrainingUI() : OpenMenuUI());
    }

    IEnumerator OpenMenuUI() {
        float time = 0;

        trainingUI.interactable = false;
        trainingUI.blocksRaycasts = false;
        while (menuUI.alpha != 1) {
            time += Time.deltaTime;
            if (time <= 1.5f) trainingUI.alpha = Mathf.Lerp(1, 0, time);
            else menuUI.alpha = Mathf.Lerp(0, 1, (time - 0.5f) / 2);
            yield return new WaitForEndOfFrame();
        }
        menuUI.interactable = true;
        menuUI.blocksRaycasts = true;
    }

    IEnumerator OpenTrainingUI() {
        float time = 0;

        menuUI.interactable = false;
        menuUI.blocksRaycasts = false;
        while (trainingUI.alpha != 1) {
            time += Time.deltaTime;
            if (time <= 1.5f) menuUI.alpha = Mathf.Lerp(1, 0, time); 
            else trainingUI.alpha = Mathf.Lerp(0, 1, (time - 0.5f) / 2);
            yield return new WaitForEndOfFrame();
        }
        trainingUI.interactable = true;
        trainingUI.blocksRaycasts = true;
    }
}
