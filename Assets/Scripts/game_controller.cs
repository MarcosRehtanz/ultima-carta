using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour
{

    [SerializeField] List<GameObject> players;
    private int turn;
    private byte step;

    [SerializeField] GameObject discart;
    private byte activateSpecialCards;

    private bool playing;


    // Start is called before the first frame update
    void Start()
    {
        turn = players.Count - 1;
        step = 0;

        activateSpecialCards = 0;

        playing = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (step)
        {
            case 0: // 
                for (int i = 0; i < 7; i++)
                {
                    players[0].GetComponent<hand_player>().DrawCard();
                    players[1].GetComponent<hand_player>().DrawCard();
                }
                GamePlayer();
                step = 1;
                break;
            case 1:
                if (playing)
                    GamePlayer();
                else
                {
                    players[turn].GetComponent<hand_player>().YourTurn();
                    playing = true;
                }
                break;
            case 2:
                CompareSpecialCards();
                break;
            case 3:
                switch (activateSpecialCards)
                {
                    case 10:
                        Block();
                        break;
                    case 12:
                        MoreTwo();
                        break;
                    case 13:
                        MoreFour();
                        break;
                }
                step = 1;
                break;
        }

    }

    public void GamePlayer()
    {
        if (!players[turn].GetComponent<hand_player>().GetPlay())
        {
            if (turn == players.Count - 1)
            {
                turn = 0;
            }
            else
            {
                turn++;
            }

            playing = false;
            step = 2;
        }
    }

    private void CompareSpecialCards()
    {
        if (discart.GetComponent<discart_deck>().GetKeyCodeCard() > 53)
            activateSpecialCards = 13;
        else
            activateSpecialCards = (byte)(discart.GetComponent<discart_deck>().GetKeyCodeCard() % 13);
        
        step = 3;
    }

    private void Block()
    {
        if (discart.GetComponent<discart_deck>().GetIsBlock())
        {
            discart.GetComponent<discart_deck>().SetIsBlock();
            GamePlayer();
        }
    }

    private void MoreTwo()
    {
        if (discart.GetComponent<discart_deck>().GetIsMoreTwo() > 0)
        {
            players[turn].GetComponent<hand_player>().SetMoreTwo();
            step = 1;
        }
    }

    private void MoreFour()
    {
        if (discart.GetComponent<discart_deck>().GetIsMoreFour() > 0)
        {
            players[turn].GetComponent<hand_player>().SetMoreFour();
            step = 1;
        }
    }
}
