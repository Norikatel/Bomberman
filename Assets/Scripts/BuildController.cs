using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class BuildController : MonoBehaviour
    {

        public int rowCount;
        public int columnCount;

        void Start()
        {
            Builder builder = new Builder(rowCount, columnCount, new ResourceLoader());
            builder.BuildFloor();
            builder.BuildUnbreakableWalls();
            builder.BuildBreakableWalls();
            builder.AddPlayer();
            builder.AddEnemy();
            builder.AddEnemy();
            builder.AddEnemy();
            builder.AddEnemy();
        }

        void Update()
        {
        }
    }
}