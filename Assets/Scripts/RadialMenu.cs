using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject BG;

    public TMP_Text WeaponName;

    public bool isMenuActive;
    private int keyCount = 0;

    public GameObject [] selectionParts;


    private void Start()
    {
        foreach (var part in selectionParts)
        {
            part.SetActive(false);
        }
        
        isMenuActive = false;
        Menu.SetActive(false);
        BG.SetActive(false);
        Debug.Log("Menu is inactive");
    }

    private void Update()
    {
        Debug.Log("update");
        if (Input.GetKeyDown(KeyCode.L))
        {

            Debug.Log("Input detected");
            isMenuActive= !isMenuActive;
            BG.SetActive(isMenuActive);
            Menu.SetActive(isMenuActive);


            foreach (var part in selectionParts)
            {
                part.SetActive(false);
            }


            if (isMenuActive)
            {
                Debug.Log("Menu is active");

            }

            else
            {
                keyCount = 0;

                Debug.Log("Menu is inactive - update");

                WeaponName.text = "None";
            }
        }

        HandleMenuSelection();
    }

    void HandleMenuSelection()
    {
        if (isMenuActive)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("we are selecting parts");
                if (keyCount >= selectionParts.Length)
                {
                    selectionParts[keyCount - 1].SetActive(false);
                    keyCount = 0;
                }

                selectionParts[keyCount].SetActive(true);

                string selectedWeapon = getWeaponName(keyCount);
                WeaponName.text = selectedWeapon;

                if (keyCount > 0)
                    selectionParts[keyCount - 1].SetActive(false);

                WeaponManager.instance.EquipWeapon(selectedWeapon);

                keyCount++;
            }
        }
    }



    string getWeaponName(int index)
    {
        switch(index)
        {
            case 0:
                return "AK 47";

            case 1:
                return "Bomb";

            case 2:
                return "Pistol";

            case 3:
                return "Bomb";

            case 4:
                return "Riffle";

            case 5:
                return "Knife";
        }

        return "None";
    }



}