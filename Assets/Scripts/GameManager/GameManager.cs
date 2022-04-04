using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;

    //Cameras
    [SerializeField]
    private GameObject battleCamera;

    [SerializeField]
    private GameObject baseCamera;

    //Teleports player
    [SerializeField]
    private Transform playerBattlePosition;

    [SerializeField]
    private Transform playerBasePosition;

    //End day actors
    [SerializeField]
    private GameObject playerEndDayActor;

    //Days
    [SerializeField]
    private int currentDay = 0;

    [SerializeField]
    private int finalDay = 7;



    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        currentDay = 0;     
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Transitions

    public void TransitionToBase()
    {
        battleCamera.SetActive(false);
        baseCamera.SetActive(true);
        PlayerController.instance.TeleportToPosition(playerBasePosition.position);
    }

    //Not Ready Yet
    public void TransitionToBatte()
    {
        //Switch cameras
        battleCamera.SetActive(true);
        baseCamera.SetActive(false);

        //Deactivate bone fire player
        playerEndDayActor.SetActive(false);      

        //Activate player and teleport
        PlayerController.instance.TeleportToPosition(playerBattlePosition.position);
        PlayerController.instance.gameObject.SetActive(true);
    }

    //End day Event
    public void StartEndDayEvent()
    {
        PlayerController.instance.Deactivate();
        playerEndDayActor.SetActive(true);     
        DialogManager.instance.StartDialog(currentDay == 0, false);
    }

    //End day
    public void EndDay()
    {
        currentDay++;
        TransitionToBatte();
    }

    public void ChangeMentalStates(float value)
    {
        PlayerController.instance.statsModule.ChangeMentalState(value);
        SarahController.instance.statsModule.ChangeMentalState(value);
    }
}
