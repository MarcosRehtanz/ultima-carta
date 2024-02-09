using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck_master_script : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;

    void Start()
    {

    }

    void Update()
    {
       
    }

    public Sprite GetSprite(int i)
    {
        return sprites[i];
    }
}
