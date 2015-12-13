using UnityEngine;
using System.Collections;

public class SpawnPlayerCreature : MonoBehaviour {

    private Creature _creature;

	// Use this for initialization
	void Awake () {
        _creature = GetComponent<Creature>();
	}
	
	// Update is called once per frame
	void OnEnable () {
        RespawnPlayerCreature();
	}

    public void RespawnPlayerCreature() {
        _creature.GenerateFromData(GameManager.Instance.player.currentMonster.data);
    }

}
