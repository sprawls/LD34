using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {

    [SerializeField]
    private Text strength;

    [SerializeField]
    private Text endurance;

    [SerializeField]
    private Text sexAppeal;

    [SerializeField]
    private Text pointsAvailable;

    public void UpdateData() {
        MonsterData monster = GameManager.Instance.player.currentMonster;
        strength.text = monster.Strength.ToString();
        endurance.text = monster.Endurance.ToString();
        sexAppeal.text = monster.SexAppeal.ToString();
        pointsAvailable.text = GameManager.Instance.player.availableTrainingPoints.ToString();
    }

    public void OnClick_TrainStrength() {
        if (GameManager.Instance.player.availableTrainingPoints > 0) {
            GameManager.Instance.player.availableTrainingPoints--;
            GameManager.Instance.player.currentMonster.Strength++;
        }
        UpdateData();
    }

    public void OnClick_TrainEndurance() {
        if (GameManager.Instance.player.availableTrainingPoints > 0) {
            GameManager.Instance.player.availableTrainingPoints--;
            GameManager.Instance.player.currentMonster.Endurance++;
        }
        UpdateData();
    }
}
