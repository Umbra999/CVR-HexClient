using Hexed.Wrappers;
using RootMotion.FinalIK;
using UnityEngine;

namespace Hexed.Modules
{
    internal class FakeSerialize
    {
        private static GameObject SerializeCapsule;
        public static bool NoSerialize = false;

        public static void CreateCapsule()
        {
            SerializeCapsule = Object.Instantiate(PlayerWrappers.GetLocalPlayer().transform.Find("[PlayerAvatar]/_CVRAvatar(Clone)").gameObject, null, true);
            if (SerializeCapsule.GetComponent<LookAtIK>()) SerializeCapsule.GetComponent<LookAtIK>().enabled = false;
            SerializeCapsule.name = "Serialize Capsule";
            SerializeCapsule.transform.position = PlayerWrappers.GetLocalPlayer().transform.position;
            SerializeCapsule.transform.rotation = PlayerWrappers.GetLocalPlayer().transform.rotation;
        }

        public static void DeleteCapsule()
        {
            if (SerializeCapsule != null) Object.Destroy(SerializeCapsule);
        }

        public static void ToggleSerialize(bool State)
        {
            NoSerialize = State;
            if (NoSerialize) CreateCapsule();
            else DeleteCapsule();
        }
    }
}
