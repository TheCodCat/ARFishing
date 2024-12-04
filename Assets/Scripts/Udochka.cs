using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem.XR;

public class Udochka : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _point;
    private LunkaController _controller;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    private async void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out LunkaController component))
        {
            _controller = component;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _controller.StartFishing();
            await Fishing(_cancellationToken);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out LunkaController component))
        {
            _lineRenderer.positionCount = 0;
            _controller.EndFishing();
            _cancellationTokenSource.Cancel();
        }
    }
    private async UniTask Fishing(CancellationToken cancellationToken)
    {
        _lineRenderer.positionCount = 2;

        while (!cancellationToken.IsCancellationRequested)
        {
            //Debug.Log("Ловлю рыбу");

            _lineRenderer?.SetPosition(0, _point.position);
            _lineRenderer?.SetPosition(1, _controller.Paplavok.position);

            await UniTask.Yield();
        }
        
    }

    private void OnApplicationQuit()
    {
        _lineRenderer = null;
    }
}
