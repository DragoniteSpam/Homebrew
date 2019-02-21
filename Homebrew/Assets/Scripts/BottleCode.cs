using UnityEngine;

public class BottleCode : MonoBehaviour {
    public DefaultPotion potionEffect;

    void OnCollisionEnter2D(Collision2D collision) {
        potionEffect.Interact(collision.gameObject);
        Destroy(this.gameObject);
    }
}
