using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPCG : MonoBehaviour
{
    public Grid grid = null;
    public GridSquare squarePrefab = null;

    public int gridWidth = 100;
    public int gridHeight = 100;

    public bool generateNewRng = false;
    public bool generateRandomSeed = true;
    public System.Random random = null;
    public System.Random seedGenerator = null;
    public int seed = 0;

    public List<Vector3Int> directions = new List<Vector3Int>() {
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 0, -1)
    };

    public Vector3Int start = Vector3Int.zero;

    public void CreateRandomMaze(int root)
    {
        int totalSquares = gridHeight * gridWidth;
        List<bool> inTree = new List<bool>();
        List<GridSquare> mazePath = new List<GridSquare>();

        for(int i = 0; i < totalSquares; i++)
        {
            inTree.Add(false);
        }
        mazePath[root] = null;
        inTree[root] = true;
        for (int i = 0; i < totalSquares; i++)
        {
            int temp = i;
            while(!inTree[temp])
            {
                mazePath[temp] = GetRandomSuccessor(temp);
            }
        }
    }

    public GridSquare GetRandomSuccessor(int index)
    {
        GridSquare currentSquare = grid.AllSquares[index];

    }

    public void Build()
    {
        for(int i = 0; i < gridWidth; i++)
        {
            for(int j = 0; j < gridHeight; j++)
            {
                GridSquare sq = Instantiate(squarePrefab, grid.transform);
                sq.Position = new Vector3Int(i, 0, j);
                grid.AddSquare(sq);
            }
        }
    }

    public void Clear()
    {
        if (grid != null)
        {
            Destroy(grid.gameObject);
        }
    }

    public GridSquare CreateSquare(Vector3Int pos)
    {
        GridSquare sq = Instantiate(squarePrefab, grid.transform);
        sq.Position = pos;
        grid.AddSquare(sq);
        return sq;
    }

    public Grid CreateGrid()
    {
        GameObject gridObj = new GameObject("Dungeon Grid");
        Grid g = gridObj.AddComponent<Grid>();
        return g;
    }

    public System.Random CreateRng()
    {
        if (generateRandomSeed)
        {
            if (seedGenerator == null)
                seed = (int)Time.time*1000;
            else
                seed = seedGenerator.Next();
        }

        random = new System.Random(seed);
        return random;
    }
}
