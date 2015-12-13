using UnityEngine;
using System.Collections;

public class TryToEscape_Button : MonoBehaviour {

    public void OnClick() {
        GameManager.Instance.SwitchScene(GameManager.Scenes.cutscene);
    }
}
