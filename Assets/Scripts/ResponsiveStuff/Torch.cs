using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Torch : Responsive {
    public override void Interact(int potionFlags) {
        SpriteRenderer sprite = transform.Find("Light").GetComponent<SpriteRenderer>();
        if (!sprite.enabled) {
            sprite.enabled = true;
            OnLight();
        }
    }

    public virtual void OnLight() {

    }
}
