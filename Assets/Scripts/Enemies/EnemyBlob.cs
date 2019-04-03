using UnityEngine;

public class EnemyBlob : Enemy {
    private float t;
    private float tjump;
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();

        t = 0f;
        tjump = 0f;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        t = t + Time.deltaTime;
        if (t > (tjump + 2f)) {
            tjump = t;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
        }
    }
}
