using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShoot : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D body;
    public float Speed;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // set the direction and velocity of the projectile
        Vector3 Direction = player.transform.position - transform.position;
        body.velocity = new Vector2(Direction.x, Direction.y).normalized * Speed;


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // if the projectile is active for more than 5 seconds kill it 
        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    // if projectile collides with a player or the ground destroy it 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
