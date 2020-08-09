using System;
using System.Collections.Generic;
using UnityEngine;
public class Log : MonoBehaviour
{
    
    protected static int Eraser_ori_Selected_tile_index;
    protected static bool Eraseing;
    private static bool obstacling;
    protected static bool Obstacling
    {
        set
        {
            if (value)
                Ex.Inst.ShowObject(Unity.Inst.ob_show_obstacling);
            else
                Ex.Inst.HideObject(Unity.Inst.ob_show_obstacling);
            obstacling = value;
        }

        get
        {

            return obstacling;
        }
    }
    protected const int tile_look_z = 7;
    protected const int tile_obst_z = -8;
    protected static int map_size_x
    {
        get
        {
            if (Unity.Inst.u_map_size_y == 0) return 50;
            return Unity.Inst.u_map_size_x;
        }
    }
    protected static int map_size_y
    {
        get
        {
            if (Unity.Inst.u_map_size_y == 0) return 50;
            return Unity.Inst.u_map_size_y;
        }
    }
    protected const float map_camera_wheel_speed = 1f;
    protected const float tile_loc_arrow_Cooldown_Ori = 0.15f;
    protected const float tile_loc_arrow_Colldown_SpeedUp = 0.01f;
    protected static float tile_loc_arrow_Cooldown = tile_loc_arrow_Cooldown_Ori;
    protected static float tile_loc_camera_add_x = -0.5f;
    protected static float tile_loc_camera_add_y = -3.5f;
    protected static Vector2 conv_last_MapClicked_pos;
    protected static Map.Piece conv_last_MapClicked_Piece { get { return curr_map.Pieces[(int)conv_last_MapClicked_pos.x, (int)conv_last_MapClicked_pos.y]; } }

    protected static Map curr_map;
    protected static Dictionary<int, Map.Tile> ref_tile = new Dictionary<int, Map.Tile>();

    protected static Vector3 hide_pos = new Vector3(-3000, -3000, 0);

    private static GameObject ob_selected_tile_edge;
    protected static GameObject Ob_selected_tile_edge
    {
        get
        {
            if (ob_selected_tile_edge == null)
            {
                ob_selected_tile_edge = (GameObject)Instantiate(Unity.Inst.ob_base_dot, hide_pos, Quaternion.identity);
                ob_selected_tile_edge.GetComponent<SpriteRenderer>().sprite = Unity.Inst.sprite_selected_tile_edge;
            }
            return ob_selected_tile_edge;
        }
    }

    private static GameObject ob_tile_look;
    public static GameObject Ob_tile_look
    {
        get
        {
            if (ob_tile_look == null)
            {
                ob_tile_look = (GameObject)Instantiate(Unity.Inst.ob_base_dot, hide_pos, Quaternion.identity);
                ob_tile_look.GetComponent<SpriteRenderer>().sprite = Unity.Inst.sprite_tile_look;
            }
            return ob_tile_look;
        }
    }

    private static GameObject ob_obstacleX;
    protected static GameObject Ob_obstacleX
    {
        get
        {
            if (ob_obstacleX == null)
            {
                ob_obstacleX = (GameObject)Instantiate(Unity.Inst.ob_base_dot, hide_pos, Quaternion.identity);
                ob_obstacleX.GetComponent<SpriteRenderer>().sprite = Unity.Inst.sprite_obstacleX;
            }
            return ob_obstacleX;
        }
    }

    private static GameObject ob_StrEffect_OnTile;
    protected static GameObject Ob_StrEffect_OnTile
    {
        get
        {
            if (ob_StrEffect_OnTile == null)
            {
                ob_StrEffect_OnTile = (GameObject)Instantiate(Unity.Inst.ob_base_dot, hide_pos, Quaternion.identity);
                ob_StrEffect_OnTile.GetComponent<SpriteRenderer>().sprite = Unity.Inst.sprite_string_exist;
            }
            return ob_StrEffect_OnTile;
        }
    }

    protected const int addIndex = 1000;
    protected const int limit_x = 10;

    protected const int map_camera_speed = 30;
    protected const int tile_loc_camera_speed = 30;
    protected const string help_str =
        "\t* HELP On/Off: F1\n" +
        "\t* 레이어 선택: 1 ~ 6\n" +
        "\t* 지우개: R\n" +
        "\t* 맵 이동: Arrow Key\n" +
        "\t* 그리기: Left Mouse\n" +
        "\t* 타일 선택: Middle Mouse\n" +
        "\t* 타일 세트 변경: Q, E\n" +
        "\t* 타일 선택 이동: W, A, S, D\n" +
        "\t* 빠른 타일 선택 이동: Shift + W, A, S, D\n" +
        "\t* 장애물 타일 On/Off: O\n" +
        "\t* 저장, 불러오기 On/Off: Ctrl + L\n";

    protected static string announce_str = "1.0 ver";
    protected const string announce_str_base = "1.0 ver";


    private static int selected_tile_index = 0;
    public static int Selected_tile_index
    {
        get
        {
            return selected_tile_index;
        }
        set
        {
            if (Eraseing)
            {
                Eraseing = false;
                Selected_tile_index = Eraser_ori_Selected_tile_index;
            }


            if (ref_tile.ContainsKey(value))
                selected_tile_index = value;
        }
    }


    // selected_layer의 초기값은 가장 낮은 1로 설정한다.

    private static int selected_layer = 1;
    protected const int selected_layer_max = 6;
    public static int Selected_layer
    {
        get
        {
            return selected_layer;
        }
        set
        {
            if (value < 1)
                selected_layer = 1;
            else if (value >= selected_layer_max)
                selected_layer = selected_layer_max;
            else
                selected_layer = value;
        }
    }
    protected static int Tile_loc_all_count { get { return Unity.Inst.tile_adder.Length; } }
    private static int tile_loc_current_count = 1;
    protected static int Tile_loc_current_count
    {
        get { return tile_loc_current_count; }
        set
        {
            if (value < 1)
                tile_loc_current_count = 1;
            else if (value >= Tile_loc_all_count)
                tile_loc_current_count = Tile_loc_all_count;
            else
                tile_loc_current_count = value;
        }
    }



}
