using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Foe : Responsive {
    protected override void Awake() {
        GetComponent<HomebrewFlags>().Set(Elements.PLAYER);
    }

    protected override void Update() {
        base.Update();
    }

    // collision with player. not collision with potions. those are checked in update.
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        HomebrewFlags flagData = other.gameObject.GetComponent<HomebrewFlags>();
        if (flagData != null) {
            if (flagData.Get(Elements.PLAYER)) {
                Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), other.gameObject.GetComponentInChildren<Collider2D>());
            }
        }
    }
}
