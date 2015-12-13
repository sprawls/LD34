using UnityEngine;
using System.Collections;

public class QuoteManager : MonoBehaviour {

    static string[,] Quotes = new string[,] {   { "Just do it.", "~ Shia Labeouf"}, 
                                                { "Try to be a rainbow in someone's cloud.", "~ Maya Angelou" },
                                                { "Perfection is not attainable, but if we chase perfection we can catch excellence.", "~ Vince Lombardi" },
                                                { "Believe you can and you're halfway there.", "~ Theodore Roosevelt" },
                                                { "Nothing in life is promised except death.", "~ Kanye West" },
                                                { "Aim for the moon. If you miss, you may hit a star.", "~ W. Clement Stone" },
                                                { "Sometimes science is a lot more art, than science. A lot of people don't get that.", "~ Rick Sanchez" },
                                                { "Giving up is always an option, but not always a failure.", "~ Cameron Conaway" },
                                                { "There are more cells in our brains, than there are brains in our entire body.", "~ Richard Dawkins"},
                                                {"Sometimes when I close my eyes, I can't see.", "~ DubstepKitty18"}
    };


    public static string[] GetQuote() {
        int index = Random.Range(0, Quotes.Length/2);
        string[] returnStrings = { Quotes[index,0],Quotes[index,1]} ;
        return returnStrings;
    }
}
