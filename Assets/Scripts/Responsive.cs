using System.Collections.Generic;
using UnityEngine;

public class Responsive : MonoBehaviour {
    public int health = 1;

    public List<Elements> weaknesses = new List<Elements>();

    protected virtual void Awake() {
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

        if (PersistentInteraction.Recognized(potionFlags, PersistentInteraction.Combination(Elements.WATER, Elements.EARTH))) {

        }
    }

    protected void UniversalInteraction(int potionFlags) {
        // Not a fan but
        if (potionFlags == PersistentInteraction.Combination(Elements.FIRE, Elements.THUNDER)) {
            // Plasma
        }
    }
}
