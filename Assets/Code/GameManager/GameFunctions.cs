using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public static class GameFunctions {
	
	#region Public Attributes
	#endregion
	
	#region Private Attributes
	#endregion
	
	#region MonoDevelop Methods
	#endregion
	
	#region User Methods
	//
	public static string[] GetTextXML(string category, string subCategory, string name)
	{
		string[] textToReturn;
		XmlDocument xml_d;
		XmlNode objectToUse;
		XmlNodeList xmlDescription;
		GameManager gmScript = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		string language = gmScript.GetLanguage ();
		TextAsset textasset = (TextAsset)Resources.Load(language, typeof(TextAsset));
		xml_d = new XmlDocument();
		xml_d.LoadXml(textasset.text);
		//Search if it is the correct ID or name
		string route = "MAIN/" + category + "/" + subCategory + "[@name='" + name + "']";
		objectToUse = xml_d.SelectSingleNode(route);
		if (objectToUse != null) 
		{
			xmlDescription = ((XmlElement)objectToUse).GetElementsByTagName ("texto");
			textToReturn = new string[xmlDescription.Count];
			int j = 0;
			foreach (XmlNode node in xmlDescription) 
			{
				textToReturn [j] = node.InnerText;
				j++;
			}
			return textToReturn;
		} 
		return null;
	}
	#endregion
}
