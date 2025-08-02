using UnityEngine;

namespace AssetsController
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private string _path;
        [SerializeField] private Transform _handler;

        public string Path => _path;
        public Transform Handler => _handler;
    }
}