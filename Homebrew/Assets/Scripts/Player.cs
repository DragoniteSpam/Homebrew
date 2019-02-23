using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed = 4f;
    public float friction = 0.2f;
    public float jump = 12f;
    public GameObject bottle;
    public GameObject bottleClone;
    public float attackX;                  //Attack x position
    public float attackY;                  //Attack y position
    private bool aimingMode;
    public float bottlespeed;
    public float launchRadius;
    public GameObject reticle;

    private float jumpTime;

    // Use this for initialization
    void Start() {
        jumpTime = 0f;

        bottle = (GameObject)Resources.Load("Prefabs/bottle");

        reticle.SetActive(false);

        GetComponent<Homebrew>().Set(Homebrew.FLAG_PLAYER);
    }

    // Update is called once per frame
    void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 scale = transform.localScale;
        scale.x = (horizontal > 0 ? 1 : (horizontal < 0) ? -1 : scale.x);
        transform.localScale = scale;

        /*
         * Move around
         */
        
        Rigidbody2D plugandplay = GetComponent<Rigidbody2D>();

        plugandplay.AddForce(new Vector2(horizontal, 0f), ForceMode2D.Impulse);

        Vector2 velocity = plugandplay.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        bool grounded = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 0.75f), Homebrew.EnvironmentalCollisionMask());
        
        if (grounded) {
            if (Input.GetButtonDown("Jump")) {
                velocity = velocity + Vector2.up * jump;
            }

            velocity = Vector2.Lerp(velocity, Vector2.zero, friction);
        }

        plugandplay.velocity = velocity;

        /*
         * Throw
         */

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3d - transform.position;

        if (Input.GetMouseButtonUp(0)) {
            //the mouse has been released
            //bottleClone.transform.position = 
            aimingMode = false;
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 pvelocity = mouseDelta;
            pvelocity.Normalize();
            bottleClone.GetComponent<Rigidbody2D>().velocity = pvelocity * bottlespeed;
            Interaction.ApplyToBottle(bottleClone, Elements.FIRE, Elements.FIRE);
            bottleClone = null;

            reticle.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) {
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
}

