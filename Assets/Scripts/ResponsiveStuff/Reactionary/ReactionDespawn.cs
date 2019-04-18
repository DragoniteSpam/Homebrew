using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDespawn : Reaction {
    public override void React(GameObject whoDidIt) {
        Destroy(gameObject);
    }
}
