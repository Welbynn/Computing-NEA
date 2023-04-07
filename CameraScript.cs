using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 CameraOffset;
    void Start()
    {
        // Get's distance between player and camera positions
        CameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        // Set's camera position equal to the player position plus the offset calculated in start
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z) + CameraOffset;
    }
}
