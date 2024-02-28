using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{  
    [SerializeField] protected ItemInfo itemInfo;
    [SerializeField] public GameObject itemGameObject;

    void Awake()
    {
        Initialise();
    }

    // The 'virtual' keyword allows this function to be overriden by scripts that derive from it, e.g. 'override void Initialise () {}'.
    // The 'protected' keyword only allows this class and derived classes to access it. For example, BombPassable could use this function, if needed.
    protected virtual void Initialise()
    {
        // No code here! This doesn't mean the function is useless. It's designed for derived classes to use.
    }

    public virtual void Use()
    {
        // No code here! This doesn't mean the function is useless. It's designed for derived classes to use.
    }
}
