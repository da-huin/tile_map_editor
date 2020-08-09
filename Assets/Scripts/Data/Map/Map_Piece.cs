using System;
using System.Collections.Generic;
using UnityEngine;


public partial class Map
{
    [Serializable]
    public partial class Piece
    {
        public string effect_str = "";
        public bool IsObstacle;
        private Dictionary<int, Tile> layerDic_tile = new Dictionary<int, Tile>();



        [NonSerialized]
        private Dictionary<int, GameObject> layerDic_realModel = new Dictionary<int, GameObject>();
        [NonSerialized]
        public GameObject ob_effect_str_realModel;
        [NonSerialized]
        public GameObject obst_realModel;

#if OBST_TILE
            PREV
                get
            {
                foreach (var piece in layerDic_tile)
                {
                    if (piece.Value.IsObstacle == true)
                        return true;
                }
                return false;
            }
#endif

    }
}
