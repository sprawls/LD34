using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour {

    public Vector3 pointA;
    public Vector3 pointB;
    public float lineWidth;

    [SerializeField]
    private RectTransform imageRectTransform;

    public void Setup(Vector3 a, Vector3 b, float width) {
        pointA = a;
        pointB = b;
        lineWidth = width;
    }

    void Awake() {
        MakeLine();
    }

    void OnValidate() {
        MakeLine();
    }

    void MakeLine() {
        Vector3 differenceVector = pointB - pointA;

        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.position = pointA;
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
