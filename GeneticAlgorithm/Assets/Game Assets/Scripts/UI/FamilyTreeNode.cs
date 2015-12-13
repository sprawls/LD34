using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FamilyTreeNode : MonoBehaviour {

    public UILineRenderer childLineRef;
    public Image imageRef;
    public Text nameRef;

    public void SetUp(Vector3 childPos, Sprite picture, string name, float width, Color color) {
        childLineRef.Setup(Vector3.zero, childPos, width, color);
        nameRef.text = name;
        imageRef.sprite = picture;
    }
}
