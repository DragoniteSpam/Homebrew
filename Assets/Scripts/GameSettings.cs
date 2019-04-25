using UnityEngine;

public class GameSettings : MonoBehaviour {
    void Awake() {
        MovementStyle = MovementStyles.SNAPPY;
    }

    // all of the things

    public static MovementStyles MovementStyle {
        get; set;
    }
}

// states

public enum MovementStyles {
    SNAPPY,
    SMOOTH
}