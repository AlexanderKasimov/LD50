using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextHandler : MonoBehaviour
{

    private Text textUI;

    private bool isCurrent = false;


    private void Awake() {
        textUI = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void PlayText(string text)
    {
        gameObject.SetActive(true);
        // textUI.text = text + "/nE - continue";      
        textUI.text = text;
        textUI.text += "\nE - continue";
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isCurrent)
        // {
        //     return;
        // }
        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("Next Line pressed");
            gameObject.SetActive(false);        
            DialogManager.instance.PlayNextLine();            
        }
        
    }
}
