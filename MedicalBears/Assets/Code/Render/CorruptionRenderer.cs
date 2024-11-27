using Extensions;
using UnityEngine;

namespace Code.Render
{
    public class CorruptionRenderer
    {
        public void RenderCellCorruption(MapCell cell, Sprite[] corruptionTiles)
        {
            var cellCorruptionHandler = cell.CorruptionHandler;
            
            if (cellCorruptionHandler.IsHealthy)
            {
                cellCorruptionHandler.corruptionSpriteRenderer.enabled = false;
                return;
            }

            if (!cellCorruptionHandler.IsMaxCorrupted)
                cellCorruptionHandler.corruptionSpriteRenderer.gameObject.transform.localRotation =
                    RotationRandomizer.GetRandom90DegreesRotation();

            var tileIndex = cellCorruptionHandler.GetCorruptionLevel() - 1;
            
            cellCorruptionHandler.corruptionSpriteRenderer.enabled = true;
            cellCorruptionHandler.corruptionSpriteRenderer.sprite = corruptionTiles[tileIndex];
        }
    }
}