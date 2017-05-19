using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Vector3Ex
    {
        private static float offset = 0.5f;

        public static Vector3 RoundPosition(this Vector3 position) {
            return new Vector3((float)Math.Round(position.x),
                position.y, (float)Math.Round(position.z));
        }
        
        public static Point GetPoint(this Vector3 position, int columnCount, int rowCount) {
            return (new Point((int)(position.x + columnCount / 2f - offset),
                (int)(position.z + rowCount / 2f - offset)));
        }
    }
}
