using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour 
{
	public delegate void CountdownCompletion();
	public NetworkRoomCountdownTimer timer;
	public CounterUI counterUI;
	public int countdownDuration = 5;

	private bool timerStarted = false;
	public CountdownCompletion completion;

	void Update()
	{
		if (timerStarted)
		{
			int secondsLeft = Mathf.CeilToInt(timer.remainingTimeInCurrentTurn());
			counterUI.updateSpritesWithNumber(secondsLeft);

			if (timer.currentTurn() != 0 && completion != null)
			{
				completion();
			}
		}
	}

	public void beginCountdownWithSeconds(int seconds, CountdownCompletion completion)
	{
		if (timerStarted == false)
		{
			this.completion = completion;
			timer.SecondsPerTurn = seconds;
			
			timer.startRound();
			timerStarted = true;
		}
	}

	public void beginCountdownWithCompletion(CountdownCompletion completion)
	{
		if (timerStarted == false)
		{
			this.completion = completion;
			timer.SecondsPerTurn = countdownDuration;
			
			timer.startRound();
			timerStarted = true;
		}
	}

	public void beginCountdown()
	{
		if (timerStarted == false)
		{
			this.completion = completion;
			timer.SecondsPerTurn = countdownDuration;
			
			timer.startRound();
			timerStarted = true;
		}
	}

	public void showCountdownUI()
	{
		counterUI.transform.root.gameObject.SetActive(true);
	}

	public void hideCountdownUI()
	{
		counterUI.transform.root.gameObject.SetActive(false);
	}
}
