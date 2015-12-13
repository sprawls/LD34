using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager {
    public PlayerManager() {
        //Create first creature TODO change this to right gen
        currentCreature = new MonsterData(new CreatureData());
    }

    public MonsterData currentCreature { get; private set; }

    public void ChangeCreature(CreatureData child, CreatureData partner) {
        currentCreature = new MonsterData(child, currentCreature, partner);
    }
}
