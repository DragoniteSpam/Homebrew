using UnityEngine;

public class Responsive : MonoBehaviour {
    public int health = 1;

    protected virtual void Awake() {
    }

    protected virtual void Update() {
        // please remember to run this at some point in every derived class
        Collider2D collider = GetComponentInChildren<Collider2D>();
        foreach (PhysicalBottle bottle in PhysicalBottle.allBottles) {
            if (collider.bounds.Contains(bottle.transform.position) && bottle.owner != gameObject) {
                Interact(bottle.Flags);
                Destroy(bottle.gameObject);
            }
        }
    }

    public virtual void Kill(GameObject whoDidIt) {

    }

    public virtual void Interact(int potionFlags) {
        int myFlags = GetComponent<HomebrewFlags>().Flags;

        /*Whoever messes with this next, look down here
        You probably want to deduct health when hit by valid elements,
        but you can do other things too, like spawn a cloud of gas
        around a detector or something
        
        and also comment out this message, i just did this to get your attention*/

        if (PersistentInteraction.Recognized(potionFlags, Elements.FIRE)) {
            if (--health <= 0) {
                Kill(null);
            }
        }
        if (PersistentInteraction.Recognized(potionFlags, Elements.WATER)) {
            if (--health <= 0) {
                Kill(null);
            }
        }
        if (PersistentInteraction.Recognized(potionFlags, Elements.WIND)) {
            if (--health <= 0) {
                Kill(null);
            }
        }
        if (PersistentInteraction.Recognized(potionFlags, Elements.THUNDER)) {
            if (--health <= 0) {
                Kill(null);
            }
        }
        if (PersistentInteraction.Recognized(potionFlags, Elements.EARTH)) {
            if (--health <= 0) {
                Kill(null);
            }
        }
    }
}
