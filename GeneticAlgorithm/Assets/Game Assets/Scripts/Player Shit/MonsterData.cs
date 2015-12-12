using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterData {

    public GameObject model;
    public List<MonsterData> parents { get; private set; }
    //TODO Stats

    public MonsterData(GameObject Model, MonsterData previousMonster, MonsterData partnerMonster) {
        model = Model;
        parents = new List<MonsterData>();
        parents.Add(previousMonster);
        parents.Add(partnerMonster);
    }

    public MonsterData(GameObject Model) {
        model = Model;
        parents = null;
    }
}
