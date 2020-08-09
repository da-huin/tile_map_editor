
using System;
using System.Collections.Generic;
using UnityEngine;
public class Material : Log
{
    public static void Create_Material()
    {
        Create_Tile();
        curr_map = Create_Void_Map(map_size_x, map_size_y);
    }

    private static Map Create_Void_Map(int sizeX, int sizeY)
    {
        Map new_map = new Map(0, sizeX, sizeY);
        for (int y = 0; y < new_map.SizeY; y++)
        {
            for (int x = 0; x < new_map.SizeX; x++)
            {
                for (int i = 1; i <= selected_layer_max; i++)
                {
                    new_map.Pieces[x, y].SetTile_Overlap(i, ref_tile[0]);
                }
            }
        }
        return new_map;
    }



    private static void Create_Tile()
    {
        int add_camera_x = -1000;
        int add_camera_y = 0;

        var tile_adder = Unity.Inst.tile_adder;
        int add_count = 0;

        for (int count = 0; count < tile_adder.Length; count++)
        {
            int curr_x = 0;
            int curr_y = 0;
            for (int i = 0; i < tile_adder[count].tile_sprites.Length; i++)
            {
                var index = i + count * addIndex;
                Map.Tile new_tile = new Map.Tile(index);
#if OBST_TILE
                if (tile_adder[count].IsObstacleSet == true)
                {
                    new_tile.IsObstacle = true;
                }
#endif
                new_tile.Model = (GameObject)Instantiate(Unity.Inst.ob_base_dot, new Vector3(curr_x + add_camera_x - (add_count * (limit_x + 3)), curr_y + add_camera_y, 0), Quaternion.identity);
                new_tile.Model.GetComponent<SpriteRenderer>().sprite = tile_adder[count].tile_sprites[i];
                new_tile.Model.name = (i + count * addIndex).ToString();
                new_tile.Model.AddComponent<BoxCollider2D>();
                var new_tile_pos = new_tile.Model.transform.position;
                Instantiate(Ob_tile_look, new Vector3(new_tile_pos.x, new_tile_pos.y, new_tile_pos.z+1), Quaternion.identity);


                ref_tile.Add(index, new_tile);

                curr_x++;
                if (curr_x % limit_x == 0)
                {
                    curr_x = 0;
                    curr_y--;
                }
            }
            add_count++;
        }
    }

}
