using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignKeybindManager : MonoBehaviour {
    [SerializeField] private InputManager inputManager;

    public void ButtonGetsClicked() {
        // get the game object that was clicked
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        // should call a coroutine to wait for the next key press to assign the keybind to the action
        StartCoroutine(AssignKeybind(clickedButton.name));

        inputManager.keybindings.SaveKeybindings();
    }

    private IEnumerator AssignKeybind(string actionName) {
        // wait for the next key press
        while (!Input.anyKeyDown) {
            yield return null;
        }

        // get the key that was pressed
        KeyCode key = KeyCode.None;
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(kcode)) {
                key = kcode;
                break;
            }
        }

        // if the key is not the same as any of the other keys, assign the key to the action
        foreach (Keybindings.KeybindingCheck keybinding in inputManager.keybindings.keybindings) {
            if (keybinding.keyCode == key) {
                key = KeyCode.None;
                break;
            }
        }

        KeybindingActions actionNameEnum = (KeybindingActions)System.Enum.Parse(typeof(KeybindingActions), actionName);

        // assign the key to the action
        inputManager.SetKeyForAction(actionNameEnum, key);
    }
}
