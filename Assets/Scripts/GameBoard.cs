using System.Collections.Generic;
using UnityEngine;
using static GameTileContent;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private GameTile _tilePrefab;
    private GameTile[] _tiles;
    private Vector2Int _size;
    private GameTileContentFactory _contentFactory;
    private Queue<GameTile> _mines = new Queue<GameTile>();
    
    public void Initialize(Vector2Int size, GameTileContentFactory contentFactory)
    {
        _size = size;
        _ground.localScale = new Vector3(_size.x, _size.y, 1f);
        _tiles = new GameTile[_size.x * _size.y];
        _contentFactory = contentFactory;
        Vector2 offset = new Vector2((_size.x - 1) / 2, (_size.y - 1) / 2);
        for (int y = 0, i = 0; y < _size.y; y++)
        {
            for (var x = 0; x < _size.x; x++, i++)
            {
                GameTile tile = _tiles[i] = Instantiate(_tilePrefab);
                tile.Index = i;
                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }

        // GameTile motherMine = _tiles[0];
        // motherMine.Content = _contentFactory.Get(GameTileContentType.Mine);
        ToggleMine(_tiles[8]);
        ToggleMine(_tiles[12]);
        SurroundMinesWithCounters();
    }

    public void SurroundMinesWithCounters()
    {
        foreach (var tile in _tiles)
        {
            if (tile.Content.Type == GameTileContentType.Mine)
            {
                _mines.Enqueue(tile);
            }
            tile.ClearTitle();
        }

        while (_mines.Count > 0)
        {
            GameTile t = _mines.Dequeue();
            IncreaseAllNeigbors(t);
        }

        foreach (var gameTile in _tiles)
        {
            gameTile.ShowCounter();
        }
    }

    private void IncreaseAllNeigbors(GameTile t)
    {
        int neighborN = t.Index + _size.x;
        int neighborNE = t.Index + _size.x + 1;
        int neighborE = t.Index + 1;
        int neighborSE = t.Index - _size.x + 1;
        int neighborS = t.Index - _size.x;
        int neighborSW = t.Index - _size.x - 1;
        int neighborW = t.Index - 1;
        int neighborNW = t.Index + _size.x - 1;

        if (neighborN < _size.x * _size.y)
            _tiles[neighborN].IncreaseMineCounter();

        if ((neighborNE < _size.x * _size.y) && (neighborNE % _size.x != 0))
            _tiles[neighborNE].IncreaseMineCounter();

        if (neighborE % _size.x != 0)
            _tiles[neighborE].IncreaseMineCounter();

        if ((neighborSE % _size.x != 0) && (neighborSE >= 0))
            _tiles[neighborSE].IncreaseMineCounter();

        if (neighborS >= 0)
            _tiles[neighborS].IncreaseMineCounter();

        if ((neighborSW >= 0) && (neighborSW + 1) % _size.x != 0)
            _tiles[neighborSW].IncreaseMineCounter();

        if ((neighborW + 1) % _size.x != 0)
            _tiles[neighborW].IncreaseMineCounter();

        if (((neighborNW + 1) % _size.x != 0) && (neighborNW < _size.x * _size.y))
            _tiles[neighborNW].IncreaseMineCounter();
    }

    public GameTile GetTile(Ray touchRay)
    {
        RaycastHit hit;
        if (Physics.Raycast(touchRay, out hit))
        {
            int x = (int)(hit.point.x + _size.x * 0.5f);
            int y = (int)(hit.point.z + _size.y * 0.5f);
            if (x >= 0 && x < _size.x && y >= 0 && y <= _size.y)
            {
                Debug.Log("Tile: " + x + " - " + y);
                Debug.Log(_tiles[x + y * _size.x].Content);
                Debug.Log("Index: " + _tiles[x + y * _size.x].Index);
                Debug.Log("Mine around: " + _tiles[x + y * _size.x].MineCounter);
                return _tiles[x + y * _size.x];
            }
        }
        return null;
    }

    public void ToggleMine(GameTile tile)
    {
        if (tile.Content.Type == GameTileContentType.Mine)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Empty);
        }
        else
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Mine);
        }
        SurroundMinesWithCounters();
    }
}