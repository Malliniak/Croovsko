using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class PressureBar : MonoBehaviour
{
    private Image _image;

    [SerializeField] private GameEvent _pressureEvent;
    [SerializeField] private GameEvent _blockShoot;
    [SerializeField] private GameEvent _unlockShoot;

    private int _comboPoints;
    private bool _notPressure = true;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 0;
    }

    private void Start()
    {
        _unlockShoot.Raise();
        _image.DOFillAmount(1, (1-_image.fillAmount)*(2.5f - 0.03f * _comboPoints)).SetEase(Ease.InOutQuad);
        InvokeRepeating(nameof(ComboPoint), 0, 0.7f);
    }

    private void Update()
    {
        if (_image.fillAmount > 0.05f && _notPressure)
        {
            _unlockShoot.Raise();
        }
    }

    public void DecreaseFillAmmount()
    {
        Debug.Log("Fill amount decrease");
        _image.DOKill(false);
        _image.DOFillAmount(_image.fillAmount - (0.2f - (0.002f * _comboPoints)), 0.4f).SetEase(Ease.InOutQuad).OnComplete(Fill);
        if (_image.fillAmount < 0.05f)
        {
            _blockShoot.Raise();
        }
    }

    private void Fill()
    {
        _notPressure = true;
        _image.DOFillAmount(1, (1-_image.fillAmount)*(2.5f - 0.03f * _comboPoints)).SetEase(Ease.InOutQuad).OnComplete(PressureRelease);
    }

    private void PressureRelease()
    {
        _notPressure = false;
        _comboPoints = 0;
        _blockShoot.Raise();
        _pressureEvent.Raise();
        _image.DOFillAmount(0, 3.1f).OnComplete(Fill);
    }

    private void  ComboPoint()
    {
        _comboPoints++;
    }
}
