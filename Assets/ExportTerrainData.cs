using UnityEngine;
using System.Collections;

public class ExportTerrainData : MonoBehaviour 
{
	public bool export = false;

	void OnDrawGizmos () 
	{
		if (!export) return;
		export = false;

		#if UNITY_EDITOR
		string path= UnityEditor.EditorUtility.SaveFilePanel(
			"Save Terrain as Unity Asset",
			"Assets",
			"ExportedTerrain.asset", 
			"asset");
		if (path!=null && path.Length!=0)
		{
			path = path.Replace(Application.dataPath, "Assets");

			Terrain terrain = GetComponent<Terrain>();
			float[,,] splats = terrain.terrainData.GetAlphamaps(0,0,terrain.terrainData.alphamapResolution, terrain.terrainData.alphamapResolution);

			UnityEditor.AssetDatabase.CreateAsset(terrain.terrainData, path);
			terrain.terrainData.SetAlphamaps(0,0,splats);
			UnityEditor.AssetDatabase.SaveAssets();
			
		}
		#endif
	}
}
