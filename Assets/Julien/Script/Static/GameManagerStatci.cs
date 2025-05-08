using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Julien.Script.Static
{
    public static class GameManagerStatic
    {
        public static List<Vector3> positions = new List<Vector3>{new Vector3(-4,0,-7), new Vector3(-2,0,-7), new Vector3(0,0,-7), new Vector3(2,0,-7)};
        public static List<Vector4> Anchors = new List<Vector4>{new Vector4(0,0.84f,0.175f,1),new Vector4(0.825f,0.84f,1,1),new Vector4(0,0,0.175f,0.16f),new Vector4(0.825f,0,1,0.16f)};
        
        public static List<GameObject> Players = new List<GameObject>();
    }
}
