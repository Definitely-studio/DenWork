using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    public Animator crosshairAnimator;
    public Camera mainCamera;
    public float translationSpeed = 5;
    private float shootingAnimationsSpeed {get; set;}

    // Start is called before the first frame update
    void Start()
    {
      mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
      crosshairAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      SetCrosshairPosition();
    }

    void FixedUpfate()
    {

    }

    public void SetCrosshairPosition()
    {
      if(mainCamera != null)
      {
        transform.position = Vector3.MoveTowards( transform.position,new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,transform.position.z), translationSpeed * Time.deltaTime);
        //transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,transform.position.z);
      }


    }

    public void SetShootingAnimationsSpeed(float speed)
    {
      shootingAnimationsSpeed = speed;
      crosshairAnimator.SetFloat("shootingAnimationsSpeed", speed);
    }

    public void PlayShootingAnimate()
    {
      if(crosshairAnimator.GetBool("Reload") != true)
        crosshairAnimator.SetTrigger("Shoot");
    }
    public void ResetShootingAnimate()
    {
        crosshairAnimator.ResetTrigger("Shoot");
    }

    public void StopShootingAnimate()
    {

    }

    public void PlayReloadingAnimate()
    {
      crosshairAnimator.SetBool("Reload", true);
    }



    public void StopReloadingAnimate()
    {
      crosshairAnimator.SetBool("Reload", false);
    }


}
