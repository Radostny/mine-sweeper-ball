using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameTileContent;

[CreateAssetMenu]
public class GameTileContentFactory : ScriptableObject
{
    [SerializeField] private GameTileContent _emptyPrefab;
    [SerializeField] private GameTileContent _minePrefab;
    
    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }

    public void Hide(GameTileContent content)
    {
        content.gameObject.SetActive(false);
    }

    public GameTileContent Get(GameTileContentType type)
    {
        switch (type)
        {
            case GameTileContentType.Empty:
                return Get(_emptyPrefab);
            case GameTileContentType.Mine:
                return Get(_minePrefab);
        }
        return null;
    }

    private GameTileContent Get(GameTileContent prefab)
    {
        GameTileContent instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveToFactoryScene(instance.gameObject);
        return instance;
    }

    private Scene _contentScene;

    private void MoveToFactoryScene(GameObject o)
    {
        if (!_contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                _contentScene = SceneManager.GetSceneByName(name);
                if (!_contentScene.isLoaded)
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _contentScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(o, _contentScene);
    }
}
