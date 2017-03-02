using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum HUDElementType
{
    None = 0,
    RootHUD,    
	PanelArrow,
	PanelMapCheck,
    PanelMapPause,
    PanelItemCheckers,
    PanelStatusProgress,
    PanelLevelUp,
    PanelPauseSave,
    PanelQuitGame,
    PanelGameOver
}

public class HUDManager : SingletonBasic<HUDManager>
{


    public Dictionary<HUDElementType, Dictionary<string, HUDElement>> elements = new Dictionary<HUDElementType, Dictionary<string, HUDElement>>();
    public Dictionary<string, Dictionary<string, HUDElement>> T_elements = new Dictionary<string, Dictionary<string, HUDElement>>();

    protected override void Init()
    {
        DontDestroyOnLoad(this.gameObject);
        //Debug.Log("Starting Battle Manager");
        base.Init();
    }

    public void Add(HUDElementType type, string key, HUDElement value)
    {
        //Debug.Log("###### adding element: " + key);
        if (elements.ContainsKey(type))
        {
            if (!elements[type].ContainsKey(key))
                elements[type].Add(key, value);
            else
            {
                //Debug.LogWarning("Key " + key + " already exists, creating new");
                ((Component)value).name += " " + 1;
                Add(type, ((Component)value).name, value);
            }
        }
        else
        {
            elements.Add(type, new Dictionary<string, HUDElement>());
            elements[type].Add(key, value);
        }
    }



    public void Add(string type, string key, HUDElement value)
    {
        //Debug.Log("###### adding element: " + key);
        if (T_elements.ContainsKey(type))
        {
            if (!T_elements[type].ContainsKey(key))
                T_elements[type].Add(key, value);
            else
            {
                //Debug.LogWarning("Key " + key + " already exists, creating new");
                ((Component)value).name += " " + 1;
                Add(type, ((Component)value).name, value);
            }
        }
        else
        {
            T_elements.Add(type, new Dictionary<string, HUDElement>());
            T_elements[type].Add(key, value);
        }
    }
    public HUDElement Get(HUDElementType type, string key)
    {
        if (elements.ContainsKey(type) && elements[type].ContainsKey(key))
            return elements[type][key];
        return null;
    }


    public HUDElement Get(string type, string key)
    {
        if (T_elements.ContainsKey(type) && T_elements[type].ContainsKey(key))
            return T_elements[type][key];
        return null;
    }

    public HUDElement GetFirst(HUDElementType type)
    {
        if (elements.ContainsKey(type) && elements[type].Count > 0)
            foreach (KeyValuePair<string, HUDElement> pair in elements[type])
                return pair.Value;
        return null;
    }


    public HUDElement GetFirst(string type)
    {
        if (T_elements.ContainsKey(type) && T_elements[type].Count > 0)
            foreach (KeyValuePair<string, HUDElement> pair in T_elements[type])
                return pair.Value;
        return null;
    }
}