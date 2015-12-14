using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour {

    public static PopupUI Instance;

    public delegate void PopupUI_Delegate(bool result);
    private PopupUI_Delegate currentCallback;

    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject popup;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    void OnDestroy() {
        Instance = null;
    }

    public void Open(string message, PopupUI_Delegate callback) {
        currentCallback = callback;
        text.text = message;
        popup.SetActive(true);
    }

    public void OnClick_Yes() {
        currentCallback(true);
        Close();
    }

    public void OnClick_No() {
        currentCallback(false);
        Close();
    }

    private void Close() {
        currentCallback = null;
        popup.SetActive(false);
    }
}
