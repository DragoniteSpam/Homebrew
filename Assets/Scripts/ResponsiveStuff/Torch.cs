using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Torch : Responsive {
    public Reaction[] reactionaryObjects;

    public override void Interact(int potionFlags) {
        foreach (Elements element in weaknesses) {
            if (PersistentInteraction.Recognized(potionFlags, element)) {
                SpriteRenderer sprite = transform.Find("Light").GetComponent<SpriteRenderer>();
                if (!sprite.enabled) {
                    sprite.enabled = true;
                    OnLight();
                }
            }
        }
    }

    public virtual void OnLight() {
        foreach (Reaction r in reactionaryObjects) {
            r.React(gameObject);
        }
    }
}
