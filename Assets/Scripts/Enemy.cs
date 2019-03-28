using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Enemy : Responsive {    
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

        if (playerCollider != null && collider.bounds.Intersects(playerCollider.bounds) && !Player.Me.Invincible) {
            Player.Me.Damage(1);
        }
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

    public override void Kill(GameObject who) {
        base.Kill(who);
        HomebrewGame.RemoveMob(gameObject);
    }

    public override void Interact(int potionFlags) {
        base.Interact(potionFlags);
        SetHealth();
    }

    public override void OnDamage() {
        SetHealth();
    }
}
