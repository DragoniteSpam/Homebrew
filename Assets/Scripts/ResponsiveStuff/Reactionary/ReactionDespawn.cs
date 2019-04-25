using UnityEngine;

public class ReactionDespawn : Reaction {
    public override void React(GameObject whoDidIt) {
        Destroy(gameObject);
    }
}
