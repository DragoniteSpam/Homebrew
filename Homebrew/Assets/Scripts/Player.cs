using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 4f;
    public GameObject bottle;
    public GameObject bottleClone;
    public bool facingRight = true;        //Bool determining directionality of punches and sprites
    public float attackX;                  //Attack x position
    public float attackY;                  //Attack y position
    public Vector3 launchPos;
    private bool aimingMode;
    public float bottlespeed;
    public GameObject launchPoint;
    public Transform playerpos;
    public Transform launchPointTrans;


    // Use this for initialization
    void Start() {
        bottle = (GameObject)Resources.Load("Prefabs/bottle");
        playerpos = transform.Find("launcher");
        launchPointTrans = playerpos;
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    // Update is called once per frame
    void Update() {
        launchPos = playerpos.position;
        launchPoint = playerpos.gameObject;

        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + horizontal, -6, 6);
        transform.position = pos;

        if ((horizontal > 0) && facingRight)
            FlipX();
        else if ((horizontal < 0) && !facingRight)
            FlipX();

        if (!aimingMode) return;
        //get current mouse position in 2d screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        //convert the mouse position to 3d world coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
        //find the delta from the launchPos to the mousePos3d
        Vector3 mouseDelta = mousePos3d - launchPos;
        //limit mouse delta to the radius of the slingshot spherecollider
        float maxmagnitude = this.GetComponent<CircleCollider2D>().radius;
        if (mouseDelta.magnitude > maxmagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxmagnitude;
        }
        //move the object to this new position 
        Vector3 projPos = launchPos + mouseDelta;
        bottleClone.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)) {
            //the mouse has been released
            //bottleClone.transform.position = 
            aimingMode = false;
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = false;
            bottleClone.GetComponent<Rigidbody2D>().velocity = mouseDelta * bottlespeed;
            bottleClone = null;


        }

    }
    void OnMouseEnter() {
        //print("Slingshot:OnMouseEnter() ");
        launchPoint.SetActive(true);

    }

    void OnMouseExit() {
        //print("Slingshot:OnMouseExit() ");
        launchPoint.SetActive(false);
    }
    void OnMouseDown() {
        //the player has pressed the mouse button down while over the slingshot 
        aimingMode = true;
        //instantiate a projectile
        bottleClone = Instantiate(bottle) as GameObject;
        //start it at launch point
        bottleClone.transform.position = launchPos;
        //set it to isKinematic for now
        bottleClone.GetComponent<Rigidbody>().isKinematic = true;
    }
    void FlipX() {
        facingRight = !facingRight;
        GetComponent<Transform>().Rotate(new Vector3(0f, 1f, 0f), 180f);
    }
}

