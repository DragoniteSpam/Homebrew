using System.Collections.Generic;
using UnityEngine;

/*
 * I'm thinking about separating the code for the projectile in physical space and
 * the effect that goes off when it hits a target, but I don't know if that's a useful
 * way to look at these things or not.
 */

public class PhysicalBottle : MonoBehaviour {
    public static List<PhysicalBottle> allBottles = new List<PhysicalBottle>();
    
    public PotionMeta metadata;
    public GameObject owner;

    void Awake() {
        allBottles.Add(this);
    }

    void OnDestroy() {
        allBottles.Remove(this);

        if (Flags==PersistentInteraction.Combination(Elements.WATER, Elements.EARTH)) {
            HazardMud.SpawnBlob(gameObject);
        }
        if (Flags == PersistentInteraction.Combination(Elements.FIRE, Elements.EARTH)) {
            HazardMagma.SpawnBlob(gameObject);
        }
    }

    // this will only fire for walls and floors and stuff since nothing else responds to this physics layer
    void OnCollisionEnter2D(Collision2D collision) {
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
