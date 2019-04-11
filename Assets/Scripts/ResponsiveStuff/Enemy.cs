using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Enemy : Responsive {
    protected SpriteRenderer healthBar;
    protected override void Awake() {
        base.Awake();

        healthBar = Instantiate(HomebrewGame.Me.prefabEnemyHealth).GetComponent<SpriteRenderer>();
        healthBar.transform.SetParent(transform, false);

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

    public override void Interact(int potionFlags) {
        int startingHealth = health;
        base.Interact(potionFlags);
        if (startingHealth == health) {
            // please make something better for this
            HomebrewGame.CreateFloatingText(transform.position, ":(", Color.white);
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

    protected override void SetHealth() {
        healthBar.sprite = HomebrewGame.Me.spritesHealth[(int)maxHealth].images[health];
    }

    // only un-comment if you want to change this
    //public override void Damage(int amount) {
    //    base.Damage(amount);
    //}
}
