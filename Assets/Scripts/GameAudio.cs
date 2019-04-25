using UnityEngine;

public class GameAudio : MonoBehaviour {
    public AudioClip explosion;
    public AudioClip hitEnemy;
    public AudioClip hitPlayer;
    public AudioClip jump;
    public AudioClip bottle;

    public AudioSource emSFX;
    public AudioSource emBGM;

    public static GameAudio Me;

    // if you don't want certain sounds to overlap each other, do more of this
    private float tJump = 0f;

    // becasue i'd rather just treat these like stati
    void Awake() {
        if (Me != null) {
            throw new System.Exception("you've already got an instance of GameAudio");
        }

        Me = this;

        emSFX = transform.Find("EmSFX").GetComponent<AudioSource>();
        emBGM = transform.Find("EmBGM").GetComponent<AudioSource>();
    }

    public void PlayHitEnemy() {
        emSFX.PlayOneShot(hitEnemy, 0.4f);
    }

    public void PlayHitPlayer() {
        emSFX.PlayOneShot(hitPlayer, 0.15f);
    }

    public void PlayJump() {
        if (Time.time - tJump >= 1f) {
            emSFX.PlayOneShot(jump, 1f);
            tJump = Time.time;
        }
    }

    public void PlayBottle() {
        emSFX.PlayOneShot(bottle, 0.15f);
    }
}
