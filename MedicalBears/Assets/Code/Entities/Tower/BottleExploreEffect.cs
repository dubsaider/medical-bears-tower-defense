using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BottleExploreEffect : MonoBehaviour
{
    private ParticleSystem particle;
    public void Init(float radius)
    {
        particle = GetComponent<ParticleSystem>();

        var shapeModule = particle.shape;

        shapeModule.radius = radius;

        particle.Play();
    }
}
