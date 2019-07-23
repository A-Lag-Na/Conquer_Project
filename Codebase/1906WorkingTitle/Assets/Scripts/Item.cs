using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    enum Type {Weapon, Potion, EOF }

    Type type;

    private void Start()
    {

        switch (type)
        {
            case Type.Weapon:

                break;
            case Type.Potion:

                break;
            case Type.EOF:
                break;
            default:
                break;
        }
    }


}
