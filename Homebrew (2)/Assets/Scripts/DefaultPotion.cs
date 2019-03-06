using UnityEngine;

public class DefaultPotion {
    public DefaultPotion() {
        
    }

    public void Interact(GameObject what) {
        Debug.Log("Potion collided with: " + what.name);
    }
}