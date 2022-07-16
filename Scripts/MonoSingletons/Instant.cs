using UnityEngine;


namespace MonoSinglentons
{
    public sealed class Instant : MonoBehaviour
    {
        // private static Instant _instance;
        // private static Instant instance {
        //     get {
        //         if (_instance == null)
        //         {
        //             var go = new GameObject("[MonoInstantiate]");
        //             _instance = go.AddComponent<Instant>();
        //             DontDestroyOnLoad(go);
        //         }
        //         return _instance;
        //     }
        // }


        public static GameObject InstantiateNewGO()
        {
            return new GameObject();
        }

        public static GameObject InstantiateNewGO(string name)
        {
            return new GameObject(name);
        }

        public static GameObject InstantiateNewGO(GameObject origin)
        {
            var go = GameObject.Instantiate(origin);
            return go;
        }

        public static GameObject InstantiateNewGO(GameObject origin, Vector3 position, Quaternion rotation)
        {
            var go = GameObject.Instantiate(origin, position, rotation);
            return go;
        }

        public static GameObject InstantiateNewGO(GameObject origin, Vector3 position, Quaternion rotation, Transform parent)
        {
            var go = GameObject.Instantiate(origin, position, rotation, parent);
            return go;
        }

        public static GameObject InstantiateNewGO(GameObject origin, Transform parent)
        {
            var go = GameObject.Instantiate(origin, parent);
            return go;
        }
        // public static GameObject InstantiateNewGO(GameObject origin, Transform parent)
        // {
        //     var go = GameObject.Instantiate(origin, Vector3.zero, Quaternion.identity, parent);
        //     return go;
        // }


    }
}
