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
            return (new Point((int)(GetFloatX(position,columnCount)),
                (int)(GetFloatY(position,rowCount))));
        }

        public static float GetFloatX(this Vector3 position, int columnCount) {
            return position.x + columnCount / 2f - offset;
        }

        public static float GetFloatY(this Vector3 position, int rowCount)
        {
            return position.z + rowCount / 2f - offset;
        }
    }
}
