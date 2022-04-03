using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 4f;

    private Rigidbody2D rb;

    private Vector2 inputVector;

    [SerializeField]
    private Weapon weapon;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Aim + Rotate Weapon
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - (Vector2)transform.position).normalized;
        if (weapon)
        {
            weapon.UpdateRotation(lookDirection);
        }
        //Weapon Fire

        if (Input.GetButton("Fire1"))
        {
            if (weapon)
            {
                weapon.StartFire();
            }        
        }
        else
        {
            if (weapon)
            {
                weapon.StopFire();
            }
        }

        //Movement input
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized;
   
    }


    private void FixedUpdate() 
    {
        Vector2 movementVector = inputVector * movementSpeed * Time.fixedDeltaTime;

        //rb.AddForce(movementVector);

        rb.MovePosition(rb.position + movementVector);
    }
}
