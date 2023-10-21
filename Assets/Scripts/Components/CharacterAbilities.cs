using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour {
    protected float horizantalInput;
    protected float verticalInput;

    // Start is called before the first frame update
    void Start() {
        
    }

    protected virtual void Update() {
        HandleAbility();
    }

    /// <summary>
    /// Here, we put the logic of each ability
    /// 
    /// We use protected virtual so that we can override this
    /// function in our child classes
    /// </summary>
    protected virtual void HandleAbility() {
        InternalUpdate();
    }

    /// <summary>
    /// Here, we get the necessary input for each ability
    /// and call the function(s) or action(s) associated with it
    /// </summary>
    protected virtual void HandleInput() {
        
    }

    /// <summary>
    /// Here, we get the main input needed for controlling
    /// our character
    /// </summary>
    protected virtual void InternalUpdate() {
        horizantalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
}
