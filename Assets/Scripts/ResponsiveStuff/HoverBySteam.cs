using UnityEngine;

[RequireComponent(typeof(HomebrewFlags))]
public class HoverBySteam : Responsive {
    private const float TOTAL_HOVER_TIME = 10f;
    private const float MOVE_TOWARDS_SPEED = 2.5f;
    private const float SINE_MAGNITUDE = 0.08f;
    private const float SINE_PERIOD = 4f;

    private Vector2 basePosition;
    private Vector2 targetPosition;
    private float activeTime = 0f;

    private float t = 0f;

    protected override void Awake() {
        base.Awake();

        basePosition = transform.position;
        
        Transform target = transform.Find("MoveToHere").transform;
        targetPosition = target.position;


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

        t = t + Time.deltaTime;

        Transform rendererPosition = transform.Find("Renderer");
        Vector3 vec3 = rendererPosition.localPosition;
        vec3.y = SINE_MAGNITUDE * Mathf.Sin(t * Mathf.PI * 2f / SINE_PERIOD);
        rendererPosition.localPosition = vec3;
    }

    public override void OnStay(int otherFlags) {
        Activate();
    }

    public override void Interact(int potionFlags) {
    }

    private void Activate() {
        activeTime = TOTAL_HOVER_TIME;
    }
}
