using UnityEngine;

public class BottleShards : Hazard {
    // Because we can't have nice things . . . LIKE CONSTRUCTORS.
    public static void SpawnBlob(GameObject what) {
        //float r = 0.5f;
        int n = 4;
        for (int i = 0; i < n; i++) {
            //GameObject blob = Instantiate(HomebrewGame.Me.prefabHazardBottleShard, new Vector3(what.transform.position.x + Random.Range(-r, r),
                //what.transform.position.y + Random.Range(-r, r), 0f/*what.transform.position.z*/), Quaternion.identity);
            
            //blob.AddComponent<BottleShards>();
        }
    }

    public override void Interact(GameObject what) {
        // nothing
    }
}
