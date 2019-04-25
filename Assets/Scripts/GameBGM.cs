using UnityEngine;

public class GameBGM : MonoBehaviour {
    void Awake() {
        GetComponent<AudioSource>().Play();
    }
}
