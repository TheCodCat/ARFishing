using UnityEngine;

public class DnoScropt : MonoBehaviour
{
    public static DnoScropt instance;
    [SerializeField] private int _currentCount; //������� ���������� ���
    [SerializeField] private int _WinerCount; //�������� ���������� ���
    [SerializeField] private ParticleSystem _particleSystem; //������ ������

    [SerializeField] private Transform _point; //����� ������� ����
    [SerializeField] private GameObject[] _fishs; //�������� ����
    private GameObject _currentFish; //������� ��������� ����

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
                Debug.Log("������");
                _particleSystem?.Play();
            }
        }
    }

    /// <summary>
    /// ��������� ���� �� ����� ������
    /// </summary>
    public void GiveFish()
    {
        _currentFish = Instantiate(_fishs[Random.Range(0, _fishs.Length - 1)], _point.position, Quaternion.identity);
    }
}
