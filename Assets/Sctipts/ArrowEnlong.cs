using UnityEngine;

public class ArrowEnlong : MonoBehaviour
{
    public float arAdd = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float arScale = 1f;
        bool msDown = Input.GetMouseButtonDown(0);
        bool msPush = Input.GetMouseButton(0);
        bool msUp   = Input.GetMouseButtonUp(0);

        if (msPush)
        {
            arScale = ++ arAdd/60;
        }
        if (msUp)
        {
            arScale = 1f;
            arAdd = 0f;
        }

        transform.localScale = new Vector3(arScale, 1, 1);
    }
}
