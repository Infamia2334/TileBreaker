using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelWin : MonoBehaviour
{
    [SerializeField] int breakableBlocks;
    // Start is called before the first frame update
    SceneLoader scene;
    void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
    }
    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            scene.LoadNextScene();
        }
    }
}
