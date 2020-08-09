using System;
using System.Collections.Generic;
using UnityEngine;

// 그 캐릭터가 있는 맵과 좌표를 저장한다.

public partial class Map
{
    public Map() { }
    public Map(int _index, int sizeX, int sizeY)
    {
        this.index = _index;
        this.SizeX = sizeX;
        this.SizeY = sizeY;
        Pieces = new Piece[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Pieces[x, y] = new Piece();
            }
        }
    }


    public bool IsCanMove(Vector2 des, List<Vector2> list_add_obst)
    {
        foreach (var add_obst in list_add_obst)
        {
            if (add_obst.x == des.x && add_obst.y == des.y)
            {
                return false;
            }
        }

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                if (Pieces[x, y].IsObstacle == true)
                {
                    if (x == des.x && y == des.y)
                        return false;
                }
            }
        }

        if ((des.x >= SizeX || des.x < 0) || (des.y >= SizeY || des.y < 0))
            return false;
        return true;
    }


    public List<Vector2> GetCanMoveList(List<Vector2> list_add_obst)
    {
        List<Vector2> list_ret = new List<Vector2>();
        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var currentPos = new Vector2(x, y);
                if (IsCanMove(currentPos, list_add_obst))
                {
                    list_ret.Add(currentPos);
                }
            }
        }

        return list_ret;
    }

   
}