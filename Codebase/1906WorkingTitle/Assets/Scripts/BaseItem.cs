using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    enum Type { Weapon, Potion, EOF};

    [SerializeField] Type itemType;
    

}
