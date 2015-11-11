using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkPlayerColorSync : MonoBehaviour {
	
	public const string colorKey = "clr";
	
	public static Dictionary<PhotonPlayer, Vector3> playerColorDictionary;

	void Awake()
	{
		playerColorDictionary = new Dictionary<PhotonPlayer, Vector3>();
	}

	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
//		updateColors();
	}
	
	private void updateColors()
	{
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			PhotonPlayer player = PhotonNetwork.playerList[i];
			Color color = PlayerColorProvider.colorForPlayerNumber(i+1, PlayerColoredComponentType.InnerCircle);
			
			Vector3 colorVec = new Vector3(color.r, color.g, color.b);
			playerColorDictionary[player] = colorVec;
		}
	}
}

/// <summary>Extension used for PunTeams and PhotonPlayer class. Wraps access to the player's custom property.</summary>
static class ColorExtensions
{
	/// <summary>Extension for PhotonPlayer class to wrap up access to the player's custom property.</summary>
	/// <returns>PunTeam.Team.none if no team was found (yet).</returns>
	public static Color GetColor(this PhotonPlayer player)
	{
		object colorValue;
		if (player.customProperties.TryGetValue(NetworkPlayerColorSync.colorKey, out colorValue))
		{
			Vector3 colorVec = (Vector3)colorValue;
			return new Color(colorVec.x, colorVec.y, colorVec.z);
		}
		
		return Color.white;
	}
	
	/// <summary>Switch that player's team to the one you assign.</summary>
	/// <remarks>Internally checks if this player is in that team already or not. Only team switches are actually sent.</remarks>
	/// <param name="player"></param>
	/// <param name="team"></param>
	public static void SetColor(this PhotonPlayer player, Color color)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("SetColor was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}
		
		Color currentColor = PhotonNetwork.player.GetColor();
		if (!currentColor.Equals(color))
		{
			Vector3 colorVec = new Vector3(color.r, color.g, color.b);
			
			Hashtable props = new Hashtable();
			props.Add(player, colorVec);
			PhotonNetwork.player.SetCustomProperties(props);
		}
	}
}
