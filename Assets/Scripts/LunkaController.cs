using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[SelectionBase]
public class LunkaController : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _lunka;
    [SerializeField] private Transform _paplavok;
    [SerializeField] private bool _isMove;
    [SerializeField] private bool _isFish;
    [SerializeField] private Animator _animator;

    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;
    public Transform Paplavok { get => _paplavok; private set => _paplavok = value; }
    public bool IsFish
    {
        get => _isFish; private set
        {
            _isFish = value;
            _animator?.SetBool("IsFish", _isFish);
        }
    }

    private void Update()
    {
        if (_isMove)
        {
            Ray ray = new Ray(_camera.position,_camera.forward);

            if(Physics.Raycast(ray, out RaycastHit hitInfo,10f,_mask))
            {
                Debug.Log(hitInfo.transform.name);
                _lunka.position = hitInfo.point;
            }
        }
    }

    /// <summary>
    /// Метод кнопки для включения перемещения
    /// </summary>
    public void EnablePoint()
    {
        _isMove = true;
    }

    /// <summary>
    /// Метод кнопки для выключения перемещения
    /// </summary>
    public void DisablePoint()
    {
        _isMove = false;
    }

    public async void StartFishing()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        await StartFishingAsync(_cancellationToken);
    }
    public void EndFishing()
    {
        if (IsFish)
        {
            Debug.Log("Я поймал рыбу");
            DnoScropt.instance.GiveFish(); //синглтон
            IsFish = false;
        }
        _cancellationTokenSource.Cancel();
    }

    private async UniTask StartFishingAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(1f, 5f)), cancellationToken: cancellationToken);
                IsFish = true;

                await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(1f, 2f)), cancellationToken: cancellationToken);
                IsFish = false;
            }
        }
        catch (Exception ex) 
        {
            Debug.Log("Ловля остановленна");
            IsFish = false;
        }
    }
}
