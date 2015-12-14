using UnityEngine;
using System.Collections;

public class SpawnPlayerCreature : MonoBehaviour {

    private Creature _creature;

    public bool generetatePlayer = true;

	// Use this for initialization
	void Awake () {
        _creature = GetComponent<Creature>();
	}
	
	// Update is called once per frame
	void OnEnable () {
        if (generetatePlayer) RespawnPlayerCreature();
	}

    public void RespawnPlayerCreature() {
        _creature.GenerateFromData(GameManager.Instance.player.currentMonster.data);
    }

    public void RespawnGenePoolCreature(int index) {
        _creature.GenerateFromData(GameManager.Instance.genesPool[index]);
    }

}
