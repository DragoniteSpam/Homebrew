using UnityEngine;

public class FloatingText : MonoBehaviour {
    private const float INITIAL_VELOCITY = 2f;      // this is technically a lie

    public float lifespan;
    public float fadeTime;
    private float t;
    
	void Awake () {
        t = 0;

        Rigidbody2D physics = GetComponent<Rigidbody2D>();
        physics.velocity = new Vector2(Random.Range(-INITIAL_VELOCITY, INITIAL_VELOCITY), INITIAL_VELOCITY);
        physics.gravityScale = 0.5f;
	}
	
	void Update () {
        t = t + Time.deltaTime;
        if (t > lifespan) {
            Destroy(gameObject);
        } else if (t >= lifespan - fadeTime) {
            TextMesh text = GetComponent<TextMesh>();
            Color color = text.color;
            color.a = ((lifespan - fadeTime) / t);
            text.color = color;
        }
	}
}
