using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> imageLockMaps = new List<GameObject>();
    [SerializeField] private SaveDataManager dataManager;

    private List<Map> maps = new List<Map>();
    private void OnEnable()
    {
        dataManager.LoadData("map");
        maps = dataManager.mapdata.maps;
        for (int i = 0; i < maps.Count; i++)
        {
            imageLockMaps[i].SetActive(!maps[i].canOpen);

        }
    }
    }
