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
        [SerializeField] private int chapters;
        [SerializeField] private ProgressBar currentProgress;
        [SerializeField] private bool[] checkmark;
        
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
            print($"{m_canSpawn}");
        }

        private IEnumerator SpawnFallingFish()
        {
            while (true)
            {
                if (m_canSpawn)
                {
                    yield return new WaitForSeconds(shootDelay);
                    Instantiate(fallingFish, gameObject.transform.position, gameObject.transform.rotation);
                    gameObject.transform.position = new Vector3(followCamera.transform.position.x + m_rndPos,
                        followCamera.transform.position.y + spawnHeight, followCamera.transform.position.z + 10);
                }

                yield return null;
            }
        }

        private IEnumerator SpawnWait()
        {
            while (true)
            {
                if (!m_canSpawn)
                {
                    for (int i = 1; i < chapters + 1; i++)
                    {
                        yield return new WaitUntil(() => currentProgress.progress >= i / (float)chapters);
                        m_canSpawn = checkmark[i - 1];
                    }
                    //Only Problem: Game Design, so that the Game doesn't crahsh, when the Player reaches the Top.
                }
                //yield return null;
            }
        }
    }
}
