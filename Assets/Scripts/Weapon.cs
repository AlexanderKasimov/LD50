using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float RPM = 300f;
    [SerializeField]
    private float damage = 10f;

    private float timeSinceFire;

    private bool isFiring = false;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private SpriteRenderer srWeapon;

    private void Awake() 
    {
        timeSinceFire = 60f / RPM;

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update timeSinceFire
        timeSinceFire += Time.deltaTime;

    }
    
    public void StartFire()
    {
        if (CanFire())
        {
            isFiring = true;
            InvokeRepeating("Fire", 0f, 60f / RPM);
        }
    }

    public void StopFire()
    {
        isFiring = false;
        CancelInvoke("Fire");
    }

    private void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        projectile.Init(transform.right, damage);
        timeSinceFire = 0f;
    }

    private bool CanFire()
    {
        return !isFiring && (timeSinceFire >= 60f / RPM);
    }



    public void UpdateRotation(Vector2 lookDirection)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, lookDirection));
        float angleLookDirection = Vector2.Angle(Vector2.right, lookDirection);
        if (angleLookDirection > 90f && angleLookDirection <= 270f)
        {
            srWeapon.flipY = true;
        }
        else
        {
            srWeapon.flipY = false;
        }


    }
}
