using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDElement : MonoBehaviour 
{
    public HUDElementType type = HUDElementType.None;
    public string typeId;
    public virtual void Awake()
    {
        if (type != HUDElementType.None)
        {
            HUDManager.Instance.Add(type, gameObject.name, this);
        }
        else
        {
            if(!string.IsNullOrEmpty(typeId)){
                HUDManager.Instance.Add(typeId, gameObject.name, this);
            } else {

               Debug.LogError("Error al utilizar Hudelement.... no hay id configurado para GO:"+ name);
            }
        }
    }
}
