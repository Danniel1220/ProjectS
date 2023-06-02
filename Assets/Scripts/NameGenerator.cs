using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    private const string lowerCaseVowels = "aeiou";
    private const string upperCaseVowels = "AEIOU";

    private const string lowerCaseConsonants = "bcdfghjklmnpqrstvwxyz";
    private const string upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

    public string generateRandomName()
    {
        string name;
        int nameLenght = Random.Range(4, 9);

        // 50/50 chance of the name starting with a vowel or consonant
        if (Random.Range(0, 2) == 0)
        {
            name = upperCaseVowels[Random.Range(0, upperCaseVowels.Length)].ToString();
            for (int i = 1; i < nameLenght; i++)
            {
                if (i % 2 == 0)
                {
                    name += lowerCaseVowels[Random.Range(0, lowerCaseVowels.Length)].ToString();

                }
                else
                {
                    name += lowerCaseConsonants[Random.Range(0, lowerCaseConsonants.Length)].ToString();
                }
            }
        }
        else
        {
            name = upperCaseConsonants[Random.Range(0, upperCaseConsonants.Length)].ToString();
            for (int i = 1; i < nameLenght; i++)
            {
                if (i % 2 == 1)
                {
                    name += lowerCaseVowels[Random.Range(0, lowerCaseVowels.Length)].ToString();

                }
                else
                {
                    name += lowerCaseConsonants[Random.Range(0, lowerCaseConsonants.Length)].ToString();
                }
            }
        }
        return name;
    }
}
