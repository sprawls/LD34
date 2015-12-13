using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager {

    public string name { get; private set; }

    

    //Creature Stats
    public CreatureData currentCreature;
    public int Strength = 1;
    public int Endurance = 1;
    public int SexAppeal = 1;

    //Creature Stats Limits
    public Vector2 StrVariationLimits = new Vector2(2f, 35f);
    public Vector2 EnduranceLifetimeLimits = new Vector2(6, 15f);
}
