using UnityEngine;
using System.Collections;

public class FollowCreature : MonoBehaviour {

    public GameObject targetCreature;

    private Vector3 targetPosition;
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }
	// Update is called once per frame
	void Update () {
        if (targetCreature != null) targetPosition = targetCreature.transform.position;
        else targetPosition = initialPosition;

        Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        transform.position = new Vector3(lerpPosition.x, lerpPosition.y, initialPosition.z);
	}
}
