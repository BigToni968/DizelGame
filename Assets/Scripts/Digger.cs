using System.Collections;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField] private float _digDamage = 25f;
    [SerializeField] private float kdTimeDig = 1f;
    [SerializeField] private Transform _rayOrigin; // Объект, от которого пускается рейкаст
    [SerializeField] private Camera _targetCamera; // Камера, от которой берётся угол поворота
    [SerializeField] private float _rayLength = 10f; // Длина рейкаста
    [SerializeField] private LayerMask _layerMask; // Слой, с которым взаимодействует рейкаст

    private bool _isDig = true;
    private Coroutine _kdDig;
    private WaitForSeconds _wait;

    private void Update()
    {
        if (_rayOrigin == null || _targetCamera == null)
        {
            Debug.LogError("Ray origin or target camera is not assigned.");
            return;
        }

        if (Input.GetMouseButton(1))
        {
            Dig();
        }
    }

    private void Dig()
    {
        // Получаем угол поворота камеры по оси Y
        float cameraYRotation = _targetCamera.transform.eulerAngles.y;

        // Вычисляем направление рейкаста на основе угла поворота камеры
        Vector3 rayDirection = Quaternion.Euler(0, cameraYRotation, 0) * Vector3.forward;

        // Пускаем рейкаст от объекта
        if (Physics.Raycast(_rayOrigin.position, _targetCamera.transform.forward, out RaycastHit hit, _rayLength, _layerMask))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");
            Debug.DrawRay(_rayOrigin.position, rayDirection * hit.distance, Color.green);
            hit.collider.gameObject.TryGetComponent(out IDamage component);
            if (_isDig || _kdDig == null)
            {
                _isDig = false;
                component?.Set(_digDamage);
                _kdDig = StartCoroutine(waiteDig());
            }
        }
        else
        {
            Debug.DrawRay(_rayOrigin.position, rayDirection * _rayLength, Color.red);
        }

    }

    private IEnumerator waiteDig()
    {
        _wait ??= new WaitForSeconds(kdTimeDig);
        yield return _wait;
        _isDig = true;
        _kdDig = null;
    }
}