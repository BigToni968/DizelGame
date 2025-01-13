using UnityEngine;

public class MouseYHeightController : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // Объект, высоту которого нужно изменять
    [SerializeField] private Camera mainCamera; // Камера для определения позиции мыши
    [SerializeField] private float minHeight = 0.5f; // Минимальная высота объекта
    [SerializeField] private float maxHeight = 5f; // Максимальная высота объекта
    [SerializeField] private float sensitivity = 1f; // Чувствительность изменения высоты

    private void Update()
    {
        if (targetObject == null || mainCamera == null)
        {
            Debug.LogError("Target object or main camera is not assigned.");
            return;
        }

        // Получаем позицию мыши в экранных координатах
        Vector3 mousePosition = Input.mousePosition;

        // Преобразуем позицию мыши в мировые координаты
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // Вычисляем новую высоту объекта
        float normalizedY = Mathf.InverseLerp(0, Screen.height, mousePosition.y); // Нормализуем Y от 0 до 1
        float newHeight = Mathf.Lerp(minHeight, maxHeight, normalizedY * sensitivity);

        // Устанавливаем новую высоту объекта
        Vector3 targetPosition = targetObject.position;
        targetPosition.y = newHeight;
        targetObject.position = targetPosition;
    }
}