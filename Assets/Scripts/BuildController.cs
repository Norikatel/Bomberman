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
            ResourceLoader rs = new ResourceLoader();
            GameObject floor = rs.LoadFloor();
            GameObject unbreakableWall = rs.LoadUnbreakableWall();
            GameObject breakableWall = rs.LoadBreakableWall();
            Builder builder = new Builder(rowCount, columnCount);
            builder.BuildFloor(floor);
            builder.BuildUnbreakableWalls(unbreakableWall);
            builder.BuildBreakableWalls(breakableWall);
        }

        void Update()
        {
        }
    }
}