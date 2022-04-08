using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float RPM = 300f;
    [SerializeField]
    private float ATK = 10f;

    [SerializeField]
    private float spreadAngle = 5f;

    private float timeSinceFire;

    private bool isFiring = false;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private SpriteRenderer srWeapon;

    [SerializeField]
    private bool isDebugEnabled = false;

    //Recoil Params
    [Header("Recoil")]
    [SerializeField]
    private ScriptableObjectAnimCurve recoilPositionXCurve;

    [SerializeField]
    private ScriptableObjectAnimCurve recoilRotationCurve;

    [SerializeField]
    private GameObject artObject;

    [SerializeField]
    private float recoilDurationMultiplier = 1f;
    [SerializeField]
    private float recoilPositionXMultiplier = 1f;
    [SerializeField]
    private float recoilRotationMultiplier = 1f;


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
        projectile.Init(CalculateSpread(), ATK);
        timeSinceFire = 0f;
        StartCoroutine("PlayRecoil");
    }

    private Vector2 CalculateSpread()
    {
        float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
        Vector2 resultVector = Quaternion.Euler(0f, 0f, randomAngle) * transform.right;
        return resultVector.normalized;
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

    private IEnumerator PlayRecoil()
    {
    
        if (!artObject)
        {
            yield break;
        }
        float time = 0f;
        float recoilDuration = 60f / RPM * recoilDurationMultiplier;
        // Debug.Log("Play Recoil:" + recoilDuration);
        while (time < recoilDuration)
        {
            // Debug.Log("Rotation:" + Quaternion.Euler(0f, 0f, recoilRotationCurve.animationCurve.Evaluate(time / recoilDuration) * recoilRotationMultiplier));
            // Debug.Log("Position:" + new Vector3(recoilPositionXCurve.animationCurve.Evaluate(time / recoilDuration) * recoilPositionXMultiplier, 0f, 0f));
            artObject.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sign(transform.right.x)* recoilRotationCurve.animationCurve.Evaluate(time / recoilDuration) * recoilRotationMultiplier);
            artObject.transform.localPosition= new Vector3(recoilPositionXCurve.animationCurve.Evaluate(time / recoilDuration) * recoilPositionXMultiplier, 0f, 0f);
            time += Time.deltaTime;
            yield return null;
        }
        //Reset for safety
        artObject.transform.localRotation = Quaternion.identity;
        artObject.transform.localPosition = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (!isDebugEnabled)
        {
            return;
        }
        float linesLength = 10f;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(muzzle.transform.position, muzzle.transform.position + transform.right * linesLength);
        Vector2 spredVectorMin = Quaternion.Euler(0f, 0f, -spreadAngle / 2) * transform.right;
        Vector2 spredVectorMax = Quaternion.Euler(0f, 0f, spreadAngle / 2) * transform.right;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(muzzle.transform.position, (Vector2)muzzle.transform.position + spredVectorMin * linesLength);
        Gizmos.DrawLine(muzzle.transform.position, (Vector2)muzzle.transform.position + spredVectorMax * linesLength);
    }
}
