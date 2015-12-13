using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class EscapeManager : MonoBehaviour {

    [Header("Scene References")]
    public Creature playerCreature;
    public Slider EnduranceSlider;
    public Image EnduranceFillImage;
    public Text QuoteText;
    public Text QuoteAuthorText;

    [Header("Attributes")]
    public Color fullColor;
    public Color emptyColor;


    private float _timeLeft;
    private float _startTime;
    private bool _haswon = false;


	// Use this for initialization
	void Start () {
        EnduranceSlider.value = 1;
        EnduranceFillImage.color = fullColor;
        QuoteText.DOFade(0, 0.00f);
        QuoteAuthorText.DOFade(0, 0.00f);


        StartEscape();
      
	}

    public void StartEscape() {
        //Reset time
        _startTime = Mathf.Lerp(GameManager.Instance.player.EnduranceLifetimeLimits[0], GameManager.Instance.player.EnduranceLifetimeLimits[1],(float)GameManager.Instance.player.Endurance / 100f);
        _timeLeft = _startTime;
        //Start Creature
        playerCreature.GenerateFromData(GameManager.Instance.player.currentMonster.data);  

        StartCoroutine(EscapeTimer());
    }

    IEnumerator EscapeTimer() {
        yield return new WaitForSeconds(1f);
        playerCreature.StartTrackingDistance();

        while (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
            EnduranceSlider.value = _timeLeft / _startTime;
            EnduranceFillImage.color = Color.Lerp(emptyColor, fullColor, _timeLeft / _startTime);
            yield return null;
        }
        //Deactivate motors
        EnduranceSlider.value = 0;
        EnduranceFillImage.color = new Color(0, 0, 0, 0);
        playerCreature.DeactivateMotors();
        yield return new WaitForSeconds(4f);

        if (!_haswon) ShowQuote();
        yield return new WaitForSeconds(5f);
    }

    void ShowQuote() {
        //getRandom Quote
        string[] quotes = QuoteManager.GetQuote();

        //Assign strings
        QuoteText.text = quotes[0];
        QuoteAuthorText.text = quotes[1];
        //Fade In
        QuoteText.DOFade(1, 2.5f);
        QuoteAuthorText.DOFade(1, 2.5f);

    }
}
