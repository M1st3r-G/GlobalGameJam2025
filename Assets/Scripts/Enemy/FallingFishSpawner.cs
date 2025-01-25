using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class FallingFishSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("camera")] [SerializeField] private Camera followCamera;
        [SerializeField] private GameObject fallingFish;
        [SerializeField] private float shootDelay;
        [SerializeField] private float spawnRangeRight;
        [SerializeField] private float spawnRangeLeft;
        [SerializeField] private float spawnHeight;
    
        private float m_rndPos;

        private void Awake()
        {
            StartCoroutine(SpawnFallingFish());
        }

        private void Update()
        {
            m_rndPos = Random.Range(spawnRangeLeft, spawnRangeRight);
        }

        private IEnumerator SpawnFallingFish()
        {
            while (true)
            {
                Instantiate(fallingFish, gameObject.transform.position, gameObject.transform.rotation);
                gameObject.transform.position = new Vector3(followCamera.transform.position.x + m_rndPos, followCamera.transform.position.y + spawnHeight, followCamera.transform.position.z + 10);
                yield return new WaitForSeconds(shootDelay);
            }
        }
    }
}
