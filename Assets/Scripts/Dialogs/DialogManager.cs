using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //Singleton
    public static DialogManager instance;

    //Dialogs
    [SerializeField]
    private DialogContainer introDialog;

    [SerializeField]
    private DialogContainer sarahDeadDialog;

    [SerializeField]
    private DialogContainer sarahNegativeDialog;

    [SerializeField]
    private DialogContainer sarahPositiveDialog;

    [SerializeField]
    private DialogContainer playerResponseDialog;

    [SerializeField]
    private DialogContainer radioPositiveDialog;

    [SerializeField]
    private DialogContainer radioNegativeDialog;

    //Dialogs containers   
    private List<DialogContainer> introDialogContainer;

    private List<DialogContainer> sarahDeadDialogContainer;

    private List<DialogContainer> genericDialogContainer;

    //Current
    private DialogContainer currentDialog;

    private List<DialogContainer> currentDialogContainer;

    //Variables
    [SerializeField]
    private float negativeSarahMentalStateThreshold = 40f;

    [SerializeField]
    private float radioPositiveChance = 60f;



    //Indexes
    [SerializeField]
    private int currentDialogIndex = 0;

    [SerializeField]
    private int currentDialogEntryIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init Dialog Known dialog containers       
        introDialogContainer = new List<DialogContainer>();
        introDialogContainer.Add(introDialog);

        sarahDeadDialogContainer = new List<DialogContainer>();
        sarahDeadDialogContainer.Add(sarahDeadDialog);



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialog(bool isIntro, bool isSarahDead)
    {
        Debug.Log("Start Dialog");
        if (isIntro)
        {
            PlayDialogContainer(introDialogContainer);
            return;
        }
        if (isSarahDead)
        {
            PlayDialogContainer(sarahDeadDialogContainer);
            return;
        }
        //Play Generic dialog 
        GenerateGenericDialog();
        PlayDialogContainer(genericDialogContainer);
    }

    //Playing Dialogs

    private void PlayDialogContainer(List<DialogContainer> dialog)
    {

        currentDialogContainer = dialog;
        currentDialogIndex = 0;
        PlayNextDialog();
    }

    private void PlayNextDialog()
    {
        Debug.Log(currentDialogContainer.Count + " | " + currentDialogIndex);
        if (currentDialogContainer.Count <= currentDialogIndex)
        {
            Debug.Log("EndDialogContainer");
            EndDialogEvent();
            return;
        }     
        PlayDialog(currentDialogContainer[currentDialogIndex++]);     
    }

    private void PlayDialog(DialogContainer dialog)
    {
        currentDialog = dialog;
        currentDialogEntryIndex = 0;
        PlayNextLine();
    }

    public void PlayNextLine()
    {
        Debug.Log(currentDialog.dialog.Count + " | " + currentDialogEntryIndex);
        if (currentDialog.dialog.Count <= currentDialogEntryIndex)
        {
            Debug.Log("EndDialog");
            PlayNextDialog();
            return;
        }
        DialogTextHandler dialogTextHandler = currentDialog.dialog[currentDialogEntryIndex].speaker.GetComponent<DialogTextHandler>();
        if (dialogTextHandler)
        {
            int randomText = Random.Range(0, currentDialog.dialog[currentDialogEntryIndex].textSO.Count);
            dialogTextHandler.PlayText(currentDialog.dialog[currentDialogEntryIndex].textSO[randomText].text);
            if (currentDialog.dialog[currentDialogEntryIndex].mentalStateEffect != 0f)
            {
                GameManager.instance.ChangeMentalStates(currentDialog.dialog[currentDialogEntryIndex].mentalStateEffect);
            }
        }
        else
        {
            EndDialogEvent();
        }
        currentDialogEntryIndex++;
    }

    public void EndDialogEvent()
    {
        GameManager.instance.EndDay();
    }

    private void GenerateGenericDialog()
    {
        genericDialogContainer.Clear();
        if (SarahController.instance.statsModule.mentalState < negativeSarahMentalStateThreshold)
        {
            genericDialogContainer.Add(sarahNegativeDialog);
        }
        else
        {
            genericDialogContainer.Add(sarahPositiveDialog);
        }
        genericDialogContainer.Add(playerResponseDialog);
        float radioPositiveRoll = Random.Range(0f, 100f);
        if (radioPositiveRoll <= radioPositiveChance)
        {
            genericDialogContainer.Add(radioPositiveDialog);
        }
        else
        {
            genericDialogContainer.Add(radioNegativeDialog);
        }
    }
}
