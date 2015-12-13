using UnityEngine;
using System.Collections;

public class Play_Button : MonoBehaviour {

    public void OnClick() {
        GameManager.Instance.SwitchScene(GameManager.Scenes.training);
    }
}
