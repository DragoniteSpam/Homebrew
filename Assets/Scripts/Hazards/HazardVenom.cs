using UnityEngine;

public class HazardVenom : Hazard {
    // Because we can't have nice things . . . LIKE CONSTRUCTORS.
    public static void SpawnBlob(GameObject what) {
        GameObject blob = Instantiate(HomebrewGame.Me.prefabHazardVenom, new Vector3(what.transform.position.x,
            what.transform.position.y, 0f), Quaternion.identity);

        blob.AddComponent<HazardVenom>();
    }
    public override void Interact(GameObject what) {
        Player player = what.GetComponent<Player>();
        if (player != null) {
            //what.GetComponent<Responsive>().Damage(1, parent);
            // this should work for *any* non-parent responsive that it hits but there's no time for that
            player.Damage(1);
            Destroy(gameObject);
        }
    }
}
