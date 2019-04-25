using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Torch : Responsive {
    public Reaction[] reactionaryObjects;

    // I realize the "weaknesses" and "strengths" terminology is a little backwards here
    // but being hit by a "weakness" has an advantageous effect (usually) and being hit
    // by a "strength" has a disadvantageous effect (usually)

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
        foreach (Elements element in strengths) {
            if (PersistentInteraction.Recognized(potionFlags, element)) {
                SpriteRenderer sprite = transform.Find("Light").GetComponent<SpriteRenderer>();
                if (sprite.enabled) {
                    sprite.enabled = false;
                    OnFizzle();
                }
            }
        }
    }

    public virtual void OnLight() {
        foreach (Reaction r in reactionaryObjects) {
            r.React(gameObject);
        }
    }

    public virtual void OnFizzle() {
        foreach (Reaction r in reactionaryObjects) {
            r.Unreact(gameObject);
        }
    }
}
