using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject ground, meanGrass, mossStone;


    public int minPlatLength = 1;
    public int maxPlatLength = 10;
    public int maxDangerLength = 3;
    public int maxY = 3;
    public int minY = -2;
    public int levelLength = 100;
    public int lowestPoint = 0;

    [Range(0.0f, 1.0f)]
    public float dangerChance = .5f;

    [Range(0.0f, 1.0f)]
    public float mossChance = .3f;

    [Range(0.0f, 1.0f)]
    public float meanGrassChance = .2f;

    private static int xOffset = 9;
    private static int yOffset = 2;
    private int blockX = 0;
    private int blockY;
    private int lastY;
    private bool isDangerous;

    void Start()
    {
        for (int i = 1; i < levelLength; i++)
        {
            float chance = Random.value;
            if (isDangerous)
            {
                int dangerLength = Mathf.RoundToInt(Random.Range(1, maxDangerLength));

                if (chance < meanGrassChance)
                {
                    blockY = lastY;

                    for (int j = 0; j < dangerLength; j++)
                    {
                        int x = blockX++ - xOffset;
                        int y = blockY - yOffset;
                        Instantiate(meanGrass, new Vector2(x, y), Quaternion.identity);
                    }
                }
                else if (chance < dangerChance)
                {
                    blockX += dangerLength;
                }

                isDangerous = false;
            }
            else if (chance < mossChance && !isDangerous)
            {
                int mossLength = Mathf.RoundToInt(Random.Range(minPlatLength, maxPlatLength));
                blockY = lastY;

                for (int j = 0; j < mossLength; j++)
                {
                    int x = blockX++ - xOffset;
                    int y = blockY - yOffset;
                    Instantiate(mossStone, new Vector2(x, y), Quaternion.identity);
                }
            }
            else
            {
                int platformLength = Mathf.RoundToInt(Random.Range(minPlatLength, maxPlatLength));
                blockY += Random.Range(minY, maxY);

                for (int j = 0; j < platformLength; j++)
                {
                    int x = blockX++ - xOffset;
                    int y = blockY - yOffset;
                    lastY = y;
                    Instantiate(ground, new Vector2(x, y), Quaternion.identity);
                }

                isDangerous = true;
            }

            if ((blockY - yOffset) < lowestPoint)
            {
                lowestPoint = blockY - yOffset;
            }
        }
    }
    
    void Update()
    {
        
    }
}
