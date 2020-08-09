using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
public class InputManager : Material
{

    private float tile_loc_arrow_Cooldown_timer = 0f;
    Vector3 tile_loc_WP_pos;
    Vector3 map_WP_pos;
    private bool IsDragging;
    private bool IsLeftCtrlDown;
   // private bool IsLeftShftDown;
    private bool isEditEffectStr;
    private bool IsEditEffectStr
    {
        set
        {
            if (value)
                Ex.Inst.ShowObject(Unity.Inst.ob_show_effect);
            else
                Ex.Inst.HideObject(Unity.Inst.ob_show_effect);
            isEditEffectStr = value;
        }

        get
        {
            return isEditEffectStr;
        }
    }
    // 코드 깔끔하게 버그없는 디자인
    // UI 깔끔하게 버그없는 디자인

    void Update()
    {
        tile_loc_WP_pos = Unity.Inst.tile_loc_camera.ScreenToWorldPoint(Input.mousePosition);
        map_WP_pos = Unity.Inst.map_camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D tile_loc_hit = Physics2D.Raycast(tile_loc_WP_pos, Vector2.zero);
        RaycastHit2D map_hit = Physics2D.Raycast(map_WP_pos, Vector2.zero);



        float mouse_wheel_value = Input.GetAxis("Mouse ScrollWheel");
        tile_loc_arrow_Cooldown_timer += Time.deltaTime;

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.L) && IsLeftCtrlDown && !IsEditEffectStr && !Obstacling)
            {
                Ex.Inst.ActiveConvert(Unity.Inst.SaveLoad_Set);
            }
            if (Input.GetKeyDown(KeyCode.K) && IsLeftCtrlDown && !Unity.Inst.SaveLoad_Set.activeSelf)
            {

                if (IsEditEffectStr == false)
                {
                    Ex.Inst.Announce("EFFECT STRING MODE ON");
                    Ex.Inst.StrEffectTile_Create(curr_map);
                    IsEditEffectStr = true;

                }
                else
                {
                    Ex.Inst.StrEffectTile_Destroy(curr_map);
                    IsEditEffectStr = false;
                    if (Unity.Inst.effect_set.activeSelf == true)
                        Ex.Inst.ActiveConvert(Unity.Inst.effect_set);
                }
            }

            if (!Unity.Inst.SaveLoad_Set.activeSelf && !Unity.Inst.effect_set.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.F1)) Ex.Inst.ShowAndHide_HelpText();
                else if (Input.GetKeyDown(KeyCode.Alpha1)) Selected_layer = 1;
                else if (Input.GetKeyDown(KeyCode.Alpha2)) Selected_layer = 2;
                else if (Input.GetKeyDown(KeyCode.Alpha3)) Selected_layer = 3;
                else if (Input.GetKeyDown(KeyCode.Alpha4)) Selected_layer = 4;
                else if (Input.GetKeyDown(KeyCode.Alpha5)) Selected_layer = 5;
                else if (Input.GetKeyDown(KeyCode.Alpha6)) Selected_layer = 6;
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                {
                    var ori = Tile_loc_current_count;
                    if (Input.GetKeyDown(KeyCode.Q)) Tile_loc_current_count--;
                    else if (Input.GetKeyDown(KeyCode.E)) Tile_loc_current_count++;

                    // 바뀌었을 경우
                    if (ori != Tile_loc_current_count)
                    {
                        Ex.Inst.SetTileCamera_OnCountValue();
                        Selected_tile_index = (Tile_loc_current_count - 1) * addIndex;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    if (Eraseing == false)
                    {
                        Eraser_ori_Selected_tile_index = Selected_tile_index;
                        Selected_tile_index = 0;
                        Eraseing = true;
                    }
                    else
                    {
                        Selected_tile_index = Eraser_ori_Selected_tile_index;
                        Eraseing = false;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.O))
                {
                    if (Obstacling == false)
                    {
                        Ex.Inst.ObstacleTile_Create(curr_map);
                        Obstacling = true;
                    }
                    else
                    {
                        Ex.Inst.ObstacleTile_Destroy(curr_map);
                        Obstacling = false;
                    }
                }


            }

        }



        if (Input.anyKey)
        {
            if (!Unity.Inst.SaveLoad_Set.activeSelf && !Unity.Inst.effect_set.activeSelf)
            {
                //// 타일 로케이션 이동
                //if(Input.GetKey(KeyCode.R))
                //    Unity.Inst.tile_loc_camera.transform.Translate(0,tile_loc_camera_speed*Time.deltaTime, 0);
                //else if (Input.GetKey(KeyCode.F))
                //    Unity.Inst.tile_loc_camera.transform.Translate(0,-tile_loc_camera_speed*Time.deltaTime, 0);
                // 맵 이동
                if (Input.GetKey(KeyCode.UpArrow))
                    Unity.Inst.map_camera.transform.Translate(0, map_camera_speed * Time.deltaTime, 0);
                else if (Input.GetKey(KeyCode.DownArrow))
                    Unity.Inst.map_camera.transform.Translate(0, -map_camera_speed * Time.deltaTime, 0);
                else if (Input.GetKey(KeyCode.LeftArrow))
                    Unity.Inst.map_camera.transform.Translate(-map_camera_speed * Time.deltaTime, 0, 0);
                else if (Input.GetKey(KeyCode.RightArrow))
                    Unity.Inst.map_camera.transform.Translate(map_camera_speed * Time.deltaTime, 0, 0);
                else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    if (tile_loc_arrow_Cooldown_timer > tile_loc_arrow_Cooldown)
                    {
                        //if (Eraseing)
                        //{
                        //    Selected_tile_index = Eraser_ori_Selected_tile_index;
                        //    Eraseing = false;
                        //}
                        tile_loc_arrow_Cooldown_timer = 0;
                        if (Input.GetKey(KeyCode.W)) Selected_tile_index -= limit_x;
                        else if (Input.GetKey(KeyCode.A)) Selected_tile_index -= 1;
                        else if (Input.GetKey(KeyCode.S)) Selected_tile_index += limit_x;
                        else if (Input.GetKey(KeyCode.D)) Selected_tile_index += 1;

                        Ex.Inst.SetTileCamera_OnSelectTileIndex();
                    }
                }

            }

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsLeftCtrlDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsLeftCtrlDown = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //IsLeftShftDown = true;
            tile_loc_arrow_Cooldown = tile_loc_arrow_Colldown_SpeedUp;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //IsLeftShftDown = false;
            tile_loc_arrow_Cooldown = tile_loc_arrow_Cooldown_Ori;
        }


        if (Input.GetMouseButton(0))
        {
            if (IsDragging)
            {

                if (!Unity.Inst.SaveLoad_Set.activeSelf && !Unity.Inst.effect_set.activeSelf)
                {
                    if (map_hit.collider != null)
                    {
                        Vector2 col_pos = map_hit.collider.transform.position;
                        conv_last_MapClicked_pos = new Vector2(col_pos.x, -col_pos.y);
                        col_pos.y = -col_pos.y;
                        var curr_piece = curr_map.Pieces[(int)col_pos.x, (int)col_pos.y];
                        if (IsEditEffectStr)
                        {
                            if (Unity.Inst.effect_set.activeSelf == false)
                            {
                                Ex.Inst.SetEffectTextFromStr();
                                Ex.Inst.ActiveConvert(Unity.Inst.effect_set);
                                Unity.Inst.IF_StrEffect.Select();
                            }
                        }
                        else
                        {
                            var new_real_model = Ex.Inst.MapTileChange(curr_map, col_pos, Selected_layer, ref_tile[Selected_tile_index]);
                            curr_piece.SetRealModel_Overlap(Selected_layer, new_real_model);
                            curr_piece.SetTile_Overlap(Selected_layer, ref_tile[Selected_tile_index]);
                            
                        }

                    }
                }
            }

        }
        if(Input.GetMouseButtonDown(1))
        {
            if(map_hit.collider != null)
            {
                Vector2 col_pos = map_hit.collider.transform.position;
                var cur_piece = curr_map.Pieces[(int)col_pos.x, (int)-col_pos.y];

                if (cur_piece.IsObstacle == false)
                    cur_piece.IsObstacle = true;
                else
                    cur_piece.IsObstacle = false;
                if (Obstacling)
                {
                    Ex.Inst.ObstacleTile_Tile_Renewal(curr_map, new Vector2(col_pos.x, -col_pos.y));
                }
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
                if (tile_loc_hit.collider != null)
                {
                    //if (Eraseing) Eraseing = false;
                    var index_tile = Convert.ToInt32(tile_loc_hit.collider.name);
                    Selected_tile_index = index_tile;
                }
        }
        if (Input.GetMouseButtonDown(0))
        {
            IsDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }


        if (mouse_wheel_value > 0)
        {

            Unity.Inst.map_camera.orthographicSize -= map_camera_wheel_speed;
            if (Unity.Inst.map_camera.orthographicSize < 1) Unity.Inst.map_camera.orthographicSize = 1;
        }
        else if (mouse_wheel_value < 0)
        {
            Unity.Inst.map_camera.orthographicSize += map_camera_wheel_speed;
        }

    }
}
