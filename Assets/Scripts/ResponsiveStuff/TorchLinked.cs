using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class TorchLinked : Torch {
    public Torch[] linkedObjects;

    public override void Interact(int potionFlags) {
        base.Interact(potionFlags);
    }

    public override void OnLight() {
        // check all linked objects before reacting
        foreach (Torch torch in linkedObjects) {
            if (!torch.transform.Find("Light").GetComponent<SpriteRenderer>().enabled) {
                return;
            }
        }
        foreach (Reaction r in reactionaryObjects) {
            r.React(gameObject);
        }
    }
}
