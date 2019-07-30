using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    [SerializeField] float healthReturn = 5;
    private Potion shallow;
    

    private void Update()
    {
        if(shallow != null)
        {
            this.SetName(shallow.GetName());
            this.healthReturn = shallow.healthReturn;
            SetSprite(shallow.GetSprite());
            SetValue(shallow.GetValue());
            shallow = null;
        }
    }

    public float Heal()
    {
        return healthReturn;
    }

    public void SetShallow(Potion _shallow)
    {
        shallow = _shallow;
    }
}
