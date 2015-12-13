using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitingIndicator : MonoBehaviour {

    [Header("Component")]
    [SerializeField]
    private Text waiting;

    [SerializeField]
    private Text waitText;

    [SerializeField]
    private GameObject grey;

    public void StartWaiting(string textDisplayed) {
        grey.SetActive(true);
        StartCoroutine(WaitingAnim());
        waiting.gameObject.SetActive(true);
        waitText.gameObject.SetActive(true);
        waitText.text = textDisplayed;
    }

    public void StopWaiting() {
        StopAllCoroutines();
        grey.SetActive(false);
        waiting.gameObject.SetActive(false);
        waitText.gameObject.SetActive(false);
    }

    IEnumerator WaitingAnim() {
        int count = 0;
        while (true) {
            switch (count % 4) {
                case 0: waiting.text = ""; break;
                case 1: waiting.text = "."; break;
                case 2: waiting.text = ".."; break;
                case 3: waiting.text = "..."; break;
            }
            count++;
            yield return new WaitForSeconds(0.3f);
        }
    }


}
