using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip[] blockSounds;
    [SerializeField] GameObject blockParticles;
    [SerializeField] int maxHits;
    [SerializeField] Sprite damagedSprite;
    //cached ref
    Level level;

    //state variables
    int timesHit;
    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;
            if(timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
            
        }
        else
        {
            AudioClip clip = blockSounds[UnityEngine.Random.Range(0, blockSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.3f);
        }
    }

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = damagedSprite;
    }

    private void DestroyBlock()
    {
        AudioClip clip = blockSounds[UnityEngine.Random.Range(0, blockSounds.Length)];
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.3f);
        FindObjectOfType<GameSession>().AddToScore();
        TriggerParticles();
        Destroy(gameObject);
        level.BlockDestroyed();
        
    }
    private void TriggerParticles()
    {
        Vector3 partPosition = new Vector3(transform.position.x, transform.position.y, -1);
        GameObject particles = Instantiate(blockParticles,partPosition,transform.rotation);
        Destroy(particles, 2f);
    }
}
