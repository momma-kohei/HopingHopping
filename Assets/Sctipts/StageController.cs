using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("スタート地点")] public GameObject[] startPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerObj != null && startPoint != null && startPoint.Length > 0)
        {
            playerObj.transform.position = startPoint[0].transform.position;
        }
        else
        {
            Debug.Log("設定が不十分です．");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
