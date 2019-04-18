using UnityEngine;

public class HazardSteam : Hazard {
    private Vector2 velocity;
    private float angularVelocity;

    // Because we can't have nice things . . . LIKE CONSTRUCTORS.
    public static void SpawnBlob(GameObject what) {
        float r = 0.01f;
        int n = 32;
        for (int i = 0; i < n; i++) {
            Vector2 offset = new Vector2(Random.Range(-r, r), Random.Range(-r, r));
            GameObject blob = Instantiate(HomebrewGame.Me.prefabHazardGassy, new Vector3(what.transform.position.x/* + offset.x*/,
                what.transform.position.y/* + offset.y*/, 0f), Quaternion.identity);
            blob.GetComponentInChildren<SpriteRenderer>().sprite = HomebrewGame.Me.spritesSteam[Random.Range(0, HomebrewGame.Me.spritesSteam.Count - 1)];

            //Rigidbody2D body = blob.GetComponent<Rigidbody2D>();
            //body.AddForce(offset * 2f/* / 1.5f*/, ForceMode2D.Impulse);
            //body.angularVelocity = Random.Range(-2f, 2f);

            HomebrewFlags flags = blob.AddComponent<HomebrewFlags>();
            flags.Set(Elements.STEAM);

            HazardSteam nova = blob.AddComponent<HazardSteam>();
            nova.velocity = offset;
            nova.angularVelocity = Random.Range(-0.4f, 0.4f);
        }
    }

    protected override void Update() {
        base.Update();

        Renderer renderer = GetComponentInChildren<Renderer>();

        transform.Translate(velocity);
        velocity = velocity * 0.99f;
        renderer.transform.Rotate(0f, 0f, angularVelocity);

        if (lifetime < 1f) {
            Color c = renderer.material.color;
            c.a = lifetime;
            renderer.material.color = c;
        }
    }

    public override void Interact(GameObject what) {
    }
}
