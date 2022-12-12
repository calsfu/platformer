using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Transform time;
    public float cameraMove = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var playerViewportPosition = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerViewportPosition.y > 1f)
        {
            Camera.main.transform.position += Vector3.up * cameraMove; // move right by move amount
            time.transform.position += Vector3.up * cameraMove;
        }
        else if (playerViewportPosition.y < 0f)
        {
            Camera.main.transform.position += Vector3.down * cameraMove; // move left by move amount
            time.transform.position += Vector3.down * cameraMove;
        }
    }

}