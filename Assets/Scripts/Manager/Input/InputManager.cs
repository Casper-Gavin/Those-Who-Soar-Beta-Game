using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : Singleton<InputManager> {
    [SerializeField] public Keybindings keybindings;
    [SerializeField] private TextMeshProUGUI SprintText;
    [SerializeField] private TextMeshProUGUI DashText;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private TextMeshProUGUI SkillMenuText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI ReloadText;
    [SerializeField] private GameObject inputMenu;

    private void Start() {
        UpdateAllControlsUI();

        if (keybindings == null) {
            keybindings = new Keybindings();

            keybindings.LoadKeybindings();
        }

        if (inputMenu == null) {
            inputMenu = GameObject.Find("InputMenu");
        }

        if (SprintText == null) {
            SprintText = GameObject.Find("Sprint").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        if (DashText == null) {
            DashText = GameObject.Find("Dash").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        if (PauseText == null) {
            PauseText = GameObject.Find("Pause").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        if (SkillMenuText == null) {
            SkillMenuText = GameObject.Find("SkillMenu").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        if (InteractText == null) {
            InteractText = GameObject.Find("Interact").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        if (ReloadText == null) {
            ReloadText = GameObject.Find("Reload").GetComponentsInChildren<TextMeshProUGUI>()[1];
        }
    }

    private void Update() {
        if (inputMenu.activeSelf) {
            UpdateAllControlsUI();
        }
    }

    public KeyCode GetKeyForAction(KeybindingActions action) {
        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
            if (keybinding.keybindingActions == action) {
                return keybinding.keyCode;
            }
        }

        return KeyCode.None;
    }

    public void SetKeyForAction(KeybindingActions action, KeyCode keyCode) {
        keybindings.SetKeybinding(action, keyCode);
    }

    public bool GetKeyDown(KeybindingActions key) {
        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
            if (keybinding.keybindingActions == key) {
                return Input.GetKeyDown(keybinding.keyCode);
            }
        }

        return false;
    }

    public bool GetKeyUp(KeybindingActions key) {
        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
            if (keybinding.keybindingActions == key) {
                return Input.GetKeyUp(keybinding.keyCode);
            }
        }

        return false;
    }

    public bool GetKey(KeybindingActions key) {
        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
            if (keybinding.keybindingActions == key) {
                return Input.GetKey(keybinding.keyCode);
            }
        }

        return false;
    }

    public void UpdateAllControlsUI() {
        int idCounter = 0;
        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
            UpdateUI(idCounter);
            idCounter++;
        }
    }

    public void UpdateUI(int id) {
        switch (id) {
            case 0:
                SprintText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
            case 1:
                DashText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
            case 2:
                PauseText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
            case 3:
                SkillMenuText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
            case 4:
                InteractText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
            case 5:
                ReloadText.text = $"{keybindings.GetActionAtIndex(id)}\nButton:\n{keybindings.GetKeyForAction(keybindings.GetActionAtIndex(id))}";
                break;
        }

        //GetComponent<Image>().color = skillMenu.skillLevels[id] >= skillMenu.skillCaps[id] ? Color.yellow : skillMenu.skillPoints >= skillMenu.skillCosts[id] ? Color.green : Color.red;
    }

    public void SetKeybindingToDefault() {
        //Debug.Log("START setting keybindings to default");
        // Create a new array for keybindings.keybindings
        keybindings.keybindings = new Keybindings.KeybindingCheck[keybindings.keyDefaults.Length];

        // Copy each default keybinding into the new keybindings list
        for (int i = 0; i < keybindings.keyDefaults.Length; i++) {
            keybindings.keybindings[i] = new Keybindings.KeybindingCheck() {
                keybindingActions = keybindings.keyDefaults[i].keybindingActions,
                keyCode = keybindings.keyDefaults[i].keyCode
            };
            //Debug.Log($"Keybinding {keybindings.keybindings[i].keybindingActions} reset to {keybindings.keybindings[i].keyCode}");
        }

        // Save the newly reset keybindings
        keybindings.SaveKeybindings();

        // Update the UI to reflect these changes
        UpdateAllControlsUI();

        //Debug.Log("END setting keybindings to default");
    }

    public void StartRebindingDash() {
        //Debug.Log("Rebinding Dash");
        StartCoroutine(WaitForKeyPress(KeybindingActions.Dash));
    }

    public void StartRebindingSprint() {
        //Debug.Log("Rebinding Sprint");
        StartCoroutine(WaitForKeyPress(KeybindingActions.Sprint));
    }

    public void StartRebindingPause() {
        //Debug.Log("Rebinding Pause");
        StartCoroutine(WaitForKeyPress(KeybindingActions.Pause));
    }

    public void StartRebindingSkillMenu() {
        //Debug.Log("Rebinding Skill Menu");
        StartCoroutine(WaitForKeyPress(KeybindingActions.SkillMenu));
    }

    public void StartRebindingInteract() {
        //Debug.Log("Rebinding Interact");
        StartCoroutine(WaitForKeyPress(KeybindingActions.Interact));
    }

    public void StartRebindingReload() {
        //Debug.Log("Rebinding Reload");
        StartCoroutine(WaitForKeyPress(KeybindingActions.Reload));
    }

    private IEnumerator WaitForKeyPress(KeybindingActions action) {
        bool waitingForKey = true;

        // wait for 0.1 seconds to prevent the key press from being detected twice
        yield return new WaitForSeconds(0.2f);
    
        while (waitingForKey) {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(keyCode)) {
                    if (Input.GetKeyDown(keyCode)) {
                        bool isKeyAlreadyBound = false;
                        
                        // blacklisted keys (reserved for core movement or core functions)
                        if (keyCode == KeyCode.W || keyCode == KeyCode.A || keyCode == KeyCode.S || keyCode == KeyCode.D || keyCode == KeyCode.Mouse0 || keyCode == KeyCode.Alpha1 || keyCode == KeyCode.Alpha2)
                        {
                            isKeyAlreadyBound = true; // don't even really need this line since we break immediately
                            break;
                        }

                        foreach (Keybindings.KeybindingCheck keybinding in keybindings.keybindings) {
                            if (keybinding.keybindingActions != action && keybinding.keyCode == keyCode) {
                                isKeyAlreadyBound = true;
                                break; // Key is already bound to another action
                            }
                        }

                        if (!isKeyAlreadyBound) {
                            SetKeyForAction(action, keyCode);
                            keybindings.SaveKeybindings();
                            UpdateAllControlsUI();
                            waitingForKey = false;
                        } else {
                            // Handle key already bound scenario, e.g., show a message to the user
                        }

                        break; // Break the loop after handling key press
                    }

                    break; // Break the foreach loop
                }
            }

            yield return null; // Wait for the next frame
        }
    }
}