using System.Collections;
using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSizeController : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private LayoutElement _scrollViewLayout;

    [SerializeField] private int _scrollViewMaxHeight;
    private RectTransform _scrollViewLayoutRectTransformCache;

    private IDisposable _disposable;
    private void Awake()
    {
        _scrollViewLayoutRectTransformCache = _scrollViewLayout.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        _disposable = _content.OnRectTransformDimensionsChangeAsObservable().Subscribe(u =>
        {
            if (_content.sizeDelta.y <= _scrollViewMaxHeight)
            {
                _scrollViewLayout.preferredHeight =  _content.sizeDelta.y;
                LayoutRebuilder.MarkLayoutForRebuild(_scrollViewLayoutRectTransformCache);
            }
        });
    }
    
    private void OnDisable()
    {
        _disposable?.Dispose();
    }
}
