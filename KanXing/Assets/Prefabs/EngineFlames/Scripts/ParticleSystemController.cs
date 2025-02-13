using KXRocket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ParticleSystemInfo
{
    public float startSpeed;
    public float startSize;
    public float simulationSpeed;
    public float rateOverTime;
    public float lengthScale;
    public float maxParticleSize;
    public float shapeRadius;
    public Gradient startColor;
    public Gradient colorOverLifeTime;
}

public class ParticleSystemController : MonoBehaviour, IParticleSystemController
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer systemRenderer;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emmissionModule;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;
    private ParticleSystem.ShapeModule shapeModule;
    public float lerp = 0.0f;


    [Header("最小时的火焰")]
    public ParticleSystemInfo minParticleSystem;
    [Header("中途的火焰")]
    public ParticleSystemInfo middleParticleSystem;
    [Header("最大时的火焰")]
    public ParticleSystemInfo maxParticleSystem;

    [Header("最大的速率"), Range(0.0f, 1.0f)]
    public float maxRate = 0.8f;
    [Header("中等的速率"), Range(0.0f, 1.0f)]
    public float middleRate = 0.5f;
    [Header("最小的速率"), Range(0.0f, 1.0f)]
    public float minRate = 0.1f;

    [Header("最小火到最大火的时间")]
    public float zero2OneTimer = 3.0f;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        systemRenderer = GetComponent<ParticleSystemRenderer>();
        mainModule = _particleSystem.main;
        emmissionModule = _particleSystem.emission;
        colorOverLifetimeModule = _particleSystem.colorOverLifetime;
        shapeModule = _particleSystem.shape;
        mainModule.startSize = 0.0f;
    }

    public void SetLerp(float lerp)
    {
        this.lerp = lerp;
    }

    private void Update()
    {
        lerp = (float)Math.Floor(lerp * 100) / 100; // 只截取到百分位：1%
        if (!_particleSystem.isPlaying)
        {
            if (lerp > 0)
                _particleSystem.Play();
            if (lerp == 0)
                return;
        }

        if (lerp > 1)
            lerp = 1;
        else if (lerp < minRate)
        {
            lerp = 0;
            if (_particleSystem.isPlaying)
                _particleSystem.Stop(); // lerp为0则关闭粒子系统，停止渲染
            return;
        }
        UpdateParticleSystem();
    }

    private void UpdateParticleSystem()
    {
        //mainModule.startSize = Mathf.Lerp(minParticleSystem.startSize, lerp < maxRate ? middleParticleSystem.startSize:maxParticleSystem.startSize, lerp);
        //mainModule.startSpeed = Mathf.Lerp(minParticleSystem.startSpeed, lerp < maxRate ? middleParticleSystem.startSpeed : maxParticleSystem.startSpeed, lerp);
        //mainModule.simulationSpeed = Mathf.Lerp(minParticleSystem.simulationSpeed, lerp < maxRate ? middleParticleSystem.simulationSpeed : maxParticleSystem.simulationSpeed, lerp);
        //emmissionModule.rateOverTime = Mathf.Lerp(minParticleSystem.rateOverTime, lerp < maxRate ? middleParticleSystem.rateOverTime : maxParticleSystem.rateOverTime, lerp);
        //systemRenderer.lengthScale = Mathf.Lerp(minParticleSystem.lengthScale, lerp < maxRate ? middleParticleSystem.lengthScale : maxParticleSystem.lengthScale, lerp);
        //systemRenderer.maxParticleSize = Mathf.Lerp(minParticleSystem.maxParticleSize, lerp < maxRate ? middleParticleSystem.maxParticleSize : maxParticleSystem.maxParticleSize, lerp);

        mainModule.startSize = Mathf.Lerp(minParticleSystem.startSize, maxParticleSystem.startSize, lerp);
        mainModule.startSpeed = Mathf.Lerp(minParticleSystem.startSpeed, maxParticleSystem.startSpeed, lerp);
        mainModule.simulationSpeed = Mathf.Lerp(minParticleSystem.simulationSpeed, maxParticleSystem.simulationSpeed, lerp);
        emmissionModule.rateOverTime = Mathf.Lerp(minParticleSystem.rateOverTime, maxParticleSystem.rateOverTime, lerp);
        systemRenderer.lengthScale = Mathf.Lerp(minParticleSystem.lengthScale, maxParticleSystem.lengthScale, lerp);
        systemRenderer.maxParticleSize = Mathf.Lerp(minParticleSystem.maxParticleSize, maxParticleSystem.maxParticleSize, lerp);

        if (lerp >= maxRate)
        {
            shapeModule.radius = maxParticleSystem.shapeRadius;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(maxParticleSystem.startColor); // todo 每个update都new一个，有可能内存泄漏？
            colorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(maxParticleSystem.colorOverLifeTime);
        }
        else if (lerp >= middleRate)
        {
            shapeModule.radius = middleParticleSystem.shapeRadius;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(middleParticleSystem.startColor);
            colorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(middleParticleSystem.colorOverLifeTime);
        }
        else
        {
            shapeModule.radius = minParticleSystem.shapeRadius;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(minParticleSystem.startColor);
            colorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(minParticleSystem.colorOverLifeTime);
        }
    }

    IEnumerator ShowOrHide(bool isShow)
    {
        yield return null;
        mainModule.startSize = isShow ? mainModule.startSize : 0;
    }
}
