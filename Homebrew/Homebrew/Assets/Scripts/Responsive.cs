using UnityEngine;

public class Responsive : MonoBehaviour {
    protected virtual void Awake() {
    }

    protected virtual void Update() {
        // please remember to run this at some point in every derived class
        Collider2D collider = GetComponentInChildren<Collider2D>();
        foreach (PhysicalBottle bottle in PhysicalBottle.allBottles) {
            if (collider.bounds.Contains(bottle.transform.position)) {
                GetComponent<Interactible>().Interact(bottle.Flags);
                Destroy(bottle.gameObject);
            }
        }
    }
}
