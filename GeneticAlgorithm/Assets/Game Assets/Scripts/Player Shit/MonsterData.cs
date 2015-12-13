using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterData {

    public CreatureData data;
    public List<MonsterData> parents { get; private set; }

    public int Strength = 1;
    public int Endurance = 1;
    public int SexAppeal = 1;
    public int availablePoints = 0;

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

    public void AddSexAppeal(int val) {
        SexAppeal = Mathf.Clamp(SexAppeal + val, 0, 100);
    }
}
