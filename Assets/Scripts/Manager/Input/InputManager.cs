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
}
