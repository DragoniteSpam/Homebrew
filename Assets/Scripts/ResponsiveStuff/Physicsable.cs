using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Physicsable : Responsive {
    public override void Interact(int potionFlags) {
        GetComponent<Rigidbody2D>().simulated = true;
    }
}
