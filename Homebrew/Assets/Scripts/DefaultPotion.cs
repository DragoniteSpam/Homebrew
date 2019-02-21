using UnityEngine;

namespace external {
    public class DefaultPotion : MonoBehaviour {
        void OnCollisionEnter(Collision other) {
            if (other.gameObject.GetComponent<DefaultPotion>() == null) {
                return;
            }

            // i guess you could make an explosion effect when you collide with the floor but start with this
        }
    }
}
