using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class Builder:BuilderBase
    {
        private const float cubeEdge = 0.9f;
        private const float offset = 0.5f;

        public override void BuildBreakableWalls(GameObject breakableWall, int rowCount, int columnCount) {
            breakableWall.transform.localScale= new Vector3(cubeEdge, cubeEdge, cubeEdge);
            int numberOfWalls = ((rowCount-2) * (columnCount-2)) / 5;
            System.Random rand = new System.Random();
            int countWall = 0;
            while (countWall != numberOfWalls) {
                int currentColumn = rand.Next(1, columnCount - 2);
                int currentRow = rand.Next(1, rowCount - 2);
                if (!IsPlaceForUnbreakableWall(currentColumn, currentRow, rowCount, columnCount))
                {
                    UnityEngine.Object.Instantiate(breakableWall, new Vector3(currentColumn - columnCount / 2f + offset, cubeEdge / 2, currentRow - rowCount / 2f + offset), new Quaternion(0, 0, 0, 0));
                    countWall++;
                }
            }
        }
        public override void BuildUnbreakableWalls(GameObject unbreakableWall,int rowCount, int columnCount) {
            unbreakableWall.transform.localScale = new Vector3(cubeEdge, cubeEdge, cubeEdge);
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    if (IsPlaceForUnbreakableWall(i,j,rowCount,columnCount))
                        UnityEngine.Object.Instantiate(unbreakableWall, new Vector3(i - columnCount / 2f + offset, cubeEdge / 2, j - rowCount / 2f + offset), new Quaternion(0, 0, 0, 0));

        }
        public override void BuildFloor(GameObject floor,int rowCount, int columnCount) {
            floor.transform.localScale = new Vector3(columnCount / 10f, 1, rowCount / 10f);
            UnityEngine.Object.Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        private bool IsPlaceForUnbreakableWall(int currentColumn,int currentRow, int rowCount, int columnCount) {
            return (currentRow == 0 || currentRow == rowCount - 1 || currentColumn == 0 || currentColumn == columnCount - 1
                || ((currentColumn % 2) == 0 && (currentRow % 2) == 0));
        }

    }
}
