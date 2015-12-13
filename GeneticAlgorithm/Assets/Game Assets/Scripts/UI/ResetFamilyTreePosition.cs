using UnityEngine;
using System.Collections;

public class ResetFamilyTreePosition : MonoBehaviour {

    public void Reset() {
        RectTransform t = GetComponent<RectTransform>();
        t.localPosition = new Vector3(t.localPosition.x, 0, t.localPosition.z);
    }
}
