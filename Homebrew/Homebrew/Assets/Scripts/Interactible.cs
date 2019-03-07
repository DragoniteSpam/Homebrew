using UnityEngine;

/*
 * anything that gets triggered by a potion/etc should have a health bar. for things like
 * environmental props that can be set on fire, the health bar doesn't have to be visible,
 * and max hp can be 1, so that the correct element will activate it in one hit.
 * 
 * foes can be more complicated.
 */

public class Interactible : MonoBehaviour {
    public int health = 1;
    
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
