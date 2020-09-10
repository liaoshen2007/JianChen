using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using game.main;
using UnityEditor;
using UnityEngine;

public static class I18NManager
{
	public enum LanguageType
	{	 
		None,
		ChineseSimplified,
		ChineseTraditional,
		English,    
	}
	
	private static Dictionary<string, string> _languageDict;

//#if UNITY_EDITOR
//	[InitializeOnLoadMethod]
//	public static void LoadLanguageConfig()
//	{
//		LanguageType type = LanguageType.ChineseSimplified;
//		LoadLanguageConfig(type);
//		
////		EditorUtility.ClearProgressBar();
//	}
//#endif
	
	public static void LoadLanguageConfig(LanguageType type)
	{
		_languageDict = new Dictionary<string, string>();

		char[] separator = new char[] { '=' };
		
		string str = new AssetLoader().LoadTextSync(AssetLoader.GetLanguageDataPath(type));
		var strings = str.Split(new char[] { '\n'}, StringSplitOptions.RemoveEmptyEntries);
		foreach (var line in strings)
		{
			string trim = line.Trim();
			if (string.IsNullOrEmpty(trim) || line.StartsWith("//"))
				continue;

			string[] arr = trim.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
			_languageDict.Add(arr[0].Trim(), Regex.Unescape(arr[1].Trim()));
		}
	}

	public static string Get(string key)
	{
		string value;
		if (_languageDict.TryGetValue(key, out value))
		{
			return value;
		}
		return null;
	}
	
	public static string Get(string key, params object[] strings)
	{
		string value;
		if (_languageDict.TryGetValue(key, out value))
		{
			return string.Format(value, strings);
		}
		
		return null;
	}
}
