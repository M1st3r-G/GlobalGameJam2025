using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class LightningStorm : MonoBehaviour
    {
        [SerializeField] private GameObject cloudPrefab;
        [SerializeField] private GameObject lightningPrefab;

        public float damage => thunderDamage;
        [SerializeField] private float thunderDamage;
        
        public float thunderAlive => Random.Range(thunderLifespanAlive.x, thunderLifespanAlive.y);
        [SerializeField] private Vector2 thunderLifespanAlive;
        public float thunderDeath => Random.Range(thunderLifespanDead.x, thunderLifespanDead.y);
        [SerializeField] private Vector2 thunderLifespanDead;
        
        
        [SerializeField] private Transform[] clouds;
        [FormerlySerializedAs("m_numberOfClouds")] [SerializeField, ReadOnly] private int numberOfClouds;
        private readonly List<LightningController> m_lControllers = new ();
        
        private void Awake()
        {
            for (int i = 0; i < clouds.Length - 1; i++)
            {
                LightningController lightningController = Instantiate(lightningPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<LightningController>();
                lightningController.SetUp(clouds[i], clouds[i+1], this);
                m_lControllers.Add(lightningController);
            }
        }

        private void OnValidate()
        {
            if (clouds.Length == 0)
            {
                numberOfClouds = 0;
                return;
            }
            
            if (numberOfClouds == clouds.Length) return;
            if (numberOfClouds > clouds.Length)
            {
                numberOfClouds = clouds.Length;
                return;
            }
            
            clouds[^1] = Instantiate(cloudPrefab, Vector3.zero, Quaternion.identity, transform).transform;
            numberOfClouds=clouds.Length;
        }

        private void OnDestroy()
        {
            foreach (LightningController lC in m_lControllers) Destroy(lC.gameObject);
        }
    }
}
