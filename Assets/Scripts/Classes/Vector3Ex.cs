using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Vector3Ex
    {
        public static Vector3 RoundPosition(this Vector3 position) {
            return new Vector3((float)Math.Round(position.x),
                position.y, (float)Math.Round(position.z));
        }
    }
}
