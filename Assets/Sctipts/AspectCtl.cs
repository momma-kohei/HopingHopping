using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class AspectRatioKeeper : MonoBehaviour
{
    [Header("固定したいアスペクト比")]
    [SerializeField] private float targetAspectWidth = 1280f;
    [SerializeField] private float targetAspectHeight = 720f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateViewport();
    }

    private void Update()
    {
        UpdateViewport();
    }

    private void UpdateViewport()
    {
        if (cam == null) return;

        float targetAspect = targetAspectWidth / targetAspectHeight;
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;

        Rect rect = cam.rect;

        if (scaleHeight < 1.0f)
        {
            // 上下に黒帯を入れる（Letterbox）
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            // 左右に黒帯を入れる（Pillarbox）
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cam.rect = rect;
    }
}