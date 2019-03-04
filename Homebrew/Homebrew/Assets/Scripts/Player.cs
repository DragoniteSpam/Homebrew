using UnityEngine;

public class Player : MonoBehaviour {
    private const float DASH_THRESHOLD = 0.5f;
    private const float MOVE_THRESHOLD = 0.1f;
    private const float DASH_MULTIPLIER = 2f;

    public float maxSpeed = 4f;
    public float friction = 0.2f;
    public float jump = 4f;
    public GameObject bottle;
    public GameObject bottleClone;
    public float attackX;                  //Attack x position
    public float attackY;                  //Attack y position
    private bool aimingMode;
    public float bottlespeed;
    public float launchRadius;
    public GameObject reticle;

    private float timeSinceJump;
    private float timeToDash;
    private bool dashing;
    private bool autodashing;

    // Use this for initialization
    void Start() {
        timeSinceJump = 0f;
        timeToDash = 0f;
        dashing = false;
        autodashing = false;

        bottle = (GameObject)Resources.Load("Prefabs/bottle");

        reticle.SetActive(false);

        GetComponent<HomebrewFlags>().Set(HomebrewFlags.FLAG_PLAYER);
    }

    // Update is called once per frame
    void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");

        /*
         * Running
         */
        
        if (Mathf.Abs(horizontal) > MOVE_THRESHOLD) {
            if (Input.GetButtonDown("Run")) {
                if (timeToDash > 0f) {
                    autodashing = true;
                } else {
                    timeToDash = DASH_THRESHOLD;
                }
            }

            if (Input.GetButton("Run")) {
                dashing = true;
                timeToDash = DASH_THRESHOLD;
            } else {
                dashing = false;
            }
        }

        timeToDash = Mathf.Max(0f, timeToDash - Time.deltaTime);

        /*
         * Move around
         */

        if (Mathf.Abs(horizontal) < MOVE_THRESHOLD) {
            dashing = false;
            autodashing = false;
        }

        Vector3 scale = transform.localScale;
        scale.x = (horizontal > 0 ? 1 : (horizontal < 0) ? -1 : scale.x);
        transform.localScale = scale;

        float f = 1;

        if (dashing || autodashing) {
            f = DASH_MULTIPLIER;
        }

        Rigidbody2D plugandplay = GetComponent<Rigidbody2D>();
        
        plugandplay.AddForce(new Vector2(horizontal * f, 0f), ForceMode2D.Impulse);

        Vector2 velocity = plugandplay.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed * f, maxSpeed * f);

        bool grounded = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 0.75f), HomebrewFlags.EnvironmentalCollisionMask());

        if (grounded) {
            if (Input.GetButtonDown("Jump")) {
                timeSinceJump = 0f;
            }
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, friction);
        }

        timeSinceJump = timeSinceJump + Time.deltaTime;

        plugandplay.velocity = velocity;

        // after the velocity has been set, process jumping. this re-reads and resets the velocity so if
        // you do it before it's set above it'll get overwritten.

        if (timeSinceJump < 0.15f && Input.GetButton("Jump")) {
            plugandplay.gravityScale = 0f;
            Jump();
        } else {
            plugandplay.gravityScale = 1f;
        }

        /*
         * Throw
         */

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3d - transform.position;

        if (Input.GetButtonUp("Potion Chuck")) {
            aimingMode = false;
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 pvelocity = mouseDelta;
            pvelocity.Normalize();
            bottleClone.GetComponent<Rigidbody2D>().velocity = pvelocity * bottlespeed;
            Interaction.ApplyToBottle(bottleClone, Elements.FIRE, Elements.FIRE);
            bottleClone = null;

            reticle.SetActive(false);
        }

        if (Input.GetButtonDown("Potion Chuck")) {
            //the player has pressed the mouse button down while over the slingshot 
            aimingMode = true;
            //instantiate a projectile
            bottleClone = Instantiate(bottle) as GameObject;
            //start it at launch point
            bottleClone.transform.position = transform.position;
            //set it to isKinematic for now
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = true;

            reticle.SetActive(true);
        }

        if (!aimingMode) return;
        
        //limit mouse delta to the radius of the slingshot spherecollider
        float maxmagnitude = launchRadius;

        Vector3 absMouseDelta = mouseDelta;
        absMouseDelta.Normalize();
        absMouseDelta = absMouseDelta * maxmagnitude;

        if (mouseDelta.magnitude > maxmagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxmagnitude;
        }

        reticle.transform.position = transform.position + absMouseDelta;

        //move the object to this new position 
        bottleClone.transform.position = transform.position + mouseDelta;
    }

    private void Jump() {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity = velocity + Vector2.up * jump;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
