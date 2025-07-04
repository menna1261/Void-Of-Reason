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

    public float scrollCooldown = 0.2f; // 200ms between scrolls
    private float scrollTimer = 0f;


    private void Start()
    {
        foreach (var part in selectionParts)
        {
            part.SetActive(false);
        }

        WeaponName.text = "None";
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
        if (!isMenuActive) return;

        scrollTimer -= Time.deltaTime;
        float scroll = Input.mouseScrollDelta.y;

        if (scrollTimer <= 0f)
        {
            int prevIndex = keyCount;

            if (scroll < 0) // Scroll down
            {
                keyCount = (keyCount + 1) % selectionParts.Length;
                scrollTimer = scrollCooldown;
            }
            else if (scroll > 0) // Scroll up
            {
                keyCount = (keyCount - 1 + selectionParts.Length) % selectionParts.Length;
                scrollTimer = scrollCooldown;
            }

            // Update selection visuals
            for (int i = 0; i < selectionParts.Length; i++)
            {
                selectionParts[i].SetActive(i == keyCount);
            }

            WeaponName.text = getWeaponName(keyCount);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            WeaponManager.instance.EquipWeapon(WeaponName.text);
            Debug.Log("Equipped: " + WeaponName.text);
            Menu.SetActive(false);
            BG.SetActive(false);
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