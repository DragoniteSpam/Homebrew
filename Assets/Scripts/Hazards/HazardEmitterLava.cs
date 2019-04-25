using UnityEngine;

public class HazardEmitterLava : MonoBehaviour {
    public float period = 2f;

    private float t;
	void Awake() {
        Blob();
    }

	void Update () {
        t = t - Time.deltaTime;

        if (t <= 0) {
            Blob();
        }
	}

    private void Blob() {
        HazardMagma.SpawnBlob(gameObject);
        t = period;
    }
}
