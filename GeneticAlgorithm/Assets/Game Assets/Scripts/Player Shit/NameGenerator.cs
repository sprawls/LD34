using UnityEngine;
using System.Collections;

public class NameGenerator : MonoBehaviour {

	public static string Randomize() {
        int length = Random.Range(2, 6);
        string name = "";

        for (int i = 0; i < length; i++) {
            int letters = Random.Range(1, 4);
            bool vowel = Random.Range(0, 2) == 0;
            for (int j = 0; j < letters; j++) {
                char next = vowel ? RandomVowel() : RandomConsonant();
                name += (i == 0 && j == 0) ? (char)((int)next - 32) : next;
                vowel = !vowel;
            }
        }

        return name;
    }

    private static char RandomVowel() {
        switch (Random.Range(0, 5)) {
            case 0: return 'a';
            case 1: return 'e';
            case 2: return 'i';
            case 3: return 'o';
            case 4: return 'u';
            default: return 'y';
        }
    }

    private static char RandomConsonant() {
        switch (Random.Range(0, 19)) {
            case 0: return 'b';
            case 1: return 'c';
            case 2: return 'd';
            case 3: return 'f';
            case 4: return 'g';
            case 5: return 'h';
            case 6: return 'j';
            case 7: return 'k';
            case 8: return 'l';
            case 9: return 'm';
            case 10: return 'n';
            case 11: return 'p';
            case 12: return 'q';
            case 13: return 'r';
            case 14: return 's';
            case 15: return 't';
            case 16: return 'v';
            case 17: return 'w';
            case 18: return 'x';
            default: return 'y';
        }
    }
}
