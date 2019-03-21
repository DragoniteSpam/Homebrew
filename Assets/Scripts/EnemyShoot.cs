using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : /*Patrol*/ Enemy {

    [Header("Stats")]
    public float speed;
    public float stoppingDistance;
    public float nearDistance;
    public float startTimeBetweenShots;

    public float bottleSpeed = 8f;
    private float timeBetweenShots;

    [Header("References")]
    public GameObject shot;
    private Transform player;

    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        //This makes the enemy move
        if (Vector2.Distance(transform.position, player.position) < nearDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        } else if (Vector2.Distance(transform.position, player.position) > stoppingDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > nearDistance) {
            transform.position = transform.position;
        }

        // This makes the enemy shoot
        if (timeBetweenShots <= 0) {
            GameObject fired = Instantiate(shot, transform.position, Quaternion.identity);
            fired.transform.position = transform.position;
            fired.GetComponent<Rigidbody2D>().isKinematic = false;
            //fired.GetComponent<Rigidbody2D>().velocity = pvelocity * bottleSpeed;

            if (player.transform.position.x > transform.position.x) {
                fired.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f) * bottleSpeed;
            } else {
                fired.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0f) * bottleSpeed;
            }

            // I don't like this
            PersistentInteraction.ApplyToBottle(fired, Elements.FIRE, Elements.FIRE, gameObject);

            timeBetweenShots = startTimeBetweenShots;
        } else {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    public override void Kill(GameObject who) {
        base.Kill(who);
    }

    public override void Interact(int potionFlags) {
        base.Interact(potionFlags);
    }
}
