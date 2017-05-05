using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class BuildController : MonoBehaviour {

        public int rowCount = 9;
        public int columnCount = 9;

        void Start() {
            ResourceLoader rs = new ResourceLoader();            
            GameObject floor = rs.LoadFloor();
            GameObject unbreakableWall = rs.LoadUnbreakableWall();
            GameObject breakableWall = rs.LoadBreakableWall();
            Builder builder = new Builder();
            builder.BuildFloor(floor, rowCount, columnCount);
            builder.BuildUnbreakableWalls(unbreakableWall, rowCount, columnCount);
            builder.BuildBreakableWalls(breakableWall, rowCount, columnCount);
        }

        void Update() {
        }
    }
}