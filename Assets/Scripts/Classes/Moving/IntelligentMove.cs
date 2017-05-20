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

        private bool IsClosedTo(Point point)
        {
            return (Math.Abs(transform.position.GetFloatX(columnCount) - point.X) < 0.1) &&
                (Math.Abs(transform.position.GetFloatY(rowCount) - point.Y) < 0.1);
        }

        override protected void OnCollisionStay(Collision other) { }

        override protected void SetNewDirection()
        {
            if (CanMove())
            {
                if (IsClosedTo(path[1]))
                {
                    transform.position = transform.position.RoundPosition();
                    path.RemoveAt(0);
                    SetNextDirection();
                }
            }
        }

        private void SetNextDirection()
        {
            if (CanMove())
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

        protected override bool CanMove()
        {
            return path != null;
        }

        private IEnumerator UpdatePath()
        {
            path = AStar.FindPath(GetField(GetWallsPointList()), GetEnemyProCoordinate(), GetPlayerCoordinate());
            yield return new WaitForSeconds(1f);
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

        private Point GetEnemyProCoordinate()
        {
            return transform.position.RoundPosition().GetPoint(columnCount, rowCount);
        }

        private List<Point> GetWallsPointList()
        {
            List<Point> WallsPointList = new List<Point>();
            GameObject[] UnbreakableWalls = GameObject.FindGameObjectsWithTag("UnbreakableWall");
            GameObject[] BreakableWalls = GameObject.FindGameObjectsWithTag("BreakableWall");
            foreach (var wall in UnbreakableWalls)
                WallsPointList.Add(wall.transform.position.GetPoint(columnCount, rowCount));
            foreach (var wall in BreakableWalls)
                WallsPointList.Add(wall.transform.position.GetPoint(columnCount, rowCount));
            return WallsPointList;
        }
    }
}
