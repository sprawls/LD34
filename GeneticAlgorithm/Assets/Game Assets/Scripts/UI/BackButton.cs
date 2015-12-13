using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

    public void OnClick() {
        GameManager.Instance.SwitchScene(GameManager.Scenes.mainMenu);
    }
}
