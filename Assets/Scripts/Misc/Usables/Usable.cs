using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{

    [SerializeField]
    private GameObject textObject;
    [SerializeField]
    private GameObject arrowObject;

    public bool isUsed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UsableUser usableUser = other.gameObject.GetComponent<UsableUser>();
        if (usableUser)
        {
            usableUser.AddUsable(this);
        }
        if (textObject)
        {
            textObject.SetActive(true);
        }
        if (arrowObject)
        {
            arrowObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UsableUser usableUser = other.gameObject.GetComponent<UsableUser>();
        if (usableUser)
        {
            usableUser.RemoveUsable(this);
        }
        ResetIsUsed();
        if (textObject)
        {
            textObject.SetActive(false);
        }
        if (arrowObject)
        {
            arrowObject.SetActive(false);
        }
    }


    public virtual void Use()
    {
        /*   
           Don't usefull? cause input usually disabled -> and if disabled here, objects will have to call ResetIsUsed, cause OnTriggerExit 
           don't called in WeaponStation
        */
        if (isUsed)
        {
            return;
        }
        isUsed = true;
        Debug.Log("Used");
    }

    public void ResetIsUsed()
    {
        isUsed = false;
    }


}
