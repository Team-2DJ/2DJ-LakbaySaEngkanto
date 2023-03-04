using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BookMiniGame : MonoBehaviour
{
    [SerializeField] private List<BookSlot> bookSlotPrefabs;
    [SerializeField] private BookPiece bookPiecePrefab;
    [SerializeField] private Transform bookSlotParent, bookPieceParent;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        var randomSet = bookSlotPrefabs.OrderBy(s => Random.value).Take(3).ToList();

        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(randomSet[i], bookSlotParent.GetChild(i).position, Quaternion.identity);
            var spawnedPiece = Instantiate(bookPiecePrefab, bookPieceParent.GetChild(i).position, Quaternion.identity);

            spawnedPiece.Initialize(spawnedSlot);
        }
    }
}
