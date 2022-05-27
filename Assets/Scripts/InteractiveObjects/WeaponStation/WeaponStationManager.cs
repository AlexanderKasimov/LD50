using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponStationManager : MonoBehaviour
{
    //Singleton
    public static WeaponStationManager instance;

    [SerializeField]
    private WeaponPurchaseButton buttonPrefab;
    //Private inspector params
    //Player
    [Header("Player")]
    [SerializeField]
    private Transform playerScrollViewContentTransform;

    [SerializeField]
    private List<Weapon> playerWeapons;

    //Sarah
    [Header("Sarah")]
    [SerializeField]
    private Transform sarahScrollViewContentTransform;
    //For debug uses players list
    //[SerializeField]
    //private List<Weapon> sarahWeapons;

    //Private 
    //Player
    private List<WeaponPurchaseButton> playerWeaponButtons;

    private WeaponPurchaseButton currentPlayerWeaponButton;

    //Sarah
    private List<WeaponPurchaseButton> sarahWeaponButtons;

    private WeaponPurchaseButton currentSarahWeaponButton;

    //To init singleton while game object disabled
    public WeaponStationManager()
    {
        instance = this;
        playerWeaponButtons = new List<WeaponPurchaseButton>();
        sarahWeaponButtons = new List<WeaponPurchaseButton>();
    }

    private void Awake()
    {       

    }

    // Use this for initialization
    void Start()
    {
        GenerateButtonsForCharacter(GameManager.instance.PlayerCharacter, playerWeapons, playerScrollViewContentTransform, playerWeaponButtons, ref currentPlayerWeaponButton); 
        //For debug - one list for both
        GenerateButtonsForCharacter(GameManager.instance.SarahCharacter, playerWeapons, sarahScrollViewContentTransform, sarahWeaponButtons, ref currentSarahWeaponButton);      
        //GenerateButtonsForCharacter(GameManager.instance.SarahCharacter, sarahWeapons, sarahScrollViewContentTransform, sarahWeaponButtons, ref currentSarahWeaponButton);      
    }

    public void OnClose()
    {
        GameManager.instance.PlayerCharacter.ToggleInput(true);
        gameObject.SetActive(false);
    }

    public void Open()
    {
        GameManager.instance.PlayerCharacter.ToggleInput(false);
        gameObject.SetActive(true);
        UpdateButtonPrices();
    }

    private void GenerateButtonsForCharacter(Character character, List<Weapon> weapons, Transform contentTransform, List<WeaponPurchaseButton> buttons, ref WeaponPurchaseButton currentWeaponButton)
    {
        foreach (var weapon in weapons)
        {          
            bool isCurrent = character.CurWeapon.WeaponName == weapon.WeaponName;
            WeaponPurchaseButton weaponPurchaseButton = Instantiate(buttonPrefab, contentTransform);
            weaponPurchaseButton.Init(weapon, character, isCurrent);
            buttons.Add(weaponPurchaseButton);
            if (isCurrent)
            {
                currentWeaponButton = weaponPurchaseButton;
            }
        }

    }

    private void UpdateButtonPricesInList(List<WeaponPurchaseButton> buttons)
    {
        foreach (var weaponPurchaseButton in buttons)
        {
            weaponPurchaseButton.UpdateStatusTextColor();
        }
    }

    private void UpdateButtonPrices()
    {
        UpdateButtonPricesInList(playerWeaponButtons);
        UpdateButtonPricesInList(sarahWeaponButtons);
    }

    public void WeaponPurchased(WeaponPurchaseButton weaponPurchaseButton)
    {
        UpdateButtonPrices();

        if (weaponPurchaseButton.User == GameManager.instance.PlayerCharacter)
        {
            currentPlayerWeaponButton.SetNotCurrent();
            currentPlayerWeaponButton = weaponPurchaseButton;   
            return;
        }
        currentSarahWeaponButton.SetNotCurrent();
        currentSarahWeaponButton = weaponPurchaseButton; 
    }     


}