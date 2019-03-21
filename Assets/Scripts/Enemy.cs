using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Enemy : Responsive {
    private List<GameObject> healthPieces;
    
    protected override void Awake() {
        GetComponent<HomebrewFlags>().Set(Elements.PLAYER);

        healthPieces = new List<GameObject>();
        SetHealth();
    }

    protected override void Update() {
        base.Update();

        Collider2D collider = GetComponentInChildren<Collider2D>();
        Collider2D playerCollider = Player.Me.GetComponentInChildren<Collider2D>();

        if (playerCollider != null && collider.bounds.Intersects(playerCollider.bounds) && !Player.Me.Invincible) {
            Player.Me.AutoIFrames();
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
    }

    public override void Interact(int potionFlags) {
        base.Interact(potionFlags);
        SetHealth();
    }

    protected void SetHealth() {
        int total = (int)Mathf.Ceil(health / 4f);
        
        foreach (GameObject sprite in healthPieces) {
            Destroy(sprite);
        }

        healthPieces.Clear();
        
        for (int i = 0; i < total; i++) {
            GameObject piece = Instantiate(HomebrewGame.Me.healthSmall);
            piece.transform.parent = transform;
            piece.transform.position = new Vector3(i - (total - 1)/2f , 0.75f, -1f) + transform.position;
            piece.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Mathf.Min(0.75f, (health - 1 - i * 4) / 4f), 0f));
            healthPieces.Add(piece);
        }
    }
}
