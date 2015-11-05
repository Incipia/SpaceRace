using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPropertiesManager : MonoBehaviour 
{
	public const string readyToRaceKey = "rdy";
	public const string playerNumberKey = "num";
}

static class PlayerExtensions
{
	public static bool readyToRace(this PhotonPlayer player)
	{
		bool retVal = false;
		object ready;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.readyToRaceKey, out ready))
		{
			retVal = (bool)ready;
		}
		return retVal;
	}

	public static void setReadyToRace(this PhotonPlayer player, bool ready)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}

		bool readyToRace = PhotonNetwork.player.readyToRace();
		if (ready != readyToRace)
		{
			Hashtable properties = new Hashtable();
			properties.Add(PlayerPropertiesManager.readyToRaceKey, ready);
			PhotonNetwork.player.SetCustomProperties(properties);
		}
	}

	public static int playerNumber(this PhotonPlayer player)
	{
		int retVal = 0;
		object number;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.playerNumberKey, out number))
		{
			retVal = (int)number;
		}
		return retVal;
	}

	public static void setPlayerNumber(this PhotonPlayer player, int number)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}
		
		int playerNumber = PhotonNetwork.player.playerNumber();
		if (number != playerNumber)
		{
			Hashtable properties = new Hashtable();
			properties.Add(PlayerPropertiesManager.playerNumberKey, number);
			PhotonNetwork.player.SetCustomProperties(properties);
		}
	}
}
