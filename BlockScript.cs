using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] AudioClip destroyBlockSound;
    [SerializeField] GameObject BlockParticleVFX;
    [SerializeField] float particleLifetime = 2f;
    
    [SerializeField] int timesHit;
    [SerializeField] Sprite[] hitSprite;
    LevelWin level;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<LevelWin>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }

    }


    void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprite.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprites();
        }
    }


    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprite[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite[spriteIndex];
        }
        else
        {
            Debug.Log("Blck Sprite is misssing from array!");
        }

    }



    void DestroyBlock()
    {
        FindObjectOfType<GameStatus>().AddToScore();        
        AudioSource.PlayClipAtPoint(destroyBlockSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerVFX();
    }

    void TriggerVFX()
    {
        GameObject particleVFX = Instantiate(BlockParticleVFX, transform.position, transform.rotation);
        Destroy(particleVFX, particleLifetime);
    }
}
