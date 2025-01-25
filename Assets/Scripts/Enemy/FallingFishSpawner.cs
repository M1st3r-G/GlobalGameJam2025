using System.Collections;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class FallingFishSpawner : MonoBehaviour
    {
        [SerializeField] private Camera followCamera;
        [SerializeField] private GameObject fallingFish;
        [SerializeField] private float shootDelay;
        [SerializeField] private float spawnRangeRight;
        [SerializeField] private float spawnRangeLeft;
        [SerializeField] private float spawnHeight;
        [SerializeField] private float waitDelay;
        [SerializeField] private bool[] checkmark;

        private ProgressBar currentProgress;

        private int chapters;
        private bool m_canSpawn;
        private float m_rndPos;

        private void Awake()
        {
            StartCoroutine(SpawnFallingFish());
            StartCoroutine(SpawnWait());

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
                gameObject.transform.position = new Vector3(followCamera.transform.position.x + m_rndPos,
                    followCamera.transform.position.y + spawnHeight, followCamera.transform.position.z + 10);
                yield return new WaitForSeconds(shootDelay);
            }
        }

        private IEnumerator SpawnWait()
        {

            for (int i = 1; i < chapters + 1; i++)
            {
                yield return new WaitUntil(() => currentProgress.progress >= i / (float)chapters);
                m_canSpawn = checkmark[i - 1];
            }
        }
    }
}
