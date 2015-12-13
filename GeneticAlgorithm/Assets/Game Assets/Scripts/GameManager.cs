using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region singleton

    [HideInInspector]
    public static GameManager Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            if (player == null) {
                player = new PlayerManager();
            }
            if (cutsceneStructure == null) {
                cutsceneStructure = new CutscenesStructure();
            }
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    #endregion

    public PlayerManager player { get; private set; }
    private CutscenesStructure cutsceneStructure;

    #region Switch Scene

    public Scenes currentScene;
    public Scenes nextScene = Scenes.training; //Null if no next scenes
    public Scenes previousScene;

    public enum Scenes {
        mainMenu,
        training,
        cutscene,
        race
    }

    public void SwitchScene(Scenes scene) {
        if (currentScene != null) {
            Event_OnEndScene(currentScene);
            previousScene = currentScene;
        }
        currentScene = scene;
        Event_OnStartScene(currentScene);
    }

    void OnLevelWasLoaded(int level) {
        if (previousScene != null && currentScene == Scenes.training) GameObject.Find("UI_Manager").GetComponent<UI_Switcher>().InstantOpenTrainingUI();
        if (currentScene == Scenes.cutscene) LoadCurrentCutscene();
    }

    private void Event_OnStartScene(Scenes scene) {
        switch (scene) {
            case Scenes.training: 
                if (previousScene != Scenes.mainMenu) Application.LoadLevel("Training");
                break;

            case Scenes.cutscene:
                Application.LoadLevel("Cutscenes");
                nextScene = Scenes.training;
                break;

            case Scenes.race: 
                Application.LoadLevel("Main");
                break;
        }
    }

    private void Event_OnEndScene(Scenes scene) {
        switch (scene) {
            case Scenes.mainMenu:
                nextScene = Scenes.training;
                break;

            case Scenes.training:
                nextScene = Scenes.race;
                break;

            case Scenes.race:
                nextScene = Scenes.training;
                break;
        }
    }

    public Cutscenes_Event[] LoadCurrentCutscene() {
        return cutsceneStructure.GetCutsceneWithNumber(0);
    }

    #endregion

    #region Breeding 

    public void GenerateBreedingPartner() {

    }

    #endregion
}
