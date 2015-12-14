using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterData {

    public string name { get; private set; }
    public CreatureData data;
    public List<MonsterData> parents { get; private set; }

    public int SexAppeal = 1;
    public int availablePoints = 0;

    public MonsterData(CreatureData Data, MonsterData previousMonster, CreatureData partnerMonster) {
        data = Data;
        parents = new List<MonsterData>();
        parents.Add(previousMonster);
        parents.Add(new MonsterData(partnerMonster));
        name = NameGenerator.Randomize();
    }

    public MonsterData(CreatureData Data) {
        data = Data;
        parents = null;
        name = NameGenerator.Randomize();
    }

    public void AddSexAppeal(int val) {
        SexAppeal = Mathf.Clamp(SexAppeal + val, 0, 100);
    }
}
