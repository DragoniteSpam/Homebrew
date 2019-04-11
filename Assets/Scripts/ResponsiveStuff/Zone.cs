using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class Zone : Responsive {
    // i attached a slime sprite renderer to the zone but it's just there so
    // that you can see where-ish they are
    public override void Interact(int potionFlags) {
        HomebrewGame.CreateFloatingText(transform.position, "Hit zone", Color.blue);
    }
}
