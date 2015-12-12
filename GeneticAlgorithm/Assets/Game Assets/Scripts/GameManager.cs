using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

#region singleton

    public GameManager Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
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

}
