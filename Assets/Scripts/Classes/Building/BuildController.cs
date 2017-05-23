using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class BuildController : MonoBehaviour
    {

        public int rowCount;
        public int columnCount;
        public int enemyCount;
        public int enemyProCount;

        void Start()
        {
            Builder builder = new Builder(rowCount, columnCount, new ResourceLoader());
            builder.BuildFloor();
            builder.BuildUnbreakableWalls();
            builder.BuildBreakableWalls();
            builder.AddPlayer();
            for (int i = 0; i < enemyCount; i++)
            {
                builder.AddEnemy();
            }
            for (int i = 0; i < enemyProCount; i++)
            {
                builder.AddEnemyPro();
            }
        }

        

    }
}