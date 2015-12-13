using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager {
    public PlayerManager() {
        //Create first creature TODO change this to right gen
        currentMonster = new MonsterData(CreatureData.GetRandom());
    }

    //Creature Stats
    public MonsterData currentMonster;

    //Creature Stats Limits
    public Vector2 StrVariationLimits = new Vector2(2f, 35f);
    public Vector2 EnduranceLifetimeLimits = new Vector2(6, 15f);


    public void ChangeCreature(CreatureData child, CreatureData partner) {
        currentMonster = new MonsterData(child, currentMonster, partner);
    }

}
