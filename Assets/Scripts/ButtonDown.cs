using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ButtonDown :  Log
{
    public void OnRenewalButtonDown()
    {
        Ex.Inst.SaveLoadRenewal();
    }

    public void OnSaveButtonDown()
    {

        Map.IO.Map_Save(curr_map, Unity.Inst.IF_save.text);
        Ex.Inst.SaveLoadRenewal();
    }

    public void OnLoadButtonDown()
    {
        Ex.Inst.Map_Destroy(curr_map);

        string str = Unity.Inst.DD_load.captionText.text;
        if(str.Contains("."))
        {
            int i;
            for(i=0; i< str.Length; i++)
            {
                if (str[i] == '.')
                    break;
            }
            str = str.Substring(0, i);
        }
        curr_map = Map.IO.Map_Load(str, ref ref_tile, selected_layer_max);
        if(curr_map != null)
        Ex.Inst.Map_Create(curr_map);
        Ex.Inst.SaveLoadRenewal();
    }

    private static ButtonDown inst;
    public static ButtonDown Inst
    {
        get
        {
            if (inst == null) inst = FindObjectOfType<ButtonDown>(); return inst;
        }
    }
}
