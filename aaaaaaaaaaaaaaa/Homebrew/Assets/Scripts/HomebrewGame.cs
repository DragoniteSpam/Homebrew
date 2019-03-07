using UnityEngine;

public class HomebrewGame : MonoBehaviour {
    void Awake() {
        // fun fact: physics uses default gravity of 0, -1, 0, but physics2d uses default
        // gravity of 0, -9.8, 0. nice, right?
        Physics2D.gravity = new Vector2(0f, -9.8f*2f);
    }
}
