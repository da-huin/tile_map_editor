using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Ex : Material
{
    private bool Annoucing;

    public void ExStart()
    {
        Unity.Inst.text_help.text = help_str;
        if (Unity.Inst.help_set.activeSelf)
            ShowAndHide_HelpText();
        if (Unity.Inst.SaveLoad_Set.activeSelf)
            ActiveConvert(Unity.Inst.SaveLoad_Set);
        if (Unity.Inst.effect_set.activeSelf)
            ActiveConvert(Unity.Inst.effect_set);
    }

    void OnGUI()
    {
        Unity.Inst.image_selected_tile.sprite = ref_tile[Selected_tile_index].Model.GetComponent<SpriteRenderer>().sprite;
        Unity.Inst.text_layer.text = Selected_layer.ToString();
        Unity.Inst.text_tile_loc_count.text = Tile_loc_current_count + " / " + Tile_loc_all_count;

        Vector3 sel_tile_pos = ref_tile[Selected_tile_index].Model.transform.position;
        Ob_selected_tile_edge.transform.position = new Vector3(sel_tile_pos.x, sel_tile_pos.y, sel_tile_pos.z - 1);

        Unity.Inst.text_selcted_tile_index.text = Selected_tile_index.ToString();
        Unity.Inst.text_announce.text = announce_str;
        Unity.Inst.text_tile_xy.text = conv_last_MapClicked_pos.x + ", " + conv_last_MapClicked_pos.y;

    }


    public GameObject MapTileChange(Map map, Vector2 pos, int layer, Map.Tile tile_des)
    {
        var map_piece = map.Pieces[(int)pos.x, (int)pos.y];
       // var tile_ori = map_piece.GetTile(layer);


        return ConvertGameObject(map_piece.GetRealModel(layer), tile_des.Model);
    }

    public GameObject ConvertGameObject(GameObject ori, GameObject des_model)
    {
        var realModel = (GameObject)Instantiate(des_model, hide_pos, Quaternion.identity);
        realModel.transform.position = ori.transform.position;
        Destroy(ori);

        return realModel;
    }


    public void ShowAndHide_HelpText()
    {
        var help_set = Unity.Inst.help_set;
        if (help_set.activeSelf)
            help_set.SetActive(false);
        else
            help_set.SetActive(true);

    }

    public void SetTileCamera_OnSelectTileIndex()
    {
        var tile_camera_pos = Unity.Inst.tile_loc_camera.transform.position;
        Unity.Inst.tile_loc_camera.transform.position =
            new Vector3(tile_camera_pos.x, ref_tile[Selected_tile_index].Model.transform.position.y + tile_loc_camera_add_y, tile_camera_pos.z);

    }
    public void SetTileCamera_OnCountValue()
    {
        var tile_pos = ref_tile[(Tile_loc_current_count - 1) * addIndex].Model.transform.position;
        var camera_pos = Unity.Inst.tile_loc_camera.transform.position;
        Unity.Inst.tile_loc_camera.transform.position =
            new Vector3(tile_pos.x + (limit_x / 2) + tile_loc_camera_add_x, tile_pos.y + tile_loc_camera_add_y, camera_pos.z);
    }


    public void Map_Destroy(Map _map)
    {
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                for (int i = 1; i <= selected_layer_max; i++)
                    Destroy(_map.Pieces[x, y].GetRealModel(i));
            }
        }
        Destroy_Tile_Look(_map);
    }

    public void Map_Create(Map _map)
    {

        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                for (int i = 1; i <= selected_layer_max; i++)
                {
                    var ob_model = (GameObject)Instantiate(_map.Pieces[x, y].GetTile(i).Model
                       , new Vector3(x, -y, -i), Quaternion.identity);
                    _map.Pieces[x, y].SetRealModel_Overlap(i, ob_model);
                }
            }
        }
        Create_Tile_Look(_map);
    }

    public void StrEffectTile_Create(Map _map)
    {
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                if (_map.Pieces[x, y].IsStrExist == true)
                {
                    _map.Pieces[x, y].ob_effect_str_realModel = (GameObject)Instantiate(Ob_StrEffect_OnTile, new Vector3(x, -y, tile_obst_z), Quaternion.identity);
                }
            }
        }

    }

    public void StrEffectTile_Destroy(Map _map)
    {
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                Destroy(_map.Pieces[x, y].ob_effect_str_realModel);
                _map.Pieces[x, y].ob_effect_str_realModel = null;
            }
        }
    }

    public void StrEffectTile_Tile_Renewal(Map _map, Vector2 pos)
    {
        var cur_piece = _map.Pieces[(int)pos.x, (int)pos.y];


        if (cur_piece.IsStrExist)
        {
            // STR인데 실제 모델이 없을 경우
            if (cur_piece.ob_effect_str_realModel == null)
            {
                _map.Pieces[(int)pos.x, (int)pos.y].ob_effect_str_realModel =
                    (GameObject)Instantiate(Ob_StrEffect_OnTile,
                    new Vector3(pos.x, -pos.y, tile_obst_z),
                    Quaternion.identity);
            }
        }
        else
        {
            if (cur_piece.ob_effect_str_realModel != null)
            {
                Destroy(cur_piece.ob_effect_str_realModel);
                cur_piece.ob_effect_str_realModel = null;
            }
        }


    }

    public void ObstacleTile_Create(Map _map)
    {
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                if (_map.Pieces[x, y].IsObstacle == true)
                {
                    _map.Pieces[x, y].obst_realModel = (GameObject)Instantiate(Ob_obstacleX, new Vector3(x, -y, tile_obst_z), Quaternion.identity);
                    _map.Pieces[x, y].obst_realModel.name = "OBST";
                }
            }
        }
    }

    public void ObstacleTile_Destroy(Map _map)
    {
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                Destroy(_map.Pieces[x, y].obst_realModel);
                _map.Pieces[x, y].obst_realModel = null;
            }
        }
    }



    public void ObstacleTile_Tile_Renewal(Map _map, Vector2 pos)
    {
        var cur_piece = _map.Pieces[(int)pos.x, (int)pos.y];


        if (cur_piece.IsObstacle)
        {
            // 장애물인데 실제 모델이 없을 경우
            if (cur_piece.obst_realModel == null)
            {
                _map.Pieces[(int)pos.x, (int)pos.y].obst_realModel =
                    (GameObject)Instantiate(Ob_obstacleX,
                    new Vector3(pos.x, -pos.y, tile_obst_z),
                    Quaternion.identity);
            }
        }
        else
        {
            if (cur_piece.obst_realModel != null)
            {
                Destroy(cur_piece.obst_realModel);
                cur_piece.obst_realModel = null;
            }
        }
    }

    public void ActiveConvert(GameObject obj)
    {
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }



    private static Ex inst;
    public static Ex Inst { get { if (inst == null) inst = FindObjectOfType<Ex>(); return inst; } }


    public void Announce(string str)
    {
        if (Annoucing == false)
        {
            StartCoroutine(Announce_Coroutine(str));
        }
    }

    private IEnumerator Announce_Coroutine(string str)
    {
        Annoucing = true;
        announce_str = str;

        yield return new WaitForSeconds(2f);

        announce_str = announce_str_base;
        Annoucing = false;

        yield return null;
    }


    public void SaveLoadRenewal()
    {


        Unity.Inst.IF_save.text = "";
        List<Dropdown.OptionData> list_options = new List<Dropdown.OptionData>();
        Unity.Inst.DD_load.ClearOptions();

        DirectoryInfo dir_info = new DirectoryInfo("Maps");

        if (Directory.Exists("Maps"))
        {
            foreach (var piece in dir_info.GetFiles())
            {
                list_options.Add(new Dropdown.OptionData(piece.Name));
            }
            Unity.Inst.DD_load.AddOptions(list_options);
        }
        else
            dir_info.Create();
    }

    public void SetEffectTextFromStr()
    {
        Unity.Inst.IF_StrEffect.text = conv_last_MapClicked_Piece.effect_str;
    }


    public void ShowObject(GameObject obj)
    {
        if (!obj.activeSelf)
            obj.SetActive(true);

    }

    public void HideObject(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);

    }

    public void Create_Tile_Look(Map _map)
    {
        _map.look_tile_realModel = new List<GameObject>();
        for (int y = 0; y < _map.SizeY; y++)
        {
            for (int x = 0; x < _map.SizeX; x++)
            {
                var ob = (GameObject)Instantiate(Ob_tile_look, new Vector3(x, -y, tile_look_z), Quaternion.identity);
                ob.name = "Tile_Look";
                _map.look_tile_realModel.Add(ob);
            }
        }
    }

    public void Destroy_Tile_Look(Map _map)
    {
        foreach (var piece in _map.look_tile_realModel)
            Destroy(piece);
    }

}
