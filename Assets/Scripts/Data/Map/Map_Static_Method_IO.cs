using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public partial class Map
{
    public class IO
    {
        public static bool Map_Save(Map map, string save_name)
        {
            if (save_name == "")
            {
                Ex.Inst.Announce("Name to save is too short");
                return false;
            }

            if (Directory.Exists("Maps")) new DirectoryInfo("Maps").Create();

            string save_addr = "Maps\\" + save_name + ".map";
            DirectoryInfo dir_info = new DirectoryInfo("Maps");
            foreach (var piece in dir_info.GetFiles())
            {
                if (piece.Name == (save_name + ".map"))
                {
                    Ex.Inst.Announce("The file name is duplicated.");
                    return false;
                }
            }

            FileStream fs = new FileStream(save_addr, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, map);
            fs.Close();

            return true;
        }


        public static Map Map_Load(string save_name, ref Dictionary<int, Tile> _ref_tile, int layer_max)
        {
            save_name += ".map";
            //if (!save_name.Contains(".map"))
            //{
            //    return null;
            //}

            Map ret_map = new Map();
            if (!Directory.Exists("Maps"))
            {
                Debug.Log("!Maps Exist");
                new DirectoryInfo("Maps").Create();
                return null;
            }
            string save_addr = "Maps\\" + save_name;
            DirectoryInfo dir_info = new DirectoryInfo("Maps");
            bool ready = false;
            foreach (var piece in dir_info.GetFiles())
            {
                if (piece.Name == (save_name))
                {
                    ready = true;
                    break;
                }
            }
            if (ready == false)
            {
                Debug.Log("Not Ready");
                return null;
            }

            FileStream fs = new FileStream(save_addr, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            ret_map = (Map)bf.Deserialize(fs);
            fs.Close();
            Alloc_NonSeri(ret_map, ref _ref_tile, layer_max);
            return ret_map;
        }

        // 역시리얼라이즈
        //  실제 타일에 대한 정보를 얻는다. (모델)
        private static void Alloc_NonSeri(Map _map, ref Dictionary<int, Tile> _ref_tile, int layer_max)
        {
            for (int y = 0; y < _map.SizeY; y++)
            {
                for (int x = 0; x < _map.SizeX; x++)
                {
                    _map.Pieces[x, y].Init_layerDic_realModel();
                    for (int i = 1; i <= layer_max; i++)
                    {
                        Tile tile = _map.Pieces[x, y].GetTile(i);
                        tile = _ref_tile[tile.index];
                        _map.Pieces[x, y].SetTile_Overlap(i, tile);
                    }
                }
            }
        }



    }

}