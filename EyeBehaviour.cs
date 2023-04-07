using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{
    public Rigidbody2D body;
    [SerializeField] public float shootCooldown;
    public bool canShoot = true;
    public GameObject flameProj;
    public Transform shootPos;
    public AudioClip EyeShoot;

    public SliderValues SliderFloats;

    // Controls cooldown on the projectile shooting
    private IEnumerator ProjCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // gets the angle between the player and the eye
        Vector2 direction = transform.position - body.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // sets rotation of the eye using quaternion function
        transform.rotation = Quaternion.Euler(angle * Vector3.forward);

        float distance = Vector2.Distance(transform.position, body.position);

        // if distance is less than threshold and there is no cooldown shoot at the player
        if (distance < 5f && canShoot)
        {
            AudioSource.PlayClipAtPoint(EyeShoot, transform.position, SliderFloats.soundValue);
            Shoot();
            StartCoroutine(ProjCooldown());
        }
    }

    // instantiates a new projectile game object
    void Shoot()
    {
        Instantiate(flameProj, shootPos.position, Quaternion.identity);
    }
}
