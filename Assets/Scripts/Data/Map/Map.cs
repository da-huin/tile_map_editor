using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public partial class Map
{
    private int index;
    public int Index { get { return index; } }
    public Piece[,] Pieces;
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    [NonSerialized]
    public List<GameObject> look_tile_realModel = new List<GameObject>();

}