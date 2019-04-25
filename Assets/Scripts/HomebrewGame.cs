using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPieceWrapper {
    public Sprite[] images;
}

[RequireComponent(typeof(GameMenu))]
public class HomebrewGame : MonoBehaviour {
    private static List<GameObject> allMobs = new List<GameObject>();

    public GameObject healthSmall;
    public GameMenu menu;

    [Header("Prefabs for this or that")]
    public GameObject prefabHazard;
    public GameObject prefabHazardGassy;
    public GameObject prefabHazardVenom;
    public GameObject prefabFloatingText;
    public GameObject prefabEnemyHealth;

    [Header("sprites")]
    public List<Sprite> spritesMud = new List<Sprite>();
    public List<Sprite> spritesMagma = new List<Sprite>();
    public List<Sprite> spritesSteam = new List<Sprite>();

    public HealthPieceWrapper[] spritesHealth = new HealthPieceWrapper[Responsive.HEALTH_VALUES.Length];
    
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

    void Start() {
        menu = GetComponent<GameMenu>();
        menu.UpdateUIThings();
    }

    public static void AddMob(GameObject what) {
        allMobs.Add(what);
    }

    public static void RemoveMob(GameObject what) {
        allMobs.Remove(what);
    }

    public static List<GameObject> AllMobs {
        get {
            return allMobs;
        }
    }

    public static GameObject CreateFloatingText(Vector3 position, string message, Color color, float lifespan = 1f, float fadeTime = 0.6f) {
        TextMesh floatingText = Instantiate(Me.prefabFloatingText, position, Quaternion.identity).GetComponent<TextMesh>();
        floatingText.text = message;
        floatingText.color = color;

        FloatingText floating = floatingText.GetComponent<FloatingText>();
        floating.lifespan = lifespan;
        floating.fadeTime = fadeTime;

        return floatingText.gameObject;
    }

    public static GameObject CreateFloatingText(Vector3 position, string message, float lifespan = 1f, float fadeTime = 0.6f) {
        return CreateFloatingText(position, message, Color.blue, lifespan, fadeTime);
    }
}