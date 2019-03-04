using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Patrol {

    public float speed;
    private Transform target;

    private bool right;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        right = true;
    }

    // Update is called once per frame
    void Update() {
        if ((target.position.x < transform.position.x && right)^(target.position.x>transform.position.x && !right)) {
            Turn();
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
    }

    protected override void Turn() {
        base.Turn();
        right = !right;
    }
}
