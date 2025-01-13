using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Plane Settings")]
    public Vector2 planeSize = new Vector2(10, 10); // Размер плейна

    [field: SerializeField, Header("Block Settings")]
    public StorageBlocks StorageBlocks { get; private set; } // Данные о блоках

    [field: SerializeField]
    public Transform Parent { get; private set; }

    [Header("Spawn Settings")]
    public int maxBlocksPerPosition = 3; // Максимум блоков на одну позицию
    public List<float> fixedHeights = new List<float> { 0.5f, 1.5f, 2.5f }; // Фиксированные высоты

    [Header("Debug")]
    public bool visualizeGrid = true; // Визуализация сетки в редакторе

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        for (int x = 0; x < planeSize.x; x++)
        {
            for (int z = 0; z < planeSize.y; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                position += transform.position;

                for (int i = 0; i < maxBlocksPerPosition; i++)
                {
                    BlockSpawnData selectedBlock = GetRandomBlock();
                    if (selectedBlock != null)
                    {
                        float randomHeight = fixedHeights[i];
                        Vector3 spawnPosition = position + Vector3.up * randomHeight;
                        if (selectedBlock.blockPrefab == null || ReferenceEquals(selectedBlock.blockPrefab,null))
                        {
                            continue;
                        }
                        Block block = GameObject.Instantiate(selectedBlock.blockPrefab, Parent);
                        block.transform.localPosition = spawnPosition;
                    }
                }
            }
        }
    }

    BlockSpawnData GetRandomBlock()
    {
        float totalProbability = 0;
        foreach (var block in StorageBlocks.Blocks)
        {
            totalProbability += block.spawnProbability;
        }

        float randomValue = Random.Range(0, totalProbability);
        float cumulative = 0;

        foreach (var block in StorageBlocks.Blocks)
        {
            cumulative += block.spawnProbability;
            if (randomValue <= cumulative)
            {
                return block;
            }
        }

        return null;
    }

    void OnDrawGizmos()
    {
        if (!visualizeGrid) return;

        Gizmos.color = Color.gray;

        for (int x = 0; x <= planeSize.x; x++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(x, 0, 0), transform.position + new Vector3(x, 0, planeSize.y));
        }
        for (int z = 0; z <= planeSize.y; z++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0, 0, z), transform.position + new Vector3(planeSize.x, 0, z));
        }
    }
}

[System.Serializable]
public class BlockSpawnData
{
    public Block blockPrefab; // Префаб блока
    public float spawnProbability; // Процентная вероятность спавна
}
