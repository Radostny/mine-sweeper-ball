using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private GameTile _tilePrefab;
    private GameTile[] _tiles;
    
    private Vector2Int _size;
    
    public void Initialize(Vector2Int size)
    {
        _size = size;
        _ground.localScale = new Vector3(_size.x, _size.y, 1f);
        _tiles = new GameTile[_size.x * _size.y];
        
        Vector2 offset = new Vector2((_size.x - 1) / 2, (_size.y - 1) / 2);
        for (int y = 0, i = 0; y < _size.y; y++)
        {
            for (var x = 0; x < _size.x; x++, i++)
            {
                GameTile tile = _tiles[i] = Instantiate(_tilePrefab);
                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
                
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
                return _tiles[x + y * _size.x];
            }
        }
        return null;
    }
}