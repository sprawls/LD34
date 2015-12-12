using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrainingSidebarUI : MonoBehaviour {

    [Header("Component")]
    [SerializeField]
    private GameObject pagesRef;

    [SerializeField]
    private GameObject tabsRef;

    public void OpenWindow(int id) {
        CloseAllWindow();
        GameObject page = pagesRef.transform.GetChild(id).gameObject;
        if (page != null) page.SetActive(true);
    }

    private void CloseAllWindow() {
        foreach (Transform child in pagesRef.transform) {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in tabsRef.transform) {
            child.GetComponent<Button>().interactable = true;
        }
    }
}
