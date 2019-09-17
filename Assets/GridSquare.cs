using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    [SerializeField] private Vector3Int position = Vector3Int.zero;
    public GridSquare nextSquare = null;
    public Vector3Int Position
    {
        get { return position; }
        set { position = value; }
    }

    [SerializeField] private List<GridSquare> neighbors = null;
    public List<GridSquare> Neighbors
    {
        get { return neighbors; }
    }
    private Dictionary<Vector3Int, GridSquare> neighborsByDirection = null;

    public GridSquare GetNeighbor(Vector3Int dir)
    {
        if (neighborsByDirection.ContainsKey(dir))
            return neighborsByDirection[dir];
        else
            return null;
    }

    public void AddNeighbor(GridSquare neighbor)
    {
        if (neighbors.Contains(neighbor))
            return;

        neighbors.Add(neighbor);
        CacheNeighborMap();
        Vector3Int dir = CalcDirectionToNeighbor(neighbor);
        neighborsByDirection[dir] = neighbor;
    }

    private void CacheNeighborMap(bool hardReset = false)
    {
        if (neighborsByDirection == null || hardReset)
        {
            neighborsByDirection = new Dictionary<Vector3Int, GridSquare>();
            foreach (GridSquare neighbor in neighbors)
            {
                Vector3Int dir = CalcDirectionToNeighbor(neighbor);
                neighborsByDirection.Add(dir, neighbor);
            }
        }
    }

    private Vector3Int CalcDirectionToNeighbor(GridSquare neighbor)
    {
        Vector3Int dir = neighbor.Position - position;
        return dir;
    }
}
