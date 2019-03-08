using UnityEngine;

/*
 * My love for bitwise and hatred of Unity tags is going to be my downfall
 */

public class HomebrewFlags : MonoBehaviour {
    /*
     * collision flags - these are the physics layers
     */
    
    public const byte BOTTLE = 8;
    public const byte FOE = 9;
    public const byte ENVIRONMENT = 10;
    public const byte PLAYER = 11;
    public const byte ENEMY_BOTTLE = 12;
    
    // Things that the player can collide with
    public static int EnvironmentalCollisionMask() {
        return 1 << ENVIRONMENT;
    }

    // General collision detection
    public static int GeneralCollisionMask() {
        return (1 << BOTTLE) | (1 << FOE) | (1 << ENVIRONMENT) | (1 << PLAYER);
    }

    /*
     * non-static stuff (mostly)
     */

    public void Set(byte what) {
        Flags = Flags | (1 << what);
    }

    public void Set(Elements what) {
        Set((byte)what);
    }

    public void Toggle(byte what) {
        Flags = Flags ^ (1 << what);
    }

    public void Toggle(Elements what) {
        Toggle((byte)what);
    }

    public bool Get(byte what) {
        return (Flags & (1 << what)) == 1;
    }

    public bool Get(Elements what) {
        return Get((byte)what);
    }
    
    public int Flags {
        get; private set;
    }
}
