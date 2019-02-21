using System.Collections.Generic;
using UnityEngine;

public struct PotionData {
    public string name;
    public Color color;
    public bool implemented;
    public System.Type pType;
    public PotionData(string name, Color color, bool implemented, System.Type pType) {
        this.name = name;
        this.color = color;
        this.implemented = implemented;
        this.pType = pType;
    }
}

public enum Elements {
    FIRE, THUNDER, WATER, WIND, EARTH
}

public class Interaction : MonoBehaviour {
    private Dictionary<int, PotionData> potionDataLookup;

    void Awake() {
        potionDataLookup = new Dictionary<int, PotionData>();

        potionDataLookup.Add(Combination(Elements.FIRE, Elements.FIRE), new PotionData("Pure Fire", new Color(0xff / 255f, 0x8c / 255f, 0), true, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.FIRE, Elements.THUNDER), new PotionData("Plasma", new Color(0f, 0f, 0f), true, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.FIRE, Elements.WATER), new PotionData("Steam", new Color(0x66 / 255f, 0xb2 / 255f, 1), true, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.FIRE, Elements.WIND), new PotionData("Gas", new Color(1, 0xcc / 255f, 0x99 / 255f), true, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.FIRE, Elements.EARTH), new PotionData("Lava", new Color(0x66 / 255f, 0, 0), false, typeof(DefaultPotion)));

        potionDataLookup.Add(Combination(Elements.THUNDER, Elements.THUNDER), new PotionData("Pure Thunder", new Color(0xcc / 255f, 0xcc / 255f, 0x33 / 255f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.THUNDER, Elements.WIND), new PotionData("Thunderstorm", new Color(0xa0 / 255f, 0xa0 / 255f, 0xa0 / 255f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.THUNDER, Elements.WATER), new PotionData("Not yet named", new Color(0f, 0f, 0f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.THUNDER, Elements.EARTH), new PotionData("Magnet", new Color(0x60 / 255f, 0x60 / 255f, 0x60 / 255f), true, typeof(DefaultPotion)));

        potionDataLookup.Add(Combination(Elements.WATER, Elements.WATER), new PotionData("Pure Water", new Color(0, 0x66 / 255f, 0xcc / 255f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.WATER, Elements.WIND), new PotionData("Not yet named", new Color(0, 0f, 0f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.WATER, Elements.EARTH), new PotionData("Mud", new Color(0x66 / 255f, 0x33 / 255f, 0), false, typeof(DefaultPotion)));

        potionDataLookup.Add(Combination(Elements.WIND, Elements.WIND), new PotionData("Pure Wind", new Color(0x33 / 255f, 0x99 / 255f, 0x66 * 255f), false, typeof(DefaultPotion)));
        potionDataLookup.Add(Combination(Elements.WIND, Elements.EARTH), new PotionData("Sandstorm", new Color(0xff / 255f, 0x99 / 255f, 0x33 / 255f), false, typeof(DefaultPotion)));

        potionDataLookup.Add(Combination(Elements.EARTH, Elements.EARTH), new PotionData("Pure Earth", new Color(0xcc / 255f, 0x66 / 255f, 0), false, typeof(DefaultPotion)));
    }

    private int Combination(Elements e1, Elements e2) {
        return 1 << ((int)e1) | 1 << ((int)e2);
    }
}