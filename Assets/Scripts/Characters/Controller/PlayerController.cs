using UnityEngine;

public class PlayerController : CharacterController
{

    private Vector2 movementInput;
  
    private UsableUser usableUser;   

    protected override void Awake()
    {
        base.Awake();
        usableUser = GetComponent<UsableUser>();   
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Aim + Rotate Weapon
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - (Vector2)transform.position).normalized;
    
        //Weapon Fire

        if (Input.GetButton("Fire1"))
        { 
            character.WeaponStartFire();
        }
        else
        {
            character.WeaponStopFire();    
        }

        //Usable input
        if (Input.GetButtonDown("Use"))
        {
            if (usableUser)
            {
                usableUser.Use();
            }
        }
        //Test input for Weapon station
        if (Input.GetKeyDown(KeyCode.T))
        {
            WeaponStationManager.instance.Open();     
        }

        //Movement input
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized;
  
        character.UpdateCharacter(aimDirection, movementInput);      

    }

}
