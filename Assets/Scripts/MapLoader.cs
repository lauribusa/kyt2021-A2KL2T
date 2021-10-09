using UnityEngine;

public class MapLoader : MonoBehaviour
{
	#region Exposed

    [SerializeField]
    private GameData _gameData;
	
	#endregion


    #region Private And Protected

    private int _index;

    #endregion
    
    
    #region Main

    public void LoadInitialMap()
    {
        _index = 0;
        Instantiate(_gameData.maps[_index], Vector3.zero, Quaternion.identity);
    }

    public void LoadNextMap()
    {
        if (!canLoad()) return;

        _index++;
        Instantiate(_gameData.maps[_index], Vector3.zero, Quaternion.identity);
    }

    #endregion


    #region Utils

    private bool canLoad() => _index < _gameData.maps.Length;
    
    #endregion
}