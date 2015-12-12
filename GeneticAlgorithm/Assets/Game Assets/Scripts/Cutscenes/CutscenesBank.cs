using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutscenesStructure {

    public CutscenesStructure() {
        CutscenesBank.Create(this);
    }

    List<Cutscenes_Event[]> cutscenes = new List<Cutscenes_Event[]>();

    public Cutscenes_Event[] GetCutsceneWithNumber(int number) {
        return number < cutscenes.Count ? cutscenes[number] : null;
    }

    public void CreateCutscene(params Cutscenes_Event[] cutscene) {
        cutscenes.Add(cutscene);
    }
}

public class Cutscenes_Event {
    public string textBubble { get; private set; }
    public bool playerSpeaking { get; private set; }
    public float time { get; private set; }

    public Cutscenes_Event(string text, bool isPlayerSpeaking, float timeToComplete) {
        textBubble = text;
        playerSpeaking = isPlayerSpeaking;
        time = timeToComplete;
    }
}

public class CutscenesBank {
    public static void Create(CutscenesStructure structure) {
        /* 1 */
        structure.CreateCutscene(
            new Cutscenes_Event("Test1", true, 5f),
            new Cutscenes_Event("Test2", false, 5f),
            new Cutscenes_Event("Test3", true, 5f),
            new Cutscenes_Event("Test4", false, 5f),
            new Cutscenes_Event("Test5", true, 5f)
        );

        /* 2 */
    }
}
