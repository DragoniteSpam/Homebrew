using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Hazard : MonoBehaviour {
    public const float DEFAULT_LIFETIME = 10f;

    public float lifetime;

    void Awake() {
        lifetime = DEFAULT_LIFETIME;
    }

    protected virtual void Update() {
        Collider2D collider = GetComponentInChildren<Collider2D>();

        foreach (GameObject mob in HomebrewGame.AllMobs) {
            Collider2D mobCollider = mob.GetComponentInChildren<Collider2D>();

            if (mobCollider != null && collider.bounds.Intersects(mobCollider.bounds)) {
                Interact(mob);
            }
        }

        lifetime = lifetime - Time.deltaTime;
        if (lifetime < 1) {
            Renderer renderer = GetComponentInChildren<Renderer>();
            Color c = renderer.material.color;
            c.a = lifetime;
            renderer.material.color = c;
        }
        if (lifetime <= 0) {
            Destroy(gameObject);
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
        // slow, burn, etc
    }
}
