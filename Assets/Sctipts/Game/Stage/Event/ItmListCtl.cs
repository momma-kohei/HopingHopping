using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ItmListCtl : MonoBehaviour
{
    private List<GameObject> itemList = new List<GameObject>();
    private int _stageNum = 0;

    private void Awake()
    {
        _stageNum = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void Start()
    {
        int j = 0;
        foreach (Transform item in gameObject.transform)
        {
            itemList.Add(item.gameObject);
            itemList[j].GetComponent<ScrItemCtl>().Index = j;
            j++;
        }

        if (GmMgr.Instance.ItemSituation[_stageNum - 1].Count < itemList.Count)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                GmMgr.Instance.ItemSituation[_stageNum-1].Add(true);
            }
        }
        

        for (int i = 0; i < itemList.Count; i++)
        {
            if (!GmMgr.Instance.ItemSituation[_stageNum - 1][i])
            {
                Destroy(itemList[i]);
            }
        }
    }
}
