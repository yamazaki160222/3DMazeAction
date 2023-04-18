using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon_Y : MonoBehaviour
{
    
    public GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        FollowCamera();
        RotateCamera();
    }
    private void FollowCamera()
    {
        transform.position = player.transform.position + offset;
    }
    private void RotateCamera()
    {
        transform.Rotate(0, player.transform.position.y, 0);
    }
}
