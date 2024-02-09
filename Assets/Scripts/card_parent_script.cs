using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_parent_script : MonoBehaviour
{

    private const int countCards = 13;
    public int keyCodeCard;

    private Sprite sprite;
    private int color;
    private int number;
    private bool isNumber;
    private bool isBlock;
    private bool isReverse;
    private byte isMoreTwo;
    private byte isMoreFour;

    // Start is called before the first frame update
    void Start()
    {
        color = 0;
        number = 0;
        isNumber = true;
        isBlock = false;
        isReverse = false;
        isMoreTwo = 0;
        isMoreFour = 0;
    }

    void Update()
    {

    }

    public void SetCardNumber(Sprite s, int i)
    {
        keyCodeCard = i;
        sprite = s;
        color = i / countCards;
        number = i % countCards;

        if (number > 9)
            SetSpecialCard((byte)number);
        else if (i > 53)
            isMoreFour++;

        gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
    public void SetCardNumber(Sprite s, int i, int c)
    {
        keyCodeCard = i;
        sprite = s;
        color = c;
        number = i;
        if (i > 53)
            isMoreFour++;
        gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetKeyCodeCard()
    {
        return keyCodeCard;
    }

    public bool CompareKeyCodeCard(int kcc)
    {
        return kcc == keyCodeCard;
    }
    public bool CompareColor(int c)
    {
        return c/countCards == color;
    }
    public bool CompareInteger(int i)
    {
        return i % countCards == number;
    }

    private void SetSpecialCard(byte b)
    {
        switch (b)
        {
            case 10:
                isBlock = true;
                break;
            case 12:
                isMoreTwo++;
                break;
        }
    }


    public void SetIsMoreTwo()
    {
        isMoreTwo = 0;
    }
    public void SetIsMoreFour()
    {
        isMoreFour = 0;
    }
    public void SetIsBlock()
    {
        isBlock = false;
    }
    public byte GetIsMoreTwo()
    {
        return isMoreTwo;
    }
    public byte GetIsMoreFour()
    {
        return isMoreFour;
    }
    public bool GetIsBlock()
    {
        return isBlock;
    }
}
