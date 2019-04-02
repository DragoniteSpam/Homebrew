using System.Collections.Generic;
using UnityEngine;

public enum Elements {
    NONE, PLAYER, FIRE, THUNDER, WATER, WIND, EARTH, SIZE
}

public class PersistentInteraction : MonoBehaviour {
    public string[] elementNames;

    private Dictionary<int, PotionMeta> potionDataLookup;

    void Awake() {
        if (Me != null) {
            throw new System.Exception("Please don't instantiate more than one PersistentInteraction, thanks");
        }

        Me = this;

        elementNames = new string[(int)Elements.SIZE] {
            "None", "Player", "Fire", "Thunder", "Water", "Wind", "Earth"
        };

        potionDataLookup = new Dictionary<int, PotionMeta> {
            // probably lerp these colors with white or something
            { Combination(Elements.NONE, Elements.NONE), new PotionMeta("Empty Bottle", new Color(0xe0 / 255f, 0xe0 / 255f, 0xe0 / 255f), true) },

            { Combination(Elements.FIRE, Elements.NONE), new PotionMeta("Basic Fire", new Color(0xff / 255f, 0x8c / 255f, 0), true) },
            { Combination(Elements.WATER, Elements.NONE), new PotionMeta("Basic Water", new Color(0, 0x66 / 255f, 0xcc / 255f), true) },
            { Combination(Elements.EARTH, Elements.NONE), new PotionMeta("Basic Earth", new Color(0xcc / 255f, 0x66 / 255f, 0), true) },

            { Combination(Elements.FIRE, Elements.FIRE), new PotionMeta("Pure Fire", new Color(0xff / 255f, 0x8c / 255f, 0), true) },
            { Combination(Elements.WATER, Elements.WATER), new PotionMeta("Pure Water", new Color(0, 0x66 / 255f, 0xcc / 255f), true) },
            { Combination(Elements.EARTH, Elements.EARTH), new PotionMeta("Pure Earth", new Color(0xcc / 255f, 0x66 / 255f, 0), true) },

            { Combination(Elements.FIRE, Elements.WATER), new PotionMeta("Steam", new Color(0x66 / 255f, 0xb2 / 255f, 1), true) },
            { Combination(Elements.FIRE, Elements.EARTH), new PotionMeta("Lava", new Color(0x66 / 255f, 0, 0), true) },
            { Combination(Elements.WATER, Elements.EARTH), new PotionMeta("Mud", new Color(0x66 / 255f, 0x33 / 255f, 0), true) },

            { Combination(Elements.WIND, Elements.NONE), new PotionMeta("Basic Wind", new Color(0x33 / 255f, 0x99 / 255f, 0x66 * 255f), false) },
            { Combination(Elements.THUNDER, Elements.NONE), new PotionMeta("Basic Thunder", new Color(0xcc / 255f, 0xcc / 255f, 0x33 / 255f), false) },
            { Combination(Elements.FIRE, Elements.THUNDER), new PotionMeta("Plasma", new Color(0f, 0f, 0f), false) },
            { Combination(Elements.FIRE, Elements.WIND), new PotionMeta("Gas", new Color(1, 0xcc / 255f, 0x99 / 255f), true) },
            { Combination(Elements.THUNDER, Elements.THUNDER), new PotionMeta("Pure Thunder", new Color(0xcc / 255f, 0xcc / 255f, 0x33 / 255f), false) },
            { Combination(Elements.THUNDER, Elements.WIND), new PotionMeta("Thunderstorm", new Color(0xa0 / 255f, 0xa0 / 255f, 0xa0 / 255f), false) },
            { Combination(Elements.THUNDER, Elements.WATER), new PotionMeta("Not yet named", new Color(0f, 0f, 0f), false) },
            { Combination(Elements.THUNDER, Elements.EARTH), new PotionMeta("Magnet", new Color(0x60 / 255f, 0x60 / 255f, 0x60 / 255f), false) },
            { Combination(Elements.WATER, Elements.WIND), new PotionMeta("Not yet named", new Color(0, 0f, 0f), false) },
            { Combination(Elements.WIND, Elements.WIND), new PotionMeta("Pure Wind", new Color(0x33 / 255f, 0x99 / 255f, 0x66 * 255f), false) },
            { Combination(Elements.WIND, Elements.EARTH), new PotionMeta("Sandstorm", new Color(0xff / 255f, 0x99 / 255f, 0x33 / 255f), false) },
        };
    }

    public PotionMeta Data(int combination) {
        return potionDataLookup[combination];
    }

    public PotionMeta Data(Elements element0, Elements element1) {
        return potionDataLookup[Combination(element0, element1)];
    }

    public static int Combination(Elements e1, Elements e2) {
        return 1 << ((int)e1) | 1 << ((int)e2);
    }

    public static bool Recognized(int flag, Elements test) {
        return (flag & (1 << (int)test)) > 0;
    }

    public static bool Recognized(int flag, int test) {
        return (flag & test) > 0;
    }

    public static PersistentInteraction Me {
        get; private set;
    }

    public static void ApplyToBottle(GameObject bottle, Elements e1, Elements e2, GameObject owner) {
        PhysicalBottle physical = bottle.AddComponent<PhysicalBottle>();
        physical.metadata = Me.potionDataLookup[Combination(e1, e2)];
        physical.Flags = Combination(e1, e2);
        physical.owner = owner;
    }
}