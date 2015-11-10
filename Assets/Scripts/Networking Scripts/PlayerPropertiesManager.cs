using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPropertiesManager : MonoBehaviour 
{
	public const string readyToRaceKey = "rdy";
	public const string playerNumberKey = "num";
	public const string needsToAttachCameraKey = "cam";
	public const string movementEnabledKey = "mov";
	public const string needsToResetPositionKey = "rpos";
	public const string crossedFinishLineKey = "fin";
	public const string totalPointsKey = "pts";
}

static class PlayerExtensions
{
	public static bool needsToResetPosition(this PhotonPlayer player)
	{
		bool retVal = false;
		object resetPos;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.needsToResetPositionKey, out resetPos))
		{
			retVal = (bool)resetPos;
		}
		return retVal;
	}
	
	public static void setNeedsToResetPosition(this PhotonPlayer player, bool resetPos)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}

		Hashtable properties = new Hashtable();
		properties.Add(PlayerPropertiesManager.needsToResetPositionKey, resetPos);
		PhotonNetwork.player.SetCustomProperties(properties);
	}

	public static bool movementEnabled(this PhotonPlayer player)
	{
		bool retVal = false;
		object ready;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.movementEnabledKey, out ready))
		{
			retVal = (bool)ready;
		}
		return retVal;
	}
	
	public static void setMovementEnabled(this PhotonPlayer player, bool enabled)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}

		Hashtable properties = new Hashtable();
		properties.Add(PlayerPropertiesManager.movementEnabledKey, enabled);
		PhotonNetwork.player.SetCustomProperties(properties);
	}

	public static bool needsToAttachCamera(this PhotonPlayer player)
	{
		bool retVal = false;
		object ready;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.needsToAttachCameraKey, out ready))
		{
			retVal = (bool)ready;
		}
		return retVal;
	}
	
	public static void setNeedsToAttachCamera(this PhotonPlayer player, bool attachCamera)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}
		
		bool needsToAttachCamera = PhotonNetwork.player.needsToAttachCamera();
		Hashtable properties = new Hashtable();
		properties.Add(PlayerPropertiesManager.needsToAttachCameraKey, attachCamera);
		PhotonNetwork.player.SetCustomProperties(properties);
	}

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
	
	public static bool crossedFinishLine(this PhotonPlayer player)
	{
		bool retVal = false;
		object ready;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.crossedFinishLineKey, out ready))
		{
			retVal = (bool)ready;
		}
		return retVal;
	}
	
	public static void setCrossedFinishLine(this PhotonPlayer player, bool crossed)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}
		
		bool crossedFinishLine = PhotonNetwork.player.crossedFinishLine();
		if (crossed != crossedFinishLine)
		{
			Hashtable properties = new Hashtable();
			properties.Add(PlayerPropertiesManager.crossedFinishLineKey, crossed);
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
	
	public static int totalPoints(this PhotonPlayer player)
	{
		int retVal = 0;
		object number;
		if (player.customProperties.TryGetValue(PlayerPropertiesManager.totalPointsKey, out number))
		{
			retVal = (int)number;
		}
		return retVal;
	}
	
	public static void setTotalPoints(this PhotonPlayer player, int number)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("setReadyToRace was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}
		
		int points = PhotonNetwork.player.totalPoints();
		if (number != points)
		{
			Hashtable properties = new Hashtable();
			properties.Add(PlayerPropertiesManager.totalPointsKey, number);
			PhotonNetwork.player.SetCustomProperties(properties);
		}
	}
	
//	public static bool allPlayersAreReadyToRace(this Room room)
//	{
//		bool allPlayersAreReady = true;
//		foreach(PhotonPlayer player in PhotonNetwork.playerList)
//		{
//			if (player.readyToRace() == false)
//			{
//				allPlayersAreReady = false;
//				break;
//			}
//		}
//		return allPlayersAreReady;
//	}
}
