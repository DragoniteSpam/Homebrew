using UnityEngine;

public class Signpost : MonoBehaviour {
    private const float CHAR_PER_SECOND = 48;

    private float t = 0f;
    private string message;

    GameObject helpRoot;
    TextMesh threedtext;

    void Awake() {
        // ლ(ಠ益ಠლ)
        helpRoot = transform.Find("Help").gameObject;
        threedtext = helpRoot.transform.Find("Text").GetComponent<TextMesh>();
        message = threedtext.text;
    }

    private void Update() {
        // because screw the collision system
        Collider2D collider = GetComponentInChildren<Collider2D>();
        Collider2D playerCollider = Player.Me.GetComponentInChildren<Collider2D>();
        
        if (playerCollider != null && collider.bounds.Intersects(playerCollider.bounds)) {
            t = t + Time.deltaTime;
            helpRoot.SetActive(true);
            threedtext.text = message.Substring(0, (int)Mathf.Min(t * CHAR_PER_SECOND, message.Length));
        } else {
            t = 0f;
            helpRoot.SetActive(false);
        }
    }
}
