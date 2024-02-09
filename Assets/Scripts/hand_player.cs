using System.Collections.Generic;
using UnityEngine;

public class hand_player : MonoBehaviour
{

    [SerializeField] GameObject deckMaster;
    [SerializeField] GameObject discart;
    [SerializeField] GameObject cardPrefab;

    private List<GameObject> cards;
    private Vector3 cardPosition;

    private Vector3 selectorPosition;
    private int selector;

    private bool play;
    private bool moreTwo;
    private bool moreFour;
    private bool draw;
    private bool change;

    void Start()
    {
        play = false;
        moreTwo = false;
        moreFour = false;
        draw = false;
        change = false;
        cards = new List<GameObject>();
        cardPosition = new Vector3(-5, transform.position.y, 0);
        selectorPosition = new Vector3(-5, transform.position.y + 0.5f, 0);
        selector = 0;
    }

    void Update()
    {
        if (play)
        {
            if (!moreTwo && !moreFour && !change)
            {

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (!draw)
                        DrawCard();
                    else
                        Next();
                }

                if (cards.Count > 0 && play)
                {
                    Selection();
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        Discart();
                }
            }
            else if (moreTwo && !moreFour && !change)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    for (byte b = 0; b < discart.GetComponent<discart_deck>().GetIsMoreTwo(); b++)
                    {
                        DrawCard();
                        DrawCard();
                    }
                    discart.GetComponent<discart_deck>().SetIsMoreTwo();
                    Next();
                }

                if (cards.Count > 0 && play)
                {
                    Selection();
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        Discart();
                }
            }
            else if (change)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    ChangeColor(0);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    ChangeColor(1);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    ChangeColor(2);
                if (Input.GetKeyDown(KeyCode.Alpha4))
                    ChangeColor(3);
            }
            else if (!moreTwo && moreFour && !change)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    for (byte b = 0; b < discart.GetComponent<discart_deck>().GetIsMoreFour(); b++)
                    {
                        DrawCard();
                        DrawCard();
                        DrawCard();
                        DrawCard();
                    }
                    discart.GetComponent<discart_deck>().SetIsMoreFour();
                    Next();
                }

                if (cards.Count > 0 && play)
                {
                    Selection();
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        Discart();
                }
            }
        }
    }

    public void DrawCard()
    {

        cards.Add(Instantiate(cardPrefab));
        int rn = Random.Range(0, 56);
        Sprite sprite = deckMaster.GetComponent<deck_master_script>().GetSprite(rn);
        cards[^1].GetComponent<card_parent_script>().SetCardNumber(sprite, rn);


        cards[^1].transform.SetParent(transform);
        cards[^1].transform.position = cardPosition;
        cardPosition.x += 0.5f;

        draw = true;
    }

    private void Selection()
    {
        selectorPosition.x = cards[selector].transform.position.x;
        cards[selector].transform.position = selectorPosition;

        if (Input.GetKeyDown(KeyCode.RightArrow) && selector < cards.Count - 1)
        {
            Vector3 v = cards[selector].transform.position;
            v.y -= 0.5f;
            cards[selector].transform.position = v;

            selector++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && selector > 0)
        {
            Vector3 v = cards[selector].transform.position;
            v.y -= 0.5f;
            cards[selector].transform.position = v;

            selector--;
        }
    }

    private void Discart()
    {
        int kcc = cards[selector].GetComponent<card_parent_script>().GetKeyCodeCard();

        bool compare;
        if (moreTwo)
            compare = discart.GetComponent<discart_deck>().CompareInteger(kcc);
        else if (moreFour)
            compare = discart.GetComponent<discart_deck>().CompareKeyCodeCard(kcc);
        else
            compare = (discart.GetComponent<discart_deck>().CompareColor(kcc) || discart.GetComponent<discart_deck>().CompareInteger(kcc));

        if (compare && kcc < 52)
        {
            Sprite s = cards[selector].GetComponent<card_parent_script>().GetSprite();
            discart.GetComponent<discart_deck>().SetCardNumber(s, kcc);
            Destroy(cards[selector]);
            cards.RemoveAt(selector);


            Vector3 v;
            for (int i = selector; i < cards.Count; i++)
            {
                v = cards[i].transform.position;
                v.x -= 0.5f;
                cards[i].transform.position = v;
            }

            cardPosition.x -= 0.5f;

            if (cards.Count == selector && cards.Count > 0)
                selector--;

            Next();

        }
        else if (kcc >= 52 && !moreFour && !moreTwo)
        {
            change = true;
        }
        else if (kcc >= 54 && moreFour && !moreTwo)
        {
            change = true;
        }
    }
    private void ChangeColor(int c)
    {
        int kcc = cards[selector].GetComponent<card_parent_script>().GetKeyCodeCard();

        Sprite s = cards[selector].GetComponent<card_parent_script>().GetSprite();
        discart.GetComponent<discart_deck>().SetCardNumber(s, kcc, c);
        Destroy(cards[selector]);
        cards.RemoveAt(selector);


        Vector3 v;
        for (int i = selector; i < cards.Count; i++)
        {
            v = cards[i].transform.position;
            v.x -= 0.5f;
            cards[i].transform.position = v;
        }

        cardPosition.x -= 0.5f;

        if (cards.Count == selector && cards.Count > 0)
            selector--;

        Next();
    }

    public void Next()
    {
        if (cards.Count > 0 && play)
        {
            Vector3 v = cards[selector].transform.position;
            v.y = transform.position.y;
            cards[selector].transform.position = v;
        }
        play = false;
        moreTwo = false;
        moreFour = false;
        change = false;
        draw = false;
    }

    public bool GetPlay()
    {
        return play;
    }

    public void YourTurn()
    {
        play = true;
    }

    public void SetMoreTwo()
    {
        moreTwo = true;
    }
    public void SetMoreFour()
    {
        moreFour = true;
    }
}
