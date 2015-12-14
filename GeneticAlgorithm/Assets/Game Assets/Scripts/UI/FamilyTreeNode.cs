using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FamilyTreeNode : MonoBehaviour {

    public UILineRenderer childLineRef;
    public Image imageRef;
    public Text nameRef;
    public MonsterData relatedMonster;
    public Button button;

    public void SetUp(Vector3 childPos, Sprite picture, string name, float width, Color color, MonsterData RelatedMonster) {
        childLineRef.Setup(Vector3.zero, childPos, width, color);
        nameRef.text = name;
        imageRef.sprite = picture;
        relatedMonster = RelatedMonster;
        if (relatedMonster == null) button.interactable = false;
    }

    public void OnClick_SwitchToMonster() {
        PopupUI.Instance.Open("Changing to a previous Creature will destroy the one that came after him. Are you sure you want to continue?", Callback);
    }

    void Callback(bool result) {
        if (result) GameManager.Instance.player.currentMonster = relatedMonster;
    }
}
