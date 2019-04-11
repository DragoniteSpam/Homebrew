using UnityEngine;

public class HazardSteam : Hazard {
    // Because we can't have nice things . . . LIKE CONSTRUCTORS.
    public static void SpawnBlob(GameObject what) {
        float r = 0.5f;
        int n = 32;
        for (int i = 0; i < n; i++) {
            Vector2 offset = new Vector2(Random.Range(-r, r), Random.Range(-r, r));
            GameObject blob = Instantiate(HomebrewGame.Me.prefabHazardGassy, new Vector3(what.transform.position.x + offset.x,
                what.transform.position.y + offset.y, 0f), Quaternion.identity);
            blob.GetComponentInChildren<SpriteRenderer>().sprite = HomebrewGame.Me.spritesSteam[Random.Range(0, HomebrewGame.Me.spritesSteam.Count - 1)];

            Rigidbody2D body = blob.GetComponent<Rigidbody2D>();
            body.AddForce(offset / 2f, ForceMode2D.Impulse);
            body.angularVelocity = Random.Range(-5f, 5f);

            blob.AddComponent<HazardSteam>();
        }
    }

    public override void Interact(GameObject what) {
    }
}
