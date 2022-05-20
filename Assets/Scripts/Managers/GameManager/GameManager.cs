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
    public int currentDay = 0;

    [SerializeField]
    private int finalDay = 7;

    [SerializeField]
    private bool isSpawnTestWave = false;

    //Characters 

    [field: SerializeField, Header("Characters")] public Character PlayerCharacter { get; private set; }

    [field: SerializeField] public Character SarahCharacter { get; private set; }





    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        // currentDay = 0;
        if (isSpawnTestWave)
        {
            WaveManager.instance.StartWave();
        }

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
        PlayerCharacter.TeleportToPosition(playerBasePosition.position);
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
        PlayerCharacter.TeleportToPosition(playerBattlePosition.position);
        PlayerCharacter.gameObject.SetActive(true);
    }

    //End day Event
    public void StartEndDayEvent()
    {
        PlayerCharacter.Deactivate();
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
        PlayerCharacter.StatsModule.ChangeMentalState(value);
        SarahCharacter.StatsModule.ChangeMentalState(value);
    }
}
