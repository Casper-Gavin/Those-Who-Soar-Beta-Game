using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectables
{
    [SerializeField] private Sprite imageForUI;
    public bool pickedUp = false;

    private void Start()
    {
        base.Start();
        pickedUp = false;
    }

    protected override void Pick()
    {
        pickedUp = true;
        UIManager.Instance.AddKey(this);
        // show key in player UI (move sprite? would have to override ontriggerenter2d)
        // play noise?
    }

    public Sprite GetKeyImageForUI()
    {
        return imageForUI;
    }
}