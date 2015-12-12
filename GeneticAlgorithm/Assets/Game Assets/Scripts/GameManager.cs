using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region singleton

    public GameManager Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            if (player == null) {
                player = new PlayerManager();
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

    #region Switch Scene

    public Scenes currentScene;

    public enum Scenes {
        mainMenu,
        training,
        cutscene,
        race
    }

    public void SwitchScene(Scenes scene) {
        if (currentScene != null) Event_OnEndScene(currentScene);
        currentScene = scene;
        Event_OnStartScene(currentScene);
    }

    private void Event_OnStartScene(Scenes scene) {
        switch (scene) {
            case Scenes.training: 
                Application.LoadLevel("Training");
                break;

            case Scenes.cutscene:
                LoadCurrentCutscene();
                break;

            case Scenes.race: 
                Application.LoadLevel("Main");
                break;
        }
    }

    private void Event_OnEndScene(Scenes scene) {
        switch (scene) {

        }
    }

    private void LoadCurrentCutscene() {

    }

    #endregion

    #region Breeding 

    public void GenerateBreedingPartner() {

    }

    #endregion
}
