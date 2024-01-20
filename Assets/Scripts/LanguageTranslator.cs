using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageTranslator : MonoBehaviour
{

    void Awake()
    {
        EnglishToConlang("Killer Queen! Stop time and defeat the bad guys!");

    }

    public static string EnglishToConlang(string englishIN)
    {
        string englishLower = englishIN.ToLower();
        string conlangOut = "";
        int englishLength = englishLower.Length;
        char currentChar = '0';
        char nextChar = '0';

        for (int i = 0; i < englishLength - 1; i++) {
            currentChar = englishLower[i]; //get current char
            if(!(i+1 > englishLength)) { nextChar = englishLower[i + 1]; }

            if(currentChar == nextChar) //check if double letter combo
            {
                conlangOut += englishLower[i];
                conlangOut += 'h';
                i++;
                continue;
            }

            switch (currentChar)
            {
                case 'h':
                    conlangOut += 'r';
                    break;
                case 'a':
                    conlangOut += "aa";
                    break;
                case 'i' :
                    conlangOut += "ii";
                    break;
                case 'e':
                    conlangOut += "ee";
                    break;
                case 'o':
                    conlangOut += "oog";
                    break;
                case 'u':
                    conlangOut += "yo";
                    break;
                case 'y':
                    conlangOut += 'e';
                    break;
                default:
                    conlangOut += englishIN[i];
                    break;
            }

        }
        Debug.Log(conlangOut); 
        return conlangOut;

    }

}
