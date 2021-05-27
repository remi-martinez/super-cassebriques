using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupDisplay : MonoBehaviour
{
    // Variables d'image et txt sur le canvas
    public GameObject smallPaddle;
    private static Image smallPaddleImage;
    private TextMeshProUGUI smallPaddleText;
    public static int numberOfSmallPaddle = 0;

    public GameObject largePaddle;
    private static Image largePaddleImage;
    private TextMeshProUGUI largePaddleText;
    public static int numberOfLargePaddle = 0;

    public GameObject invertedPaddle;
    private static Image invertedPaddleImage;
    public static bool isInverted = false;

    public GameObject fastPaddle;
    private static Image fastPaddleImage;
    public static bool isFastPaddle = false;

    public Paddle paddle;
    public Ball ball;
    
    void Start()
    {
        smallPaddleText = smallPaddle.GetComponentInChildren<TextMeshProUGUI>();
        smallPaddleImage = smallPaddle.GetComponentInChildren<Image>();
        smallPaddleImage.fillAmount = 0;

        largePaddleText = largePaddle.GetComponentInChildren<TextMeshProUGUI>();
        largePaddleImage = largePaddle.GetComponentInChildren<Image>();
        largePaddleImage.fillAmount = 0;

        invertedPaddleImage = invertedPaddle.GetComponentInChildren<Image>();
        invertedPaddleImage.fillAmount = 0;

        fastPaddleImage = fastPaddle.GetComponentInChildren<Image>();
        fastPaddleImage.fillAmount = 0;        
    }
    
    void Update()
    {
        if (numberOfSmallPaddle > 0) // Si on a des malus "petite raquette"
        {
            smallPaddleImage.fillAmount -= 0.1f * Time.deltaTime; // 4s / 50 = 0.08
            if (smallPaddleImage.fillAmount <= 0){
                numberOfSmallPaddle--;
                paddle.UpdatePaddleSize(1); // Fin du malus : on agrandit la raquette
            }
        }

        if (numberOfLargePaddle > 0) // Si on a le bonus "grande raquette"
        {
            largePaddleImage.fillAmount -= 0.1f * Time.deltaTime; // 4s / 50 = 0.08
            if (largePaddleImage.fillAmount <= 0)
            {
                numberOfLargePaddle--;
                paddle.UpdatePaddleSize(-1); // Fin du bonus : on rétrécit la raquette

            }
        }

        if (isInverted) // Si on a la malus "contrôles inversés" (bool)
        {
            invertedPaddleImage.fillAmount -= 0.1f * Time.deltaTime;
            if(invertedPaddleImage.fillAmount <= 0)
            {
                isInverted = false; // Fin du malus : on fini d'inverser les contrôles
                paddle.UpdatePaddleInverted(false);
            }
        }

        if (isFastPaddle) // Si on a le bonus "raquette rapide" (bool)
        {
            fastPaddleImage.fillAmount -= 0.1f * Time.deltaTime;
            if(fastPaddleImage.fillAmount <= 0)
            {
                isFastPaddle = false; // Fin du bonus : on ralentit la raquette
                paddle.UpdateFastPaddle(false);
            }
        }

        smallPaddleText.text = "x" + numberOfSmallPaddle;
        largePaddleText.text = "x" + numberOfLargePaddle;

    }

    public static void NewSmallPaddle()
    {
        smallPaddleImage.fillAmount = 1;
        numberOfSmallPaddle++;
    }

    public static void NewLargePaddle()
    {
        largePaddleImage.fillAmount = 1;
        numberOfLargePaddle++;
    }

    public static void NewInvertedPaddle()
    {
        invertedPaddleImage.fillAmount = 1;
        isInverted = true;
    }

    public static void NewFastPaddle()
    {
        fastPaddleImage.fillAmount = 1;
        isFastPaddle = true;
    }
}
