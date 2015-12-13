using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterData {

    public CreatureData data;
    public List<MonsterData> parents { get; private set; }
    //TODO Stats

    public MonsterData(CreatureData Data, MonsterData previousMonster, CreatureData partnerMonster) {
        data = Data;
        parents = new List<MonsterData>();
        parents.Add(previousMonster);
        parents.Add(new MonsterData(partnerMonster));
    }

    public MonsterData(CreatureData Data) {
        data = Data;
        parents = null;
    }
}
