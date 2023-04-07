using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public PlayerMovement playerMovement;
    public CapsuleCollider2D capsuleCollider;
    public Rigidbody2D body;

    private bool IsInvincible = false;
    [SerializeField] private float ImmunityTime;

    public AudioClip EnemyHit;
    public AudioClip EnemyKill;

    public SliderValues SliderFloats;

    // Start is called before the first frame update
    void Start()
    {
        // on load health is set to max
        health = maxHealth;
    }

    // controls how long player is invincible for after they are hit
    private IEnumerator Immunity()
    {
        // make player invincible, wait for set time then remove invincibility
        IsInvincible= true;
        yield return new WaitForSeconds(ImmunityTime);
        IsInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
       // kill player if health drops to 0
       if (health <= 0)
       {
            KillPlayer();
       }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if collides with spike damage the player and launch them upwards
        if (collider.tag == "Spike" && !IsInvincible)
        {

            health -= 1;
            body.velocity = new Vector2(body.velocity.x, 10f);
            AudioSource.PlayClipAtPoint(EnemyHit, body.position, SliderFloats.soundValue);
            StartCoroutine(Immunity());

        }
        // if collides with enemy
        else if (collider.tag == "Enemy")
        {
            // if player is falling kill the enemy and don't damage the player
            if (body.velocity.y < -3)
            {
                AudioSource.PlayClipAtPoint(EnemyKill, body.position, SliderFloats.soundValue);
                Destroy(collider.gameObject);
                body.velocity = new Vector2(body.velocity.x, 10f);

            }
            // otherwise damage player and knock them back
            else if (collider.tag == "Enemy" && !IsInvincible)
            {
                health -= 1;
                AudioSource.PlayClipAtPoint(EnemyHit, body.position, SliderFloats.soundValue);

                // if the player is to the left of the enemy knock them left
                if (collider.transform.position.x >= body.transform.position.x)
                {
                    playerMovement.playerSpeed = -10f;
                    body.velocity = new Vector2(-10f, 5f);
                }
                // otherwise knock them right
                else
                {
                    playerMovement.playerSpeed = 10f;
                    body.velocity = new Vector2(10f, 5f);
                }
                StartCoroutine(Immunity());
            }
        }
        // if collides with a projectile damage the player
        else if (collider.tag == "Projectile")
        {
            health -= 1;
            AudioSource.PlayClipAtPoint(EnemyHit, body.position, SliderFloats.soundValue);
            StartCoroutine(Immunity());
        }
        // if collides with a fallbox kill the player instantly
        else if (collider.tag == "Fallbox")
        {
            KillPlayer();
        }

    }

    private void KillPlayer()
    {
        // if player has a saved respawn point respawn the player there
        if (playerMovement.respawnPoint != Vector3.zero)
        {
            body.velocity = Vector3.zero;
            body.transform.position = playerMovement.respawnPoint;
            health = 3;
        }
        // if not respawn them at the start of the level
        else
        {
            body.velocity = Vector3.zero;
            body.transform.position = playerMovement.spawnPoint;
            health = 3;
        }     
    }
}
