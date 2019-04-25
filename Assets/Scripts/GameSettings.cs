using UnityEngine;

public class GameSettings : MonoBehaviour {
    // all of the things

    public static MovementStyles MovementStyle {
        get; set;
    }

    private static float fVolumeSFX = 1.0f;
    public static float VolumeSFX {
        get {
            return fVolumeSFX;
        }
        set {
            fVolumeSFX = value;
            if (GameAudio.Me != null) {
                GameAudio.Me.emSFX.volume = fVolumeSFX;
            }
        }
    }

    private static float fVolumeBGM = 1.0f;
    public static float VolumeBGM {
        get {
            return fVolumeBGM;
        }
        set {
            fVolumeBGM = value;
            if (GameAudio.Me != null) {
                GameAudio.Me.emBGM.volume = fVolumeBGM;
            }
        }
    }
}

// states

public enum MovementStyles {
    SNAPPY,
    SMOOTH
}