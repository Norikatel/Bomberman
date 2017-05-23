﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class IntelligentMove : AutoMoveDynamicObject
    {
        int columnCount;
        int rowCount;
        List<Point> path = new List<Point>();

        private bool IsClosedTo(Point point)
        {
            return (Math.Abs(transform.position.GetFloatX(columnCount) - point.X) < 0.01) &&
                (Math.Abs(transform.position.GetFloatY(rowCount) - point.Y) < 0.01);
        }

        override protected void SetNewDirection()
        {
            if (path != null)
            {
                if (IsClosedTo(path[1]))
                {
                    path.RemoveAt(0);
                    transform.position = transform.position.RoundPosition();
                    SetNextDirection();
                }
            }
            else
                base.SetNewDirection();
        }

        private void SetNextDirection()
        {
            if (path!=null)
            {
                if (path[0].X > path[1].X)
                {
                    SetLeftDirection();
                    return;
                }
                if (path[0].X < path[1].X)
                {
                    SetRightDirection();
                    return;
                }
                if (path[0].Y > path[1].Y)
                {
                    SetDownDirection();
                    return;
                }
                if (path[0].Y < path[1].Y)
                {
                    SetUpDirection();
                    return;
                }
            }
        }
        
        private IEnumerator UpdatePath()
        {
            path = AStar.FindPath(GetField(GetWallsPointList()), GetEnemyCoordinate(), GetPlayerCoordinate());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(UpdatePath());
            SetNextDirection();
        }

        private void Start()
        {
            columnCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().columnCount;
            rowCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().rowCount;
            StartCoroutine(UpdatePath());
        }
        
        private int[,] GetField(List<Point> WallsPointList)
        {
            int[,] field = new int[columnCount, rowCount];
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    field[i, j] = WallsPointList.Contains(new Point(i, j)) ? 1 : 0;
            return field;
        }

        private Point GetPlayerCoordinate()
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position.RoundPosition().GetPoint(columnCount, rowCount);
        }

        private Point GetEnemyCoordinate()
        {
            return transform.position.RoundPosition().GetPoint(columnCount, rowCount);
        }

        private List<Point> GetWallsPointList()
        {
            List<Point> wallsPointList = new List<Point>();
            GameObject[] unbreakableWalls = GameObject.FindGameObjectsWithTag("UnbreakableWall");
            GameObject[] breakableWalls = GameObject.FindGameObjectsWithTag("BreakableWall");
            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            wallsPointList.AddRange(GetPoints(unbreakableWalls));
            wallsPointList.AddRange(GetPoints(breakableWalls));
            wallsPointList.AddRange(GetPoints(bombs));
            return wallsPointList;
        }

        private List<Point> GetPoints(GameObject[] objects) {
            List<Point> points = new List<Point>();
            foreach (var obj in objects)
                points.Add(obj.transform.position.GetPoint(columnCount, rowCount));
            return points;
        }
    }
}
