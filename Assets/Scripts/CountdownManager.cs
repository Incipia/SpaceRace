using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour 
{
	public delegate void CountdownCompletion();
	public InRoomRoundTimer timer;
	public CounterUI counterUI;

	private bool timerStarted = false;
	private CountdownCompletion completion;

	void Update()
	{
		if (timerStarted)
		{
			int secondsLeft = Mathf.CeilToInt((float)timer.remainingTime);
			counterUI.updateSpritesWithNumber(secondsLeft);

			if (timer.turn != 0 && completion != null)
			{
				counterUI.transform.root.gameObject.SetActive(false);
				completion();
			}
		}
	}

	public void beginCountdownWithSeconds(int seconds, CountdownCompletion completion)
	{
		this.completion = completion;
		timer.SecondsPerTurn = seconds;

		timer.turn = 0;
		timer.StartRoundNow();
		timerStarted = true;

		counterUI.updateSpritesWithNumber(seconds);
	}
}
