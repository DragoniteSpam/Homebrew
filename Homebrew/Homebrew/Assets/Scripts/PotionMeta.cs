using UnityEngine;

/*
 * So, things like the animation, particles, etc
 */

public class PotionMeta {
    public PotionMeta(string name, Color color, bool implemented) {
        Name = name;
        PotionColor = color;
        Implemented = implemented;
    }

    public Color PotionColor {
        get; private set;
    }

    public string Name {
        get; private set;
    }

    public bool Implemented {
        get; private set;
    }
}
