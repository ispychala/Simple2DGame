using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] playerChoices;
    public AnimatorOverrideController[] playerAnimations;
    public Image characterImage;

    private int numCharacters;
    int current = 0;

    private void Start()
    {
        numCharacters = playerChoices.Length;    
    }

    public void PickNext()
    {
        current += 1;
        if (current >= numCharacters) current = 0;
        characterImage.sprite = playerChoices[current];
    }

    public void GoBack()
    {
        current -= 1;
        if (current < 0) current = numCharacters-1;
        characterImage.sprite = playerChoices[current];
    }

    public void PickCharacter()
    {

    }
}
