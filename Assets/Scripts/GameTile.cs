using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    private GameTileContent _content;

    public GameTileContent Content
    {
        get => _content;
        set
        {
            if (_content != null)
            {
                _content.Recycle();
            }
            _content = value;
            _content.transform.localPosition = transform.localPosition;
        }
    }
}
