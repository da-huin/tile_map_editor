using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unity : Log
{
    public int u_map_size_x;
    public int u_map_size_y;
    public InputField IF_save;
    public Dropdown DD_load;
    public Button button_save;
    public Button button_load;
    public Camera map_camera;
    public Camera tile_loc_camera;
    public Sprite sprite_obstacleX;

    public Image image_selected_tile;
    public Sprite sprite_selected_tile_edge;
    public Text text_selcted_tile_index;
    public Text text_help;
    public GameObject help_set;
    public Text text_layer;
    public SpriteAdder[] tile_adder;
    public Text text_tile_loc_count;
    public GameObject ob_base_dot;
    public Sprite sprite_tile_look;
    public GameObject SaveLoad_Set;
    public Text text_announce;
    public Text text_tile_xy;
    public InputField IF_StrEffect;
    public Sprite sprite_string_exist;
    public GameObject effect_set;
    public GameObject ob_show_obstacling;
    public GameObject ob_show_effect;
    private static Unity inst;
    public static Unity Inst {
        get {
            if (inst == null) inst = FindObjectOfType<Unity>(); return inst; } }

}

[System.Serializable]
public class SpriteAdder
{
    public Sprite[] tile_sprites;
    public bool IsObstacleSet;
}