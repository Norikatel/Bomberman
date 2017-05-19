using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class IntelligentMove:MoveDynamicObject
    {
        int columnCount;
        int rowCount;

        private void OnCollisionEnter(Collision otherObject)
        {
            if (otherObject.collider.CompareTag("Player"))
                StartCoroutine(Effects.FadeDiactivate(otherObject.gameObject));
        }

        override protected void SetNewDirection()
        {
           
        }

        private void Start()
        {
            columnCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().columnCount;
            rowCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildController>().rowCount;
            List<Point> path =
              AStar.FindPath(GetField(GetWallsPointList()), GetEnemyProCoordinate(), GetPlayerCoordinate());
        }

        private int[,] GetField(List<Point> WallsPointList) {
            int[,] field = new int[columnCount, rowCount];
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    field[i, j]= WallsPointList.Contains(new Point(i, j)) ?  1 : 0;
            return field;
        }

        private Point GetPlayerCoordinate() {
            return GameObject.FindGameObjectWithTag("Player").transform.position.RoundPosition().GetPoint(columnCount, rowCount);
        }

        private Point GetEnemyProCoordinate()
        {
            return transform.position.RoundPosition().GetPoint(columnCount, rowCount);
        }

        private List<Point> GetWallsPointList() {
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
