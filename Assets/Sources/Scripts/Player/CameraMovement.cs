using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject Player;
    public float offset = 0.4f;
    public float TopLimitWorld;
    public float RighttLimitWorld;
    public float BottomLimitWorld;
    public float LeftLimitWorld;

    private float TimeShaking = 0;
    private float OldPositionX = 0;
    private float OldPositionY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dTime = Time.deltaTime;
        if (TimeShaking > 0)
            TimeShaking -= dTime;

        FollowCamera();
      
    }

    void FollowCamera()
    {
        float cameraX = OldPositionX;
        float cameraY = OldPositionY;
        if (Player.transform.position.x < RighttLimitWorld && Player.transform.position.x > LeftLimitWorld)
        {
            cameraX = Player.transform.position.x;
            OldPositionX = cameraX;
        }
        if (Player.transform.position.y < TopLimitWorld && Player.transform.position.y > BottomLimitWorld)
        {
            cameraY = Player.transform.position.y + offset;
            OldPositionY = cameraY;
        }

        // шэйк камеры 
        float multiplayShaking = 0.1f;
        if (TimeShaking > 0)
        {
            cameraX += UnityEngine.Random.Range(-TimeShaking * multiplayShaking, TimeShaking * multiplayShaking);
            cameraY += UnityEngine.Random.Range(-TimeShaking * multiplayShaking, TimeShaking * multiplayShaking);
        }

        if (Player != null)
        {
            transform.position = new Vector3(
                cameraX,
                cameraY, 
                transform.position.z
            );
        }
    }

    public void Shaking()
    {
        TimeShaking = 1;

    }
}
