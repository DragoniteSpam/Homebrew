using System.Collections.Generic;
using UnityEngine;

public class Responsive : MonoBehaviour {
    public const int BURN_RATE = 1;     // per tick
    public const float BURN_TICK_RATE = 2;  // burn ticks per second
    public const float STATUS_DURATION = 5f;
    public const float SLOW_SPEED = 0.5f;
    public const float SLOW_SPEED_PARTIAL = 0.75f;
    
    public int maxHealth = 1;
    protected List<GameObject> healthPieces;
    protected float speedFactor = 1f;

    public List<Elements> weaknesses = new List<Elements>();

    protected int health;

    private float timeBurn;
    private float timeBurnLastTick;
    private float timeSlow;

    protected virtual void Awake() {
        health = maxHealth;

        healthPieces = new List<GameObject>();
        // by default don't show the health pieces

        timeBurn = 0f;
        timeBurnLastTick = 0f;
        timeSlow = 0f;
    }

    protected virtual void Update() {
        // please remember to run this at some point in every derived class

        // i really dont like this but i procrastinated and don't have time to come up with something
        // less awful
        if (GetComponent<HomebrewFlags>().Get(Elements.PLAYER)) {
            Collider2D collider = GetComponentInChildren<Collider2D>();
            foreach (PhysicalBottle bottle in PhysicalBottle.allBottles) {
                if (collider.bounds.Contains(bottle.transform.position) && bottle.owner != gameObject) {
                    Interact(bottle.Flags);
                    UniversalInteraction(bottle.Flags);
                    Destroy(bottle.gameObject);
                }
            }
        }
        
        timeBurn = timeBurn - Time.deltaTime;
        timeSlow = timeSlow - Time.deltaTime;
        if (timeBurn > 0f) {
            if (timeBurn <= (timeBurnLastTick - 0.5f)) {
                timeBurnLastTick = timeBurn;
                timeBurn = timeBurn - Time.deltaTime;
                health = health - BURN_RATE;
                OnDamage(BURN_RATE);
                speedFactor = SLOW_SPEED_PARTIAL;
            }
        } else {
            Unburn();
            speedFactor = 1f;
        }
        if (timeSlow > 0f) {
            speedFactor = SLOW_SPEED;
        } else {
            speedFactor = 1f;
        }
    }

    public virtual void Kill(GameObject whoDidIt) {
        Destroy(gameObject);
    }

    public virtual void Interact(int potionFlags) {
        int myFlags = GetComponent<HomebrewFlags>().Flags;

        foreach (Elements element in weaknesses) {
            if (PersistentInteraction.Recognized(potionFlags, element)) {
                if (--health <= 0) {
                    Kill(null);
                }
            }
        }
    }

    public virtual void OnDamage(int amount) {
        HomebrewGame.CreateFloatingText(transform.position, amount + "", Color.red);
        if (health <= 0) {
            Kill(null);
        }
    }

    protected void UniversalInteraction(int potionFlags) {
        // Not a fan but
        if (potionFlags == PersistentInteraction.Combination(Elements.FIRE, Elements.THUNDER)) {
            // Plasma
        }
    }

    protected virtual void SetHealth() {
        int total = (int)Mathf.Ceil(health / 4f);

        foreach (GameObject sprite in healthPieces) {
            Destroy(sprite);
        }

        healthPieces.Clear();

        for (int i = 0; i < total; i++) {
            GameObject piece = Instantiate(HomebrewGame.Me.healthSmall);
            piece.transform.parent = transform;
            piece.transform.position = new Vector3(i - (total - 1) / 2f, 0.75f, -1f) + transform.position;
            piece.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Mathf.Min(0.75f, Mathf.Ceil((health - 1 - i * 4) / 4f)), 0f));
            healthPieces.Add(piece);
        }
    }

    public void Burn() {
        // continuously re-burning yourself would keep the burn timer from ticking down, which would be a problem
        // because it would keep getting reset before you could take damage - if something is still trying to burn
        // you after you cool off it'll re-burn you, but that's all
        if (timeBurn <=0) {
            timeBurn = STATUS_DURATION;
            timeBurnLastTick = STATUS_DURATION;

            Material material = GetComponent<Renderer>().material;
            material.color = Color.Lerp(material.color, Color.red, 0.5f);
        }
    }

    public void Slow() {
        timeSlow = STATUS_DURATION;
    }

    public void Unburn() {
        Material material = GetComponent<Renderer>().material;
        material.color = Color.white;
    }

    public void Unslow() {

    }
}
