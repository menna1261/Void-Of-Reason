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
        if (Input.GetKeyDown(KeyCode.L))
        {
            isMenuActive = !isMenuActive;
            BG.SetActive(isMenuActive);
            Menu.SetActive(isMenuActive);

            if (isMenuActive)
            {
                Time.timeScale = 0f; // pause game
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Menu is active");
            }
            else
            {
                Time.timeScale = 1f; // resume game
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                keyCount = 0;
                WeaponName.text = "None";
                Debug.Log("Menu is inactive");
            }

            foreach (var part in selectionParts)
            {
                part.SetActive(false);
            }
        }

        // Always allow menu logic to run
        HandleMenuSelection();
    }

    void HandleMenuSelection()
    {
        if (!isMenuActive) return;

        // Use unscaled time while paused
        float delta = Time.unscaledDeltaTime;
        scrollTimer -= delta;

        float scroll = Input.mouseScrollDelta.y;

        if (scrollTimer <= 0f)
        {
            int prevIndex = keyCount;

            if (scroll < 0)
            {
                keyCount = (keyCount + 1) % selectionParts.Length;
                scrollTimer = scrollCooldown;
            }
            else if (scroll > 0)
            {
                keyCount = (keyCount - 1 + selectionParts.Length) % selectionParts.Length;
                scrollTimer = scrollCooldown;
            }

            // Update UI
            for (int i = 0; i < selectionParts.Length; i++)
                selectionParts[i].SetActive(i == keyCount);

            WeaponName.text = getWeaponName(keyCount);
        }

        // Confirm selection (e.g., press M)
        if (Input.GetKeyDown(KeyCode.M))
        {
            WeaponManager.instance.EquipWeapon(WeaponName.text);
            Debug.Log("Equipped: " + WeaponName.text);

            isMenuActive = false;
            Menu.SetActive(false);
            BG.SetActive(false);
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Cancel menu (e.g., press Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = false;
            Menu.SetActive(false);
            BG.SetActive(false);
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    /*   public void AddWeapon(int index)
       {

           if (!isMenuActive) return;
           Weapons[index].SetActive(true);
           Debug.Log("Weapon added");
       }

   */

    string getWeaponName(int index)
    {
        switch(index)
        {
            case 0:
                return "smg";

            case 1:
                return "Saiga";

            case 2:
                return "AK47";

            case 3:
                return "None";

            case 4:
                return "None";

            case 5:
                return "None";
        }

        return "None";
    }



}