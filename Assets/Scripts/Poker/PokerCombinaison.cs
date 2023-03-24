using Gotohell.Dice;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gotohell.Poker
{
    public enum PokerHand
    {
        None,
        OnePair,        // Two same cards
        TwoPair,        // Two same cards twice
        Three,          // Three same cards
        Full,           // Three same + one pair
        Four,           // Four same cards
        Poker           // Fice same cards
    }
    public class PokerCombinaison
    {
       

        private PokerHand _actualHand;
        public PokerHand ActualHand { get => _actualHand; }
        public PokerCombinaison()
        {
            _actualHand = PokerHand.None;
        }
        public void ChangeHand(PokerHand hand)
        {
            _actualHand = hand;
        }

        /* 
         * Return 1 if hand beat _actualHand
         * Return 0 if two hands are equals
         * Return -1 if hand loose against _actualHand
         */
        public int CompareHand(PokerHand hand)
        {
            if (hand > _actualHand)
            {
                return 1;
            }
            else if (hand < _actualHand)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public PokerHand BuildHand(List<DiceFace> faceArray)
        {
            List<DiceFace> disctinctFace = faceArray.Distinct().ToList();
            int distinct = disctinctFace.Count();
            if (distinct == 5)
            {
                _actualHand = PokerHand.None;
            }
            else if (distinct == 4)
            {
                _actualHand = PokerHand.OnePair;
            }
            else if (distinct == 4)
            {
                _actualHand = PokerHand.OnePair;
            }
            else if (distinct == 3)
            {
                for (int i = 0; i < disctinctFace.Count; i++)
                {
                    int count = faceArray.Select(o => o == disctinctFace[i]).Count(o => o == true);
                    if (count == 3)
                    {
                        _actualHand = PokerHand.Three;
                    }
                    else if (count == 2)
                    {
                        _actualHand = PokerHand.TwoPair;
                    }
                }
            }
            else if (distinct == 2)
            {
                int count = faceArray.Select(o => o == disctinctFace[0]).Count(o=> o == true);
                if (count == 1 || count == 4)
                {
                    _actualHand = PokerHand.Four;
                }
                else
                {
                    _actualHand = PokerHand.Full;
                }
            }
            else if (distinct == 1)
            {
                _actualHand = PokerHand.Poker;
            }
            else
            {
                _actualHand = PokerHand.None;
            }
            return _actualHand;
        }

        public override string ToString()
        {
            return _actualHand.ToString();
        }
    }
}