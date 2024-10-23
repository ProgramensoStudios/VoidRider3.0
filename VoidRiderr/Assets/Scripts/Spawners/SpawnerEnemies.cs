using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    public StructSpawner[] spawners;

    private void OnTriggerEnter(Collider other)
    {
        for (int indexSpawner = 0;  indexSpawner < spawners.Length; indexSpawner++)
        {
            for (int i = 0; i < spawners[indexSpawner].points.Length; i++)
            {
                spawners[indexSpawner].enemyPool.AskForObject(spawners[indexSpawner].points[i].position);
            }
        }
    }

    [System.Serializable]
    public struct StructSpawner
    {
        public Transform[] points;
        public PoolManager enemyPool;
    }
}
