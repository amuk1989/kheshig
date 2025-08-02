using UnityEngine;

namespace Environment.TestEnv
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private float _angle = 45f;
        private void Update()
        {
            _angle += _speed * Time.deltaTime;
            _angle = Mathf.Clamp(_angle, 0f, 360f);

            transform.position = new Vector3(Mathf.Sin(_angle) * 4f,0.6f, Mathf.Cos(_angle) * 4f);
        }
    }
}