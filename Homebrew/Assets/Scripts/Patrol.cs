using UnityEngine;

public class Patrol : MonoBehaviour {
    public float maxSpeed = 4;

    private void Update() {
        float currentSpeed = maxSpeed * (transform.localScale.x > 0 ? 1f : -1f);
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);

        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 testPoint = position2D + Vector2.right * (transform.localScale.x > 0 ? 2f : -2f);

        // does not seek immediately underneath the foe. seeks off to the side instead.
        bool grounded = Physics2D.OverlapPoint(testPoint + Vector2.down * 0.75f, HomebrewFlags.EnvironmentalCollisionMask());
        bool walled = Physics2D.OverlapPoint(testPoint, HomebrewFlags.EnvironmentalCollisionMask());
        
        if (!grounded || walled) {
            Turn();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        HomebrewFlags flagData = other.gameObject.GetComponent<HomebrewFlags>();
        if (flagData != null) {
            if (flagData.Get(HomebrewFlags.FLAG_PLAYER)) {
                Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), other.gameObject.GetComponentInChildren<Collider2D>());
            }
        }
    }

    private void Turn() {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}
