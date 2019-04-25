using UnityEngine;

public class GameSettings : MonoBehaviour {
    // all of the things

    public static MovementStyles MovementStyle {
        get; set;
    }

    public static float VolumeSFX {
        get; set;
    }

    public static float VolumeBGM {
        get; set;
    }
}

// states

public enum MovementStyles {
    SNAPPY,
    SMOOTH
}