using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StorageBlocks")]
public class StorageBlocks : ScriptableObject
{
    [SerializeField] private BlockSpawnData[] _blocks;

    public IReadOnlyList<BlockSpawnData> Blocks => _blocks;
}