using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XaviEssencials.Runtime;
using XaviEssencials.Shared;

namespace XaviGames.Generator
{
    [ExecuteInEditMode]
    public class ScenarioGenerator : MonoBehaviour
    {
        [SerializeField]
        [Min(0f)]
        private float _width;

        [SerializeField]
        [Min(0f)]
        private float _length;

        [SerializeField] 
        private float _height;

        [SerializeField]
        private int _density;

        [SerializeField]
        private List<GameObject> _prefabs;

        [SerializeField]
        private Transform _pivotReference;

        [Header("Debug")]
        [SerializeField]
        private Color _debugColor = Color.green;

        [Button("Generate", false)]
        private void Generate()
        {
            GameObject father = new GameObject("Father");
            List<GameObject> objects = new();
            
            float minX = _pivotReference.position.x - (_width / 2f);
            float maxX = _pivotReference.position.x + (_width / 2f);
            float minZ = _pivotReference.position.z - (_length / 2f);
            float maxZ = _pivotReference.position.z + (_length / 2f);

            for (int i = 0; i < _density; i++)
            {
                int prefabIndex = Random.Range(0, _prefabs.Count);

                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(_pivotReference.position.y, _pivotReference.position.y + _height);
                float randomZ = Random.Range(minZ, maxZ);

                Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

#if UNITY_EDITOR
                GameObject prefab = _prefabs[prefabIndex];
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, father.transform);
                instance.transform.position = randomPosition;
#endif
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _debugColor;
            Vector3 center = _pivotReference.position;
            Vector3 size = new Vector3(_width, _height, _length);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
