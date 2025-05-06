using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Julien.Script.Static
{
    public static class GameManagerStatic
    {
        public static List<Vector3> positions = new List<Vector3>{new Vector3(-4,0,-7), new Vector3(-2,0,-7), new Vector3(0,0,-7), new Vector3(2,0,-7)};
        
        public static List<GameObject> Players = new List<GameObject>();
    }
}
