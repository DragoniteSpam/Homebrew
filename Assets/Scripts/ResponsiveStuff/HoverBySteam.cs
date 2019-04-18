using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class HoverBySteam : Responsive {
    private const float TOTAL_HOVER_TIME = 10f;
    private const float MOVE_TOWARDS_SPEED = 2.5f;

    private Vector2 basePosition;
    private Vector2 targetPosition;
    private float activeTime = 0f;

    protected override void Awake() {
        base.Awake();

        basePosition = transform.position;
        
        Transform target = transform.Find("MoveToHere").transform;
        targetPosition = target.position;

        // you don't need the target object after you've grabbed its position
        Destroy(target.gameObject);
    }

    protected override void Update() {
        base.Update();

        if (activeTime > 0f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MOVE_TOWARDS_SPEED * Time.deltaTime);
            activeTime = Mathf.Max(activeTime - Time.deltaTime, 0f);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, basePosition, MOVE_TOWARDS_SPEED * Time.deltaTime);
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D other) {
        HomebrewFlags flagData = other.gameObject.GetComponent<HomebrewFlags>();
        if (flagData != null && flagData.Get(Elements.STEAM)) {
            Activate();
        }
    }

    public override void Interact(int potionFlags) {
    }

    private void Activate() {
        activeTime = TOTAL_HOVER_TIME;
    }
}
