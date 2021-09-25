using UnityEngine;

namespace _Project.Enemies.Scripts
{
    public class TestEnemy : Enemy
    {
        [SerializeField]
        private bool isMovingBack;

        private Rigidbody rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        // Update is called once per frame
        void Update()
        {
            if (isMovingBack)
            {
                rb.velocity = new Vector3(-4, 0, 0);
            }
        }
    }
}
