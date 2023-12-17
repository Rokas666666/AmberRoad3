using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHP;
    float HP;
    ParticleSystem particles;
    [SerializeField] AudioSource damageAudio;
    [SerializeField] AudioSource breakAudio;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }
    private void Update()
    {
        if (HP == 0)
        {
            if (breakAudio == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if (!breakAudio.isPlaying)
                {
                    StartCoroutine(DelayedDestroy());
                }
            }
        }
    }

    public float getHP()
    {
        return HP;
    }
    public void Damage(float damage)
    {
        Debug.Log("Tower Hp: " + HP);
        PlayParticles();
        HP -= damage;
        if(HP < 0) HP = 0;
        if (HP != 0) PlayDamageAudio();
    }
    public void PlayParticles()
    {
        if (particles != null)
        {
            particles.Play();
        }
    }
    public void PlayDamageAudio()
    {
        damageAudio.Play();
    }
    public void PlayBreakAudio()
    {
        breakAudio.Play();
    }
    private IEnumerator DelayedDestroy()
    {
        PlayBreakAudio();
        yield return new WaitForSeconds(breakAudio.clip.length);
        Destroy(gameObject);
        yield return null;
    }
}
