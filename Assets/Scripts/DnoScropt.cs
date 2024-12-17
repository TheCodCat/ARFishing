using UnityEngine;

public class DnoScropt : MonoBehaviour
{
    public static DnoScropt instance;
    [SerializeField] private int _currentCount; //текущее количество рыб
    [SerializeField] private int _WinerCount; //победное количество рыб
    [SerializeField] private ParticleSystem _particleSystem; //эффект победы

    [SerializeField] private Transform _point; //место падения рыбы
    [SerializeField] private GameObject[] _fishs; //варианты рыбы
    private GameObject _currentFish; //текущая пойманная рыба

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.transform.parent?.gameObject} {_currentFish}");
        if (other.transform.parent.gameObject.Equals(_currentFish))
        {
            _currentCount++;
            if (_currentCount.Equals(_WinerCount))
            {
                Debug.Log("Победа");
                _particleSystem?.Play();
            }
        }
    }

    /// <summary>
    /// Добавляет рыбу на место сброса
    /// </summary>
    public void GiveFish()
    {
        _currentFish = Instantiate(_fishs[Random.Range(0, _fishs.Length - 1)], _point.position, Quaternion.identity);
    }
}
