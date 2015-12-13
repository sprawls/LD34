using UnityEngine;
using System.Collections;

public class CameraPositionChanger : MonoBehaviour {

    [Header("Menu")]
    [SerializeField]
    private Vector3 menuPosition;

    [SerializeField]
    private float menuSize;

    [Header("Training")]
    [SerializeField]
    private Vector3 trainingPosition;

    [SerializeField]
    private float trainingSize;

    [Header("Base value")]
    [SerializeField]
    private bool menuActive;

    public void SwitchPosition(float timeTaken) {
        StopAllCoroutines();
        StartCoroutine(SwitchAnim(menuActive ? trainingPosition : menuPosition, menuActive ? trainingSize : menuSize, timeTaken));
    }

    IEnumerator SwitchAnim(Vector3 pos, float size, float timeTaken) {
        float time = 0;
        Camera camera = GetComponent<Camera>();
        Vector3 startingPos = transform.position;
        float startingSize = camera.orthographicSize;

        //Lerp to target
        while (pos != transform.position) {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startingPos, pos, time / timeTaken);
            camera.orthographicSize = Mathf.Lerp(startingSize, size, time / timeTaken);
            yield return new WaitForEndOfFrame();
        }
    }
}
