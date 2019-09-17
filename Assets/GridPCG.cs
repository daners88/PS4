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

        for (int i = 0; i < totalSquares; i++)
        {
            inTree.Add(false);
        }
        grid.AllSquares[root].nextSquareID = -1;
        inTree[root] = true;
        for (int i = 0; i < totalSquares; i++)
        {
            int temp = i;
            while (!inTree[temp])
            {
                long nextId = GetRandomSuccessor(temp);
                grid.AllSquares[temp].nextSquareID = nextId;
                temp = (int)nextId;
            }
            temp = i;
            while (!inTree[temp])
            {
                inTree[temp] = true;
                temp = (int)grid.AllSquares[temp].nextSquareID;
            }
        }
        RemoveWalls(root);
    }

    public void RemoveWalls(int root)
    {
        int totalSquares = gridHeight * gridWidth;
        long prevID = -1;
        long curID = 0;
        for (int i = 0; i < totalSquares; i++)
        {
            if(curID > 0)
            {
                GridSquare curr = grid.AllSquares[(int)curID];
                curr.prevSquareID = prevID;
                prevID = curr.GetID();
                curID = curr.nextSquareID;
            }
        }

        for (int i = 0; i < totalSquares; i++)
        {
            if (grid.AllSquares[i].nextSquareID == grid.AllSquares[i].GetID() + 1 || grid.AllSquares[i].prevSquareID == grid.AllSquares[i].GetID() + 1)
            {
                grid.AllSquares[i].walls[2].SetActive(false);
            }
            if (grid.AllSquares[i].nextSquareID == grid.AllSquares[i].GetID() - 1 || grid.AllSquares[i].prevSquareID == grid.AllSquares[i].GetID() - 1)
            {
                grid.AllSquares[i].walls[3].SetActive(false);
            }
            if (grid.AllSquares[i].nextSquareID == grid.AllSquares[i].GetID() + 100 || grid.AllSquares[i].prevSquareID == grid.AllSquares[i].GetID() + 1000)
            {
                grid.AllSquares[i].walls[0].SetActive(false);
            }
            if (grid.AllSquares[i].nextSquareID == grid.AllSquares[i].GetID() - 100 || grid.AllSquares[i].prevSquareID == grid.AllSquares[i].GetID() - 100)
            {
                grid.AllSquares[i].walls[1].SetActive(false);
            }
        }
    }

    public long GetRandomSuccessor(int index)
    {
        GridSquare currentSquare = grid.AllSquares[index];

        return currentSquare.Neighbors[random.Next(0, currentSquare.Neighbors.Count)].GetID();
    }

    public void Build()
    {
        if (grid == null)
        {
            grid = CreateGrid();
        }

        if (random == null || generateNewRng)
        {
            random = CreateRng();
            grid.name = $"Dungeon Grid {seed}";
        }
        long curID = 0;
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                GridSquare sq = CreateSquare(new Vector3Int(i, 0, j), curID);
                sq.transform.position = grid.BoardToWorld(new Vector3Int(i, 0, j));
                sq.gameObject.SetActive(true);
                curID++;
            }
        }
        CreateRandomMaze(0);
    }

    //IEnumerator BuildDungeon()
    //{
    //    long curID = 0;
    //    for (int i = 0; i < gridWidth; i++)
    //    {
    //        for (int j = 0; j < gridHeight; j++)
    //        {
    //            GridSquare sq = CreateSquare(new Vector3Int(i, 0, j), curID);
    //            sq.transform.position = grid.BoardToWorld(new Vector3Int(i, 0, j));
    //            sq.gameObject.SetActive(true);
    //            curID++;
    //        }
    //        yield return null;
    //    }
    //    CreateRandomMaze(0);
    //}

    public void Clear()
    {
        if (grid != null)
        {
            DestroyImmediate(grid.gameObject);
        }
    }

    public GridSquare CreateSquare(Vector3Int pos, long curID)
    {
        GridSquare sq = Instantiate(squarePrefab, grid.transform);
        sq.Position = pos;
        sq.SetID(curID);
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
                seed = (int)Time.time * 1000;
            else
                seed = seedGenerator.Next();
        }

        random = new System.Random(seed);
        return random;
    }
}
