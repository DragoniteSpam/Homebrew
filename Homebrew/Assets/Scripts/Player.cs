using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;
    public GameObject bottle;
    public GameObject bottleClone;
    public float attackX;                  //Attack x position
    public float attackY;                  //Attack y position
    private bool aimingMode;
    public float bottlespeed;
    public GameObject launcher;
    public float distance;
    public Transform groundDetection;


    // Use this for initialization
    void Start()
    {
        bottle = (GameObject)Resources.Load("Prefabs/bottle");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + horizontal, -10, 10);
        transform.position = pos;
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x = (horizontal > 0 ? 1 : (horizontal < 0) ? -1 : scale.x);
        GetComponent<Transform>().localScale = scale;

        //get current mouse position in 2d screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        //convert the mouse position to 3d world coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
        //find the delta from the launchPos to the mousePos3d
        Vector3 mouseDelta = mousePos3d - launcher.transform.position;

        if (Input.GetMouseButtonUp(0))
        {
            //the mouse has been released
            //bottleClone.transform.position = 
            aimingMode = false;
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 pvelocity = mouseDelta;
            pvelocity.Normalize();
            bottleClone.GetComponent<Rigidbody2D>().velocity = pvelocity * bottlespeed;
            bottleClone = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //the player has pressed the mouse button down while over the slingshot 
            aimingMode = true;
            //instantiate a projectile
            bottleClone = Instantiate(bottle) as GameObject;
            //start it at launch point
            bottleClone.transform.position = launcher.transform.position;
            //set it to isKinematic for now
            bottleClone.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (!aimingMode) return;

        //limit mouse delta to the radius of the slingshot spherecollider
        float maxmagnitude = launcher.GetComponent<CircleCollider2D>().radius;
        if (mouseDelta.magnitude > maxmagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxmagnitude;
        }
        //move the object to this new position 
        bottleClone.transform.position = launcher.transform.position + mouseDelta;
    }
}

