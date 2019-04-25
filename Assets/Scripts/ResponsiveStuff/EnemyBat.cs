using UnityEngine;

public class EnemyBat : Enemy {

    [Header("Set in Inspector")]

    // Prefab for instantiating apples

    public GameObject venomPrefab;

    // Speed at which the Bat moves

    public float speed = 1f;

    // Distance where Bat turns around

    public float leftAndRightEdge = 10f;

    // Chance that the Bat will change directions

    public float chanceToChangeDirections = 0.025f;

    // Rate at which Venom will be instantiated

    public float secondsBetweenVenomDrops = 1f;


    void Start()
    {

        // Dropping venom every second
        Invoke("DropVenom", 2f);
    }

    void DropVenom()
    {
        HazardVenom.SpawnBlob(gameObject);
        Invoke("DropVenom", secondsBetweenVenomDrops);
    }

    protected override void Update()
    {
        base.Update();

        // Basic Movement

        Vector2 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        /*if (pos.x < -leftAndRightEdge)
        {

            speed = Mathf.Abs(speed); // Move right

        }
        else if (pos.x > leftAndRightEdge)
        {

            speed = -Mathf.Abs(speed); // Move left                    

        }
        else if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change direction
        }*/
    }

    void FixedUpdate()
    {

        // Changing Direction Randomly is now time-based because of FixedUpdate()

        if (Random.value < chanceToChangeDirections)
        {                     

            speed *= -1; // Change direction

        }

    }
}
