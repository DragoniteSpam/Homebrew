using UnityEngine;

public class Responsive : MonoBehaviour {
    public int health = 1;

    protected virtual void Awake() {
    }

    protected virtual void Update() {
        // please remember to run this at some point in every derived class
        Collider2D collider = GetComponentInChildren<Collider2D>();
        foreach (PhysicalBottle bottle in PhysicalBottle.allBottles) {
            if (collider.bounds.Contains(bottle.transform.position)) {
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
            Debug.Log(name + " was hit by an instance of Fire");
        } else if (PersistentInteraction.Recognized(potionFlags, Elements.WATER)) {
            Debug.Log(name + " was hit by an instance of Water");
        } else if (PersistentInteraction.Recognized(potionFlags, Elements.WIND)) {
            Debug.Log(name + " was hit by an instance of Wind");
        } else if (PersistentInteraction.Recognized(potionFlags, Elements.THUNDER)) {
            Debug.Log(name + " was hit by an instance of Thunder");
        } else if (PersistentInteraction.Recognized(potionFlags, Elements.EARTH)) {
            Debug.Log(name + " was hit by an instance of Earth");
        } else {
            // colliding elements not recognized
        }
    }
}
