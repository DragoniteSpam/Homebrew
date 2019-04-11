using UnityEngine;

public class HazardMagma : Hazard {
    // Because we can't have nice things . . . LIKE CONSTRUCTORS.
    public static void SpawnBlob(GameObject what) {
        float r = 0.5f;
        int n = 16;
        for (int i = 0; i < n; i++) {
            GameObject blob = Instantiate(HomebrewGame.Me.prefabHazard, new Vector3(what.transform.position.x + Random.Range(-r, r),
                what.transform.position.y + Random.Range(-r, r), 0f/*what.transform.position.z*/), Quaternion.identity);
            blob.GetComponent<SpriteRenderer>().sprite = HomebrewGame.Me.spritesMud[Random.Range(0, HomebrewGame.Me.spritesMud.Count - 1)];
            blob.GetComponent<SpriteRenderer>().material.color = Color.red;

            blob.AddComponent<HazardMagma>();
        }
    }

    public override void Interact(GameObject what) {
        what.GetComponent<Responsive>().Burn();
    }
}
