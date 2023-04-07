using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform[] turnPoints;
    public float Speed;
    public int Destination;

    // Update is called once per frame
    void Update()
    {
        // if the zombie is moving towards the first point
        if (Destination == 0)
        {
            // Position updates to move towards target
            transform.position = Vector2.MoveTowards(transform.position, turnPoints[0].position, Speed * Time.deltaTime);

            // if the distance to target is small flip the sprite and change destination
            if (Vector2.Distance(transform.position, turnPoints[0].position) <= 0.2f)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 1);
                Destination = 1;
            }
        }
        // if the zombie is moving towards the second point
        if (Destination == 1)
        {
            // Position updates to move towards target
            transform.position = Vector2.MoveTowards(transform.position, turnPoints[1].position, Speed * Time.deltaTime);

            // if the distance to target is small flip the sprite and change destination
            if (Vector2.Distance(transform.position, turnPoints[1].position) <= 0.2f)
            {
                transform.localScale = new Vector3(-2.5f, 2.5f, 1);
                Destination = 0;
            }
        }
    }
}
