using UnityEngine;


namespace MonoSinglentons
{
    public sealed class Instant : MonoBehaviour
    {

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

    }
}
