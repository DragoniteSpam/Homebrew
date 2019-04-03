using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Enemy : Responsive {
    new protected static bool showHealth = true;

    protected override void Awake() {
        base.Awake();

        GetComponent<HomebrewFlags>().Set(Elements.PLAYER);

        HomebrewGame.AddMob(gameObject);
        SetHealth();
    }

    protected override void Update() {
        base.Update();

        Collider2D collider = GetComponentInChildren<Collider2D>();
        Collider2D playerCollider = Player.Me.GetComponentInChildren<Collider2D>();

        if (playerCollider != null && collider.bounds.Intersects(playerCollider.bounds)) {
            Player.Me.Damage(1);
        }
    }
    
    // this was for something at some point
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        HomebrewFlags flagData = other.gameObject.GetComponent<HomebrewFlags>();
        if (flagData != null) {
        }
    }

    public override void Kill(GameObject who) {
        base.Kill(who);
        HomebrewGame.RemoveMob(gameObject);
    }

    public override void Interact(int potionFlags) {
        base.Interact(potionFlags);
        SetHealth();
    }

    // only un-comment if you want to change this
    //public override void Damage(int amount) {
    //    base.Damage(amount);
    //}
}
