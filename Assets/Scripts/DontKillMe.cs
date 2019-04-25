using UnityEngine;

public class DontKillMe : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
