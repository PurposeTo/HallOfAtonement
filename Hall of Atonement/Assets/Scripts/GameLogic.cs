using UnityEngine;

public static class GameLogic
{
    public static void Shuffle<T> (T[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            T temp = deck[i];
            int randomIndex = Random.Range(0, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }


    public static void Swap<T>(ref T x, ref T y)
    {
        T temp = x;
        x = y;
        y = temp;
    }
}
