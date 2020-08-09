using System;
using System.Collections.Generic;
using UnityEngine;
public class MainGame : Log
{
    void Awake()
    {
        Material.Create_Material();
        Ex.Inst.Map_Create(curr_map);
        Ex.Inst.SetTileCamera_OnCountValue();
        var center_pos = curr_map.Pieces[map_size_x / 2, map_size_y / 2].GetRealModel(1).transform.position;

        Unity.Inst.map_camera.transform.position =
            new Vector3(center_pos.x, center_pos.y, Unity.Inst.map_camera.transform.position.z);
        ButtonDown.Inst.OnRenewalButtonDown();
        Ex.Inst.ExStart();
    }

}
