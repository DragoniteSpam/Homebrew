using UnityEngine;

public class HazardMud : Hazard {
    public static void SpawnBlob(GameObject what) {
        float r = 0.5f;
        int n = 16;
        for (int i = 0; i < n; i++) {
            Instantiate(HomebrewGame.Me.prefabMud, new Vector3(what.transform.position.x + Random.Range(-r, r),
                what.transform.position.y + Random.Range(-r, r), what.transform.position.z), Quaternion.identity).
                GetComponent<SpriteRenderer>().sprite = HomebrewGame.Me.spritesMud[Random.Range(0, HomebrewGame.Me.spritesMud.Count - 1)];
        }
    }
}
