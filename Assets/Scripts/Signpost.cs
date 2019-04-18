using UnityEngine;

public class Signpost : MonoBehaviour {
    public GameObject help;
    
    private void Update() {
        Collider2D collider = GetComponentInChildren<Collider2D>();
        Collider2D playerCollider = Player.Me.GetComponentInChildren<Collider2D>();
        
        // because screw the collision system
        help.SetActive(playerCollider != null && collider.bounds.Intersects(playerCollider.bounds));
    }
}
