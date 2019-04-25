using UnityEngine;

public class ReactionDespawn : Reaction {
    public override void React(GameObject whoDidIt) {
        gameObject.SetActive(false);
    }

    public override void Unreact(GameObject whoDidIt) {
        gameObject.SetActive(true);
    }
}
