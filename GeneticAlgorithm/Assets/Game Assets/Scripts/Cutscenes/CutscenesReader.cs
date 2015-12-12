using UnityEngine;
using System.Collections;

public class CutscenesReader : MonoBehaviour {

    [Header("Component")]
    [SerializeField]
    private SpeechBubble player;

    [SerializeField]
    private SpeechBubble scientist;

    void Start() {
        StartCoroutine(PlayCutscene(GameManager.Instance.LoadCurrentCutscene()));
    }

    IEnumerator PlayCutscene(Cutscenes_Event[] events) {
        foreach (Cutscenes_Event evnt in events) {
            player.gameObject.SetActive(evnt.playerSpeaking);
            scientist.gameObject.SetActive(!evnt.playerSpeaking);
            SpeechBubble current = evnt.playerSpeaking ? player : scientist;
            current.text.text = evnt.textBubble;

            bool skip = false;
            float time = 0f;
            while (!skip) {
                if (Input.GetKey(KeyCode.Mouse0) || time >= evnt.time) skip = true;
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.3f);
        }

        GameManager.Instance.SwitchScene(GameManager.Instance.nextScene);
    }
}
