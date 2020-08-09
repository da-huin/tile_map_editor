using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputFieldFunction : Log
{
    public void OnStrEffectValueChange(String str)
    {
        conv_last_MapClicked_Piece.effect_str = str;

    }

    public void OnStrEffectEditEnd(String str)
    {
        if (Unity.Inst.effect_set.activeSelf == true)
        {
            Ex.Inst.ActiveConvert(Unity.Inst.effect_set);
            Ex.Inst.StrEffectTile_Tile_Renewal(curr_map, new Vector2(conv_last_MapClicked_pos.x, conv_last_MapClicked_pos.y));
        }

    }


}
