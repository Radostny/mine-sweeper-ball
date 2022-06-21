using UnityEngine;
using static GameTileContent;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private GameTile _tilePrefab;
    private GameTile[] _tiles;
    private Vector2Int _size;
    private GameTileContentFactory _contentFactory;
    
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
                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }
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
    }
}