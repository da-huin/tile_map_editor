using System;
using System.Collections.Generic;
using UnityEngine;


public partial class Map
{
    public partial class Piece
    {
        public bool IsStrExist
        {
            get
            {
                if (effect_str == "") return false;
                else return true;
            }
        }

        public bool SetTile_Overlap(int layer, Tile tile)
        {
            if (IsSettedTile(layer)) RemoveTile(layer);

            layerDic_tile.Add(layer, tile);
            return true;
        }

        public bool IsSettedTile(int layer)
        {
            foreach (var piece in layerDic_tile.Keys)
            {
                if (piece == layer) return true;
            }
            return false;
        }

        public void RemoveTile(int layer)
        {
            layerDic_tile.Remove(layer);
        }

        public Tile GetTile(int layer)
        {
            return layerDic_tile[layer];
        }


        public void Init_layerDic_realModel()
        {
            layerDic_realModel = new Dictionary<int, GameObject>();
        }


        public bool SetRealModel_Overlap(int layer, GameObject realModel)
        {
            if (IsSettedRealModel(layer)) RemoveRealModel(layer);
            layerDic_realModel.Add(layer, realModel);
            return true;
        }

        public bool IsSettedRealModel(int layer)
        {
            foreach (var piece in layerDic_realModel.Keys)
            {
                if (piece == layer) return true;
            }
            return false;
        }

        public void RemoveRealModel(int layer)
        {
            layerDic_realModel.Remove(layer);
        }

        public GameObject GetRealModel(int layer)
        {
            return layerDic_realModel[layer];
        }

    }
}
