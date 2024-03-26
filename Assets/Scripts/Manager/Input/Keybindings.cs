using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject {
    [System.Serializable]
    public class KeybindingCheck {
        public KeybindingActions keybindingActions;
        public KeyCode keyCode;
    }

    // default keybindings
    public KeybindingCheck[] keyDefaults = {
        new KeybindingCheck { keybindingActions = KeybindingActions.Dash, keyCode = KeyCode.Space },
        new KeybindingCheck { keybindingActions = KeybindingActions.Sprint, keyCode = KeyCode.LeftShift },
        new KeybindingCheck { keybindingActions = KeybindingActions.Pause, keyCode = KeyCode.Escape },
        new KeybindingCheck { keybindingActions = KeybindingActions.SkillMenu, keyCode = KeyCode.T },
        new KeybindingCheck { keybindingActions = KeybindingActions.Interact, keyCode = KeyCode.C },
        new KeybindingCheck { keybindingActions = KeybindingActions.Reload, keyCode = KeyCode.R }
    };

    public KeybindingCheck[] keybindings;

    // class to save the keybindings to PlayerPrefs
    public void SaveKeybindings() {
        foreach (KeybindingCheck keybinding in keybindings) {
            PlayerPrefs.SetInt(keybinding.keybindingActions.ToString(), (int)keybinding.keyCode);
        }
    }

    // on game start, load the keybindings from PlayerPrefs
    public void LoadKeybindings() {
        foreach (KeybindingCheck keybinding in keybindings) {
            keybinding.keyCode = (KeyCode)PlayerPrefs.GetInt(keybinding.keybindingActions.ToString());
        }
    }

    // set the keybinding for a specific action
    public void SetKeybinding(KeybindingActions action, KeyCode keyCode) {
        foreach (KeybindingCheck keybinding in keybindings) {
            if (keybinding.keybindingActions == action) {
                keybinding.keyCode = keyCode;
            }
        }
    }

    public KeyCode GetKeyForAction(KeybindingActions action) {
        foreach (KeybindingCheck keybinding in keybindings) {
            if (keybinding.keybindingActions == action) {
                return keybinding.keyCode;
            }
        }

        return KeyCode.None;
    }

    public KeybindingActions GetActionAtIndex(int index) {
        return keybindings[index].keybindingActions;
    }

    // in awake, load the keybindings
    private void Awake() {
        LoadKeybindings();

        if (keybindings[0].keyCode == KeyCode.None) {
            keybindings = keyDefaults;
        }
    }

    // when the game is closed, save the keybindings
    private void OnApplicationQuit() {
        SaveKeybindings();
    }
}
