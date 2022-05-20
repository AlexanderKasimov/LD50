using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPurchaseButton : MonoBehaviour
{
    //Private enum
    private enum WeaponPurchaseStatus
    {
        NotPurchased,
        Purchased,
        Current
    }

    //Private inspector params
    [SerializeField]
    private Text weaponNameText;

    [SerializeField]
    private Text weaponStatusText;

    [SerializeField]
    private Image icon; 

    [SerializeField]
    private Color canPurchaseColor;

    [SerializeField]
    private Color cantPurchaseColor;

    //Public get only props
    public Character User { get; private set; }

    //Private
    private Color defaultStatusColor;

    private Weapon weaponPrefab;

    private int weaponPrice = 0;

    private WeaponPurchaseStatus weaponPurchaseStatus = WeaponPurchaseStatus.NotPurchased;


    private void Awake()
    {
        defaultStatusColor = weaponStatusText.color;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(Weapon weaponPrefab, Character user, bool isCurrent)
    {
        this.weaponPrefab = weaponPrefab;
        User = user;

        weaponPrice = Mathf.RoundToInt(this.weaponPrefab.WeaponPrice);

        if (weaponNameText)
        {
            weaponNameText.text = this.weaponPrefab.WeaponName;
        }

        if (isCurrent)
        {
            weaponPurchaseStatus = WeaponPurchaseStatus.Current;
        }

        UpdateStatus();

    }

    public void OnClicked()
    {
        switch (weaponPurchaseStatus)
        {
            case WeaponPurchaseStatus.NotPurchased:
                TryToPurchase();
                break;
            case WeaponPurchaseStatus.Purchased:
                SwitchWeapon();
                break;
            case WeaponPurchaseStatus.Current:
                break;
            default:
                break;
        }
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        switch (weaponPurchaseStatus)
        {
            case WeaponPurchaseStatus.NotPurchased:
                weaponStatusText.text = weaponPrice.ToString();
                UpdateStatusTextColor();
                break;
            case WeaponPurchaseStatus.Purchased:
                weaponStatusText.text = "Owned";
                //icon.gameObject.SetActive(false);
                break;
            case WeaponPurchaseStatus.Current:
                weaponStatusText.text = "Current";
                //icon.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void UpdateStatusTextColor()
    {
        if (weaponPurchaseStatus != WeaponPurchaseStatus.NotPurchased)
        {
            weaponStatusText.color = defaultStatusColor;
            return;
        }
        weaponStatusText.color = BiomassManager.instance.CurrentBiomass >= weaponPrice ? canPurchaseColor : cantPurchaseColor;
    }

    public void SetNotCurrent()
    {
        weaponPurchaseStatus = WeaponPurchaseStatus.Purchased;
        UpdateStatus();
    }

    private void TryToPurchase()
    {
        bool purchaseResult = BiomassManager.instance.TryToPurchase(weaponPrice);
        if (!purchaseResult)
        {
            return;
        }
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        weaponPurchaseStatus = WeaponPurchaseStatus.Current;
        User.SwitchWeapon(weaponPrefab);
        WeaponStationManager.instance.WeaponPurchased(this);
    }

}
