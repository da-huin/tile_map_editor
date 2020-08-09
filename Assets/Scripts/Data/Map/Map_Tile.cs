using System;
using UnityEngine;

public partial class Map
{
    [Serializable]
    public class Tile
    {
        public int index;

        [NonSerialized]
        public GameObject Model;


#if OBST_TILE
        public bool IsObstacle;
#endif

        public Tile(int _index)
        {
            this.index = _index;
        }

    }
}