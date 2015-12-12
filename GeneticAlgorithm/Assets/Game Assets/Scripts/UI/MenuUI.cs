using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour {

    public void OnClick_Play() {
        GameManager.Instance.SwitchScene(GameManager.Scenes.cutscene);
    }

    public void OnClick_Quit() {
        Application.Quit();
    }
}
