using Julien.Script.Data.Zombie;
using System;

namespace Julien.Script.Static
{
    public static class EventBus
    {
        public static Action<float,float> OntakedDamage;
        public static Action<float> OnAddPoint;

        public static Action OnZombieDie;
    }
}
