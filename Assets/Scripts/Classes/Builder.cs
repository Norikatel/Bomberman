using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class Builder : BuilderBase
    {
        List<KeyValuePair<int, int>> emptyPlace = new List<KeyValuePair<int, int>>();
        private const float cubeEdge = 0.9f;
        private const float offset = 0.5f;

        public Builder(int rowCount, int columnCount) {
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    emptyPlace.Add(new KeyValuePair<int, int>(i, j));
        }

        public override void BuildBreakableWalls(GameObject breakableWall, int rowCount, int columnCount) {
            breakableWall.transform.localScale = new Vector3(cubeEdge, cubeEdge, cubeEdge);
            int numberOfWalls = ((rowCount-2) * (columnCount-2)) / 5;
            System.Random rand = new System.Random();      
            for(int i =0;i<numberOfWalls;i++) {
                KeyValuePair<int, int> randomCell = emptyPlace[rand.Next(0, emptyPlace.Count - 1)];
                UnityEngine.Object.Instantiate(breakableWall, new Vector3(randomCell.Key - columnCount / 2f + offset, cubeEdge / 2, randomCell.Value - rowCount / 2f + offset), new Quaternion(0, 0, 0, 0));
                emptyPlace.Remove(randomCell);
            }
        }
        public override void BuildUnbreakableWalls(GameObject unbreakableWall,int rowCount, int columnCount) {
            unbreakableWall.transform.localScale = new Vector3(cubeEdge, cubeEdge, cubeEdge);
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    if (IsPlaceForUnbreakableWall(i, j, rowCount, columnCount))
                    {
                        UnityEngine.Object.Instantiate(unbreakableWall, new Vector3(i - columnCount / 2f + offset, cubeEdge / 2, j - rowCount / 2f + offset), new Quaternion(0, 0, 0, 0));
                        emptyPlace.Remove(new KeyValuePair<int, int>(i, j));
                    }
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
