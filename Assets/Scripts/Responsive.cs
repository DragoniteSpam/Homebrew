using System.Collections.Generic;
using UnityEngine;

public enum MaxHealthValues {
    ONE,
    TWO,
    FOUR,
    EIGHT,
    TEN,
    // ...
    MAXIMUM,
}

public class Responsive : MonoBehaviour {
    // this can't be static because c# doesn't know if it's a constant value or not,
    // but i'm naming it with capital letters anyway because you should treat it that way
    public static int[] HEALTH_VALUES = new int[(int)MaxHealthValues.MAXIMUM] {
        1, 2, 4, 8, 10
    };

    public const int BURN_RATE = 1;                 // damage per tick
    public const float BURN_TICK_RATE = 1f;         // burn ticks per second
    public const float BURN_DURATION = 1f;          // seconds
    public const float SLOW_DURATION = 0.25f;       // seconds
    public const float SLOW_SPEED = 0.5f;           // fraction
    public const float SLOW_SPEED_PARTIAL = 0.75f;  // fraction

    public MaxHealthValues maxHealth = MaxHealthValues.FOUR;
    protected int health = 1;
    protected float speedFactor = 1f;

    public List<Elements> weaknesses = new List<Elements>();

    private float timeBurn;
    private float timeSlow;
    private float timeBurnLastTick;

    protected virtual void Awake() {
        health = HEALTH_VALUES[(int)maxHealth];

        timeBurn = 0f;
        timeSlow = 0f;
        timeBurnLastTick = 0f;
    }

    protected virtual void Update() {
        // please remember to run this at some point in every derived class

        Collider2D collider = GetComponentInChildren<Collider2D>();
        foreach (PhysicalBottle bottle in PhysicalBottle.allBottles) {
            if (collider.bounds.Contains(bottle.transform.position) && bottle.owner != gameObject) {
                Interact(bottle.Flags);
                UniversalInteraction(bottle.Flags);
                Destroy(bottle.gameObject);
            }
        }

        timeBurn = timeBurn - Time.deltaTime;
        timeSlow = timeSlow - Time.deltaTime;

        if (timeBurn > 0f) {
            if (timeBurn <= (timeBurnLastTick - 0.5f)) {
                timeBurnLastTick = timeBurn;
                Damage(BURN_RATE);
            }
        } else {
            Unburn();
        }

        if (timeSlow > 0f) {
            speedFactor = SLOW_SPEED;
        } else {
            speedFactor = 1f;
        }
    }

    public virtual void Damage(int amount, GameObject whoDidIt = null) {
        health = health - amount;
        if (health <= 0) {
            Kill(whoDidIt);
        }
        OnDamage(amount);
    }

    public virtual void Kill(GameObject whoDidIt) {
        Destroy(gameObject);
    }

    public virtual void Interact(int potionFlags) {
        int myFlags = GetComponent<HomebrewFlags>().Flags;

        foreach (Elements element in weaknesses) {
            if (PersistentInteraction.Recognized(potionFlags, element)) {
                Damage(1);
            }
        }
    }

    public virtual void OnDamage(int amount) {
        HomebrewGame.CreateFloatingText(transform.position, amount + "", Color.red);
        SetHealth();
    }

    // this probably isn't needed anymore but
    protected void UniversalInteraction(int potionFlags) {
    }

    protected virtual void SetHealth() {
    }

    public void Burn() {
        if (timeBurn <= 0f) {
            timeBurn = BURN_DURATION;
            timeBurnLastTick = timeBurn;
            speedFactor = SLOW_SPEED_PARTIAL;
            Material material = GetComponentInChildren<Renderer>().material;
            material.color = Color.red;
        }
    }

    public void Slow() {
        timeSlow = SLOW_DURATION;
    }

    public void Unburn() {
        Material material = GetComponentInChildren<Renderer>().material;
        material.color = Color.white;
        speedFactor = 1f;
    }
}