using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HomebrewFlags))]
public class Player : MonoBehaviour {
    private const float DASH_THRESHOLD = 0.5f;
    private const float MOVE_THRESHOLD = 0.1f;
    private const float DASH_MULTIPLIER = 2f;
    private const float SELECTOR_TIME_SCALE = 0.5f;

    public float maxSpeed = 4f;
    public float friction = 0.2f;
    public float jump = 4f;
    public GameObject bottle;
    public float attackX;                  //Attack x position
    public float attackY;                  //Attack y position
    private bool aimingMode;
    public float bottlespeed;
    public float launchRadius;
    public GameObject reticle;

    public GameObject selectorOverlay;

    public Elements element0 = Elements.FIRE;
    public Elements element1 = Elements.NONE;

    private float timeSinceJump;
    private float timeToDash;
    private bool dashing;
    private bool autodashing;

    private bool[] ownedElements;
    private int[] elementMap;
    private GameObject[] quadrants;
    private Text[] quadrantText;

    // Use this for initialization
    void Awake() {
        if (Me == null) {
            Me = this;
        } else {
            throw new System.Exception("Can't have two players at once");
        }

        elementMap = new int[4] {
            (int)Elements.NONE, (int)Elements.FIRE, (int)Elements.WATER, (int)Elements.EARTH
        };

        ownedElements = new bool[(int)Elements.SIZE] {
            true, false, true, false, true, false, true
        };

        quadrants = new GameObject[4] {
            selectorOverlay.transform.Find("Ring 0").gameObject,
            selectorOverlay.transform.Find("Ring 1").gameObject,
            selectorOverlay.transform.Find("Ring 2").gameObject,
            selectorOverlay.transform.Find("Ring 3").gameObject
        };

        quadrantText = new Text[4] {
            selectorOverlay.transform.Find("Text 0").GetComponent<Text>(),
            selectorOverlay.transform.Find("Text 1").GetComponent<Text>(),
            selectorOverlay.transform.Find("Text 2").GetComponent<Text>(),
            selectorOverlay.transform.Find("Text 3").GetComponent<Text>()
        };

        timeSinceJump = 0f;
        timeToDash = 0f;
        dashing = false;
        autodashing = false;

        bottle = (GameObject)Resources.Load("Prefabs/bottle");

        reticle.SetActive(false);

        GetComponent<HomebrewFlags>().Set(Elements.PLAYER);
    }

    // Update is called once per frame
    void FixedUpdate() {
        // invincibility

        if (IFrames > 0) {
            IFrames = Mathf.Max(IFrames - Time.deltaTime, 0f);
            // there should be some graphical indication of this
        }

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

            Vector3 pvelocity = mouseDelta;
            pvelocity.Normalize();

            GameObject bottleClone = Instantiate(bottle);

            bottleClone.transform.position = transform.position;
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = false;
            bottleClone.GetComponent<Rigidbody2D>().velocity = pvelocity * bottlespeed;

            bottleClone.transform.position = reticle.transform.position;

            PersistentInteraction.ApplyToBottle(bottleClone, element0, element1, gameObject);

            reticle.SetActive(false);
        }

        if (Input.GetButtonDown("Potion Chuck")) {
            //the player has pressed the mouse button down while over the slingshot 
            aimingMode = true;

            reticle.SetActive(true);
        }

        if (aimingMode) {
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
        }

        /*
         * Potions menu
         */

        if (Input.GetButtonDown("Potion Menu")) {
            bool currentlyShown = !selectorOverlay.activeInHierarchy;
            selectorOverlay.SetActive(currentlyShown);
            Time.timeScale = currentlyShown ? SELECTOR_TIME_SCALE : 1f;

            for (int i = 0; i < 4; i++) {
                Text text = quadrantText[i];
                if (ownedElements[elementMap[i]]) {
                    text.text = PersistentInteraction.Me.elementNames[elementMap[i]];
                } else {
                    text.text = "None";
                }
            }
        }

        if (selectorOverlay.activeInHierarchy) {
            /*
             * Bottom left: (0, 0); top right: (W, H)
             */
            
            Vector2 position = new Vector2((Input.mousePosition.x / Screen.width) - 0.5f, (Input.mousePosition.y / Screen.height) - 0.5f);

            foreach (GameObject what in quadrants) {
                what.SetActive(false);
            }

            if (position.magnitude > 0.1f) {
                int quadrant;
                if (position.x > 0f) {
                    // upper right
                    if (position.y > 0f) {
                        quadrant = 0;
                    // lower right
                    } else {
                        quadrant = 1;
                    }
                } else {
                    // upper left
                    if (position.y > 0f) {
                        quadrant = 3;
                    // lower left
                    } else {
                        quadrant = 2;
                    }
                }

                quadrants[quadrant].SetActive(true);
            }
        }
    }

    private void Jump() {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity = velocity + Vector2.up * jump;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public static Player Me {
        get; private set;
    }

    public float IFrames {
        get; set;
    }

    public void AutoIFrames() {
        // this is completely arbitrary
        IFrames = 1f;
    }
}
