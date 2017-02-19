using UnityEngine;
using System.Collections;

public class PanelContainerHud : HUDElement
{
//**    StatsTypePersonaje hero;
    
    public bool DontDestroyonLoad;
    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        if (DontDestroyonLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }

    

    
}
