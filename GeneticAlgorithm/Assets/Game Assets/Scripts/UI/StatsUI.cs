using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {

    [SerializeField]
    private Text strength;

    [SerializeField]
    private Button strengthButton;

    [SerializeField]
    private Text endurance;

    [SerializeField]
    private Button enduranceButton;

    [SerializeField]
    private Text sexAppeal;

    [SerializeField]
    private Text pointsAvailable;

    void Start() {
        UpdateData();
    }

    public void UpdateData() {
        MonsterData monster = GameManager.Instance.player.currentMonster;
        strength.text = monster.Strength.ToString();
        endurance.text = monster.Endurance.ToString();
        sexAppeal.text = monster.SexAppeal.ToString();
        enduranceButton.interactable = monster.Endurance < 100 && GameManager.Instance.player.currentMonster.availablePoints > 0;
        strengthButton.interactable = monster.Strength < 100 && GameManager.Instance.player.currentMonster.availablePoints > 0;
        pointsAvailable.text = GameManager.Instance.player.currentMonster.availablePoints.ToString();
    }

    public void OnClick_TrainStrength() {
        GameManager.Instance.player.currentMonster.availablePoints--;
        GameManager.Instance.player.currentMonster.Strength++;
        UpdateData();
    }

    public void OnClick_TrainEndurance() {
        PlayerManager player = GameManager.Instance.player;
        player.currentMonster.availablePoints--;
        player.currentMonster.Endurance++;
        UpdateData();
    }
}
