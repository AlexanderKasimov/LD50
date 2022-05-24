using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    //Params
    //[Header("Properties")]
    [SerializeField]
    [Range(0f,8f)]
    private float movementSpeed = 4f;

    [SerializeField]
    private Weapon startingWeaponPrefab;
  
    //public properties
    public StatsModule StatsModule { get; private set; }

    public Weapon CurWeapon { get; private set; }

    //private fields
    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer srCharacter;

    private Vector2 movementInput;

    private CharacterController controller;

    private bool isInputEnabled = true;


    private void Awake()
    {
        srCharacter = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
        StatsModule = GetComponent<StatsModule>();
        SpawnStartingWeapon();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCharacter(Vector2 aimDirection, Vector2 movementInput)
    {
        if (!isInputEnabled)
        {
            this.movementInput = Vector2.zero;
            animator.Play("Idle");
            return;
        }
        //Set movementInput
        this.movementInput = movementInput;

        //Weapon rotation
        if (CurWeapon)
        {
            CurWeapon.UpdateRotation(aimDirection);
        }

        //Rotate sprite    
        if (aimDirection.x <= 0)
        {
            srCharacter.flipX = true;
        }
        else
        {
            srCharacter.flipX = false;
        }

        //Animation   
        if (movementInput.magnitude > 0f)
        {
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle");
        }     

    }

    private void FixedUpdate()
    {
        Vector2 movementVector = movementInput * movementSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movementVector);
    }

    //Weapon management

    private void SpawnStartingWeapon()
    {
        if (!startingWeaponPrefab)
        {
            Debug.Log("No starting Weapon ");
            return;
        }
        CurWeapon = Instantiate(startingWeaponPrefab, transform);       
    }

    public void SwitchWeapon(Weapon weapon)
    {
        Destroy(CurWeapon.gameObject);
        CurWeapon = Instantiate(weapon, transform);
    }


    public void WeaponStartFire()
    {
        if (!CurWeapon)
        {
            return;
        }
        CurWeapon.StartFire();
    }
    public void WeaponStopFire()
    {
        if (!CurWeapon)
        {
            return;
        }
        CurWeapon.StopFire();
    }

    //Helper Functions

    public void ToggleInput(bool isEnabled)
    {
        isInputEnabled = isEnabled;
        controller.enabled = isEnabled;
        WeaponStopFire();       
    }

    public void TeleportToPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Deactivate()
    {
        WeaponStopFire();
        gameObject.SetActive(false);
    }

}
