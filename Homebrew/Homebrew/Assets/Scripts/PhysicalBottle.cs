using UnityEngine;

/*
 * I'm thinking about separating the code for the projectile in physical space and
 * the effect that goes off when it hits a target, but I don't know if that's a useful
 * way to look at these things or not.
 */

public class PhysicalBottle : MonoBehaviour {
    public PotionMeta metadata;

    void OnCollisionEnter2D(Collision2D collision) {
        Interactible collisionInteraction = collision.gameObject.GetComponent<Interactible>();

        if (collisionInteraction != null) {
            collisionInteraction.Interact(Flags);
        } else {
            Debug.Log(collision.gameObject.name + " doesn't have any defined interaction. Should probably just be ignored since the only things that won't have them are the walls and floor and stuff, but if you want to do anything with them they should go here.");
        }

        Destroy(gameObject);
    }

    /*
     * i guess you could subclass the potion bottles, but you could also just swap out the PotionMeta
     * and set the flag based on whatever it's carrying (ie the elements)
     * 
     * peng doesn't want to subclass anything, what's happening?!
     */
    
    public int Flags {
        get; set;
    }

    /*
     * For better or for worse unity does the stuff like gravity for you
     */
}
