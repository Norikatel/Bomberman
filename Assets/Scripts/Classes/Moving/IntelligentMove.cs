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
        int[,] field;
        Point currentPlayerPosition;

        public void AddLet(Vector3 let)
        {
            Point p = let.RoundPosition().GetPoint(columnCount, rowCount);
            field[p.X, p.Y] = 1;
            UpdatePath();
        }

        public void DeleteLet(Vector3 let)
        {
            Point p = let.RoundPosition().GetPoint(columnCount, rowCount);
            field[p.X, p.Y] = 0;
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
            path = AStar.FindPath(field, GetEnemyCoordinate(), currentPlayerPosition);
            SetNextDirection();
        }

        private void Update()
        {
            if (GetPlayerCoordinate() != currentPlayerPosition)
            {
                currentPlayerPosition = GetPlayerCoordinate();
                UpdatePath();
            }
        }

        private void Start()
        {
            columnCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().columnCount;
            rowCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().rowCount;
            field = new int[columnCount, rowCount];
            SetField(GetWallsPointList());
            currentPlayerPosition = GetPlayerCoordinate();
        }
        
        private void SetField(List<Point> WallsPointList)
        {
            field = new int[columnCount, rowCount];
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    field[i, j] = WallsPointList.Contains(new Point(i, j)) ? 1 : 0;
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
