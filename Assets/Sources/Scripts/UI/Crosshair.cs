using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField] private Input _input;
    public Animator crosshairAnimator;
    public Camera mainCamera;
    public float translationSpeed = 5;
    private float shootingAnimationsSpeed {get; set;}


    void  Awake() {
      
      _input = new Input();
      mainCamera =  Camera.main;
      
    }

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

     private void OnEnable()
    {
        _input.Enable();
    }

    public void SetCrosshairPosition()
    {
      if(mainCamera != null)
      {
        
        Vector2 AimPosition = _input.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(AimPosition);
        transform.position = Vector2.MoveTowards( transform.position, new Vector2(mouseWordPosition.x, mouseWordPosition.y), translationSpeed * Time.deltaTime);

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
