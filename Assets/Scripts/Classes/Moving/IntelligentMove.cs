using System;
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
        bool[,] field;
        Point lastSeenPlayerPosition;


        public void AddBarrier(Vector3 barrier)
        {
            Point p = barrier.RoundPosition().GetPoint(columnCount, rowCount);
            field[p.X, p.Y] = true;
        }

        public void DeleteBarrier(Vector3 barrier)
        {
            Point p = barrier.RoundPosition().GetPoint(columnCount, rowCount);
            field[p.X, p.Y] = false;
            UpdatePath();
        }

        private bool IsClosedTo(Point point)
        {
            return (Math.Abs(transform.position.GetFloatX(columnCount) - point.X) < 0.01) &&
                (Math.Abs(transform.position.GetFloatY(rowCount) - point.Y) < 0.01);
        }

        override protected void SetNewDirection()
        {
            if (path != null && path.Count > 0)
            {
                if (IsClosedTo(path[1]))
                {
                    transform.position = transform.position.RoundPosition();
                    path.RemoveAt(0);
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
        
        private void UpdatePath()
        {
            path = AStar.FindPath(field, GetEnemyCoordinate(), lastSeenPlayerPosition);
            SetNextDirection();
        }

        private void Update()
        {
            if (IsRequireToUpdatePath())
            {
                lastSeenPlayerPosition = GetPlayerCoordinate();
                UpdatePath();
            }
        }

        private bool IsRequireToUpdatePath()
        {
            return (GetPlayerCoordinate() != lastSeenPlayerPosition) &&
                            field[lastSeenPlayerPosition.X, lastSeenPlayerPosition.Y] != true;
        }

        private void Start()
        {
            columnCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().columnCount;
            rowCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().rowCount;           
            SetField(GetWallsPointList());
            lastSeenPlayerPosition = GetPlayerCoordinate();
            UpdatePath();
        }
        
        private void SetField(List<Point> WallsPointList)
        {
            field = new bool[columnCount, rowCount];
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    field[i, j] = WallsPointList.Contains(new Point(i, j)) ? true : false;
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
            List<Point> letPointList = new List<Point>();
            GameObject[] unbreakableWalls = GameObject.FindGameObjectsWithTag("UnbreakableWall");
            GameObject[] breakableWalls = GameObject.FindGameObjectsWithTag("BreakableWall");
            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            letPointList.AddRange(GetPoints(unbreakableWalls));
            letPointList.AddRange(GetPoints(breakableWalls));
            letPointList.AddRange(GetPoints(bombs));
            return letPointList;
        }

        private List<Point> GetPoints(GameObject[] objects) {
            List<Point> points = new List<Point>();
            foreach (var obj in objects)
                points.Add(obj.transform.position.GetPoint(columnCount, rowCount));
            return points;
        }
    }
}
