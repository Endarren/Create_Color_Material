using npScripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class CreateColorMaterialWindow : EditorWindow
{
	
	struct NameIndex
	{
		public string name;
		public int index;
		public NameIndex(string n, int i)
		{
			name = n;
			index = i;
		}
	}


	#region Fields
	static List<string> colors = new List<string>();
	static Dictionary<ColorNameGroup, List<NameIndex>> colorDict = new Dictionary<ColorNameGroup, List<NameIndex>>();
	public string colorFilePath;
	public List<ColorData> colorDatas = new List<ColorData>();
	public Color materialColor = Color.red;
	public string materialName;
	public bool usePremade = false;
	public enum ColorNameGroup { All, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z }
	public int premadeIndex;
	public Shader shader;
	Vector2 scrollPoint;
	ColorNameGroup colorGroup;
	public ColorData.ColorDataType colorType = ColorData.ColorDataType.RGB;
	bool [] foldOuts = new bool [26];
	
	#endregion
	[MenuItem("nonPareil/Color Material Creator")]
	internal static void Init()
	{
		// Get existing open window or if none, make a new one:
		CreateColorMaterialWindow window = (CreateColorMaterialWindow)EditorWindow.GetWindow(typeof(CreateColorMaterialWindow));
		window.Show();
	}
	public bool DrawColor(int index)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(colorDatas [index].name);
		EditorGUILayout.ColorField(colorDatas [index].CreateColor());
		if (GUILayout.Button("Use"))
		{
			colorDatas [index].colorType = colorType;
			materialColor = colorDatas [index].CreateColor();
			EditorGUILayout.EndHorizontal();
			return true;
		}
		EditorGUILayout.EndHorizontal();
		return false;
	}
	
	void LoadColors()
	{
		string path = EditorUtility.OpenFilePanel("Save new spline profile", "Assets", "xml");

		XmlReaderSettings settings = new XmlReaderSettings();
		settings.IgnoreWhitespace = true;
		//Read the file.
		colorDatas.Clear();
		using (XmlReader reader = XmlReader.Create(path, settings))
		{
			while (reader.Read())
			{
				if (reader.IsStartElement())
				{
					if (reader.Name == "Color")
					{
						ColorData cd = new ColorData();
						cd.ReadFromXML(reader);
						cd.colorType = ColorData.ColorDataType.RGB;
						colorDatas.Add(cd);
					}
				}
			}
		}
	}
	private void OnGUI()
	{

		colorType = (ColorData.ColorDataType)EditorGUILayout.EnumPopup("Color Format", colorType);
		materialColor = EditorGUILayout.ColorField("Color", materialColor);
		shader = (Shader)EditorGUILayout.ObjectField("Shader", shader, typeof(Shader), false);
		usePremade = EditorGUILayout.Toggle("Use Premade Color", usePremade);
		if (GUILayout.Button("Load Color File"))
		{
			LoadColors();
		}
		if (GUILayout.Button("Save"))
		{
			string path = EditorUtility.SaveFilePanelInProject("Save material", "NEW mat.mat", "mat", "Please enter a file name to save the material");
			if (!string.IsNullOrEmpty(path))
			{
				Material mat = new Material(shader);
				mat.color = materialColor;
				AssetDatabase.CreateAsset(mat, path);
			}
		}
		if (usePremade)
		{
			scrollPoint = EditorGUILayout.BeginScrollView(scrollPoint);
			char currentC = ' ';
			int currentFoldIndex = -1;
			for (int i = 0; i < colorDatas.Count; i++)
			{
				if (colorDatas [i].name.Length != 0)
				{
					if (colorDatas [i].name.ToLower() [0] != currentC)
					{
						currentC = colorDatas [i].name.ToLower() [0];
						currentFoldIndex++;
						foldOuts [currentFoldIndex] = EditorGUILayout.Foldout(foldOuts [currentFoldIndex], (currentC + "").ToUpper());
					}
					if (foldOuts [currentFoldIndex])
					{
						EditorGUI.indentLevel++;
						if (DrawColor(i))
						{
							materialColor = colorDatas [i].CreateColor();//TODO fix bug
						}
						EditorGUI.indentLevel--;
					}
				}
			}
			EditorGUILayout.EndScrollView();
		}
	}
}
