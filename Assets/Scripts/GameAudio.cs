using UnityEngine;

public class GameAudio : MonoBehaviour {
    public AudioClip explosion;
    public AudioClip hitEnemy;
    public AudioClip hitPlayer;
    public AudioClip jump;
    public AudioClip bottle;

    public AudioSource emitter;

    public static GameAudio Me;

    // becasue i'd rather just treat these like stati
    void Awake() {
        if (Me != null) {
            throw new System.Exception("you've already got an instance of GameAudio");
        }

        Me = this;

        emitter = GetComponent<AudioSource>();
    }
}
