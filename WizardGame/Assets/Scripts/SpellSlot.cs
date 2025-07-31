using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    public SpellType Type;
    public int Beat;

    public void ChangeValue(SpellType newType)
    {
        Type = newType;
        print("NEw type: "+ newType);
    }
}
