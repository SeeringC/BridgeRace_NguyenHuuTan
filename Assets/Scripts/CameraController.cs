using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    public Vector3 offset;

    private void Start()
    {
        transform.position = Player.transform.position;
    }
    void FixedUpdate()
    {
        transform.position = Player.transform.position + offset;
        transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
    }
}
