using UnityEngine;
using AssemblyCSharp;
using System.Collections;

public class NetworkRoomConnectionInfo : MonoBehaviour 
{
	public CounterUI youArePlayerCounter;
	public CounterUI openSpotsLeftCounter;

	public void updatePlayerNumber(int number)
	{
		Color numberColor = PlayerColorProvider.colorForPlayerNumber(number, PlayerColoredComponentType.InnerCircle);
		youArePlayerCounter.updateColor(numberColor);
		youArePlayerCounter.updateSpritesWithNumber(number);
	}

	public void updateOpenSpotsLeft(int number)
	{
		openSpotsLeftCounter.updateSpritesWithNumber(number);
	}
}
