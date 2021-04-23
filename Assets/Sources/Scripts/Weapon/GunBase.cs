using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    //first use right mouse button for changing mode
    private bool button = false;
    private bool isReloading = false;
    private bool Canshot = true;

    public Transform barrel;
	public GameObject bullet;    
    // статы разных типов оружия
    public Animator muzzleFlashAnimator;
    public AudioSource AudioShoot;
    public AudioSource AudioReload;
    public Crosshair crosshair;

	public float reloadingTime = 5f;
	public int MaxBulletsInMagazine = 6;
	private int currentBulletsInMagazine;
	public float shootingSpeed = 60f;



    // Start is called before the first frame update
    void Start()
    {
      muzzleFlashAnimator = GetComponent <Animator> ();
      currentBulletsInMagazine = MaxBulletsInMagazine;
    }



    public void Shoot()
    {
        // если сейчас не кулдаун
         if(Canshot)
         {
            //если есть патроны
            if (currentBulletsInMagazine > 0){

            if(crosshair != null)
              crosshair.PlayShootingAnimate();

            AudioShoot.Play();

          // создаем проджектайл пули
                Instantiate (bullet, barrel.position, barrel.rotation);



            muzzleFlashAnimator.SetTrigger("Shoot");

            currentBulletsInMagazine = currentBulletsInMagazine - 1;
            //запускаем кулдаун
            StartCoroutine(shootingCooldown(60/shootingSpeed));

            //если патронов нет, то перезаряжаем
            if (currentBulletsInMagazine <= 0 && !isReloading){

                StartCoroutine(reloading(reloadingTime));
            }

        }

      }
    }

    public IEnumerator shootingCooldown(float waitTime)
    {
        Canshot = false;
        yield return new WaitForSeconds(waitTime);
        Canshot = true;

    }

    public IEnumerator reloading(float waitTime)
    {
      if(crosshair != null)
      {
        crosshair.PlayReloadingAnimate();
        crosshair.ResetShootingAnimate();
      }

      if(AudioReload != null)
        AudioReload.Play();

      isReloading = true;
      yield return new WaitForSeconds(waitTime);
      currentBulletsInMagazine = MaxBulletsInMagazine;
      isReloading = false;

      if(crosshair != null)
        crosshair.StopReloadingAnimate();

    }

    public IEnumerator AbilityTimer(float waitTime)
    {
      yield return new WaitForSeconds(waitTime);
    }

}
