using UnityEngine;

public class Hazard : MonoBehaviour {
    void Awake() {
        
    }

    void Update() {
        Collider2D collider = GetComponentInChildren<Collider2D>();

        foreach (GameObject mob in HomebrewGame.AllMobs) {
            Collider2D mobCollider = mob.GetComponentInChildren<Collider2D>();

            if (mobCollider != null && collider.bounds.Intersects(mobCollider.bounds)) {
                Interact(mob);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        HomebrewFlags flagData = other.gameObject.GetComponent<HomebrewFlags>();
        if (flagData != null) {
            if (flagData.Get(Elements.PLAYER)) {
                Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), other.gameObject.GetComponentInChildren<Collider2D>());
            }
        }
    }
    
    public virtual void Interact(GameObject what) {
        Debug.Log(what.name);
    }
}
