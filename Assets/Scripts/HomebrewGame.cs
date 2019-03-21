using UnityEngine;

public class HomebrewGame : MonoBehaviour {
    public GameObject healthSmall;

    public static HomebrewGame Me;
    void Awake() {
        if (Me == null) {
            Me = this;
        } else {
            throw new System.Exception("Please don't instantiate more than one HomebrewGame");
        }

        // fun fact: physics uses default gravity of 0, -1, 0, but physics2d uses default
        // gravity of 0, -9.8, 0. nice, right?
        Physics2D.gravity = new Vector2(0f, -9.8f * 2f);
    }
}
