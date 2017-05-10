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
        private int rowCount;
        private int columnCount;
        private ResourceLoader resourceLoader;
        private System.Random rand = new System.Random();

        public Builder(int rowCount, int columnCount, ResourceLoader resourceLoader)
        {
            this.resourceLoader = resourceLoader;
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    emptyPlace.Add(new KeyValuePair<int, int>(i, j));
        }

        private void ClearRadius(float xPosition, float zPosition)
        {
            xPosition = xPosition + columnCount / 2 - offset;
            zPosition = zPosition + rowCount / 2 - offset;
            int radiusRow = (rowCount - 2) / 2;
            int radiusColumn = (columnCount - 2) / 2;
            for (int j = ((int)zPosition - radiusRow); j <= (zPosition + radiusRow); j++)
                for (int i = ((int)xPosition - radiusColumn); i <= (xPosition + radiusColumn); i++)
                    emptyPlace.Remove(new KeyValuePair<int, int>(i, j));
        }

        private GameObject AddDynamicObject(GameObject dynamicObject) {   
            KeyValuePair <int, int> randomCell = emptyPlace[rand.Next(emptyPlace.Count - 1)];
            GameObject gameObject = UnityEngine.Object.Instantiate(dynamicObject,
                new Vector3(randomCell.Key - columnCount / 2f + offset, dynamicObject.transform.lossyScale.y, randomCell.Value - rowCount / 2f + offset),
                new Quaternion(0, 0, 0, 0));          
                emptyPlace.Remove(randomCell);
            return gameObject;
        }


        public override void AddPlayer() {
            GameObject player = AddDynamicObject(resourceLoader.LoadPlayer());
            ClearRadius(player.transform.localPosition.x, player.transform.localPosition.z);
        }

        public override void AddEnemy()
        {
            AddDynamicObject(resourceLoader.LoadEnemy());
        }

        public override void BuildBreakableWalls()
        {
            int numberOfWalls = ((rowCount - 2) * (columnCount - 2)) / 5;
            BuildBreakableWalls(numberOfWalls);
        }

        public void BuildBreakableWalls(int numberOfWalls) {
            GameObject breakableWall = resourceLoader.LoadBreakableWall();
            breakableWall.transform.localScale = new Vector3(cubeEdge, cubeEdge, cubeEdge);
            for (int i = 0; i < numberOfWalls; i++)
            {
                KeyValuePair<int, int> randomCell = emptyPlace[rand.Next(emptyPlace.Count - 1)];
                UnityEngine.Object.Instantiate(breakableWall,
                    new Vector3(randomCell.Key - columnCount / 2f + offset, cubeEdge / 2, randomCell.Value - rowCount / 2f + offset),
                    new Quaternion(0, 0, 0, 0));
                emptyPlace.Remove(randomCell);
            }
        }

        public override void BuildUnbreakableWalls()
        {
            GameObject unbreakableWall = resourceLoader.LoadUnbreakableWall();
            unbreakableWall.transform.localScale = new Vector3(cubeEdge, cubeEdge, cubeEdge);
            for (int j = 0; j < rowCount; j++)
                for (int i = 0; i < columnCount; i++)
                    if (IsPlaceForUnbreakableWall(i, j, rowCount, columnCount))
                    {
                        UnityEngine.Object.Instantiate(unbreakableWall, 
                            new Vector3(i - columnCount / 2f + offset, cubeEdge / 2, j - rowCount / 2f + offset), 
                            new Quaternion(0, 0, 0, 0));
                        emptyPlace.Remove(new KeyValuePair<int, int>(i, j));
                    }
        }
        public override void BuildFloor()
        {
            GameObject floor = resourceLoader.LoadFloor();
            float planeScale = 10f;
            floor.transform.localScale = new Vector3(columnCount / planeScale, 1, rowCount / planeScale);
            UnityEngine.Object.Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        private bool IsPlaceForUnbreakableWall(int currentColumn, int currentRow, int rowCount, int columnCount)
        {
            return (currentRow == 0 || currentRow == rowCount - 1 || currentColumn == 0 || currentColumn == columnCount - 1
                || ((currentColumn % 2) == 0 && (currentRow % 2) == 0));
        }
    }
}
