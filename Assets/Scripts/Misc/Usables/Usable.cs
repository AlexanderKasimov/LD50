using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{

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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UsableUser usableUser = other.gameObject.GetComponent<UsableUser>();
        if (usableUser)
        {
            usableUser.RemoveUsable(this);
        }
        ResetIsUsed();
    }

    public virtual void Use()
    {
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
