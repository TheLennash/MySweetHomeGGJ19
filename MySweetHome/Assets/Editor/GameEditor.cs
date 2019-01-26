using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BreinWave.Editor
{
    public class GameEditor : EditorWindow
    {

        [MenuItem("BreinWave/GameEditor &l")]
        static void Init()
        {
            var window = EditorWindow.GetWindow(typeof(GameEditor));
            window.Show();
        }

        //ReplaceSelection
        GameObject ReplacementObj;
        enum OffsetSettings
        {
            None,
            World,
            Local
        }

        OffsetSettings eOffsetRotationX;
        OffsetSettings eOffsetRotationY;
        OffsetSettings eOffsetRotationZ;
        float offsetRotationX = 0;
        float offsetRotationY = 0;
        float offsetRotationZ = 0;

        //RemoveDuplicates
        float radius = 0;

        //Select All object by name
        string objectName;
        string materialObjectName;
        string rename;

        string meshObjectName;
        int meshVerts;
        int meshTris;
        string groupGameObjectName;
        bool groupSelected;

        string screenshotName;

        Mesh meshNewMesh;
        bool meshSetSelected;


        public float MaxSize = 2;
        public float MinSize = 1;


        void OnGUI()
        {
            ResetPlayerPrefs();
            GUILayout.Space(5);
            DrawSelectionUI();
            GUILayout.Space(5);
            RemoveDuplicates();
            GUILayout.Space(5);
            SelectAllObjectByName();
            GUILayout.Space(5);
            SelectAllObjectsByMaterialName();
            GUILayout.Space(5);
            SelectAllObjectsByMeshProperties();
            GUILayout.Space(5);
            RevertMesh();
            GUILayout.Space(5);
            NameIteratively();
            GUILayout.Space(5);
            TakeScreenShot();
            GUILayout.Space(5);
            RandomizeSize();
            Repaint();
        }

        public void RandomizeSize()
        {

            MaxSize = EditorGUILayout.FloatField("MaxSize", MaxSize);
            MinSize = EditorGUILayout.FloatField("MinSize", MinSize);

            if (GUILayout.Button("Resize selection"))
            {

                Transform[] selected = GetAllSelected();
                foreach (Transform transform in selected)
                {
                    if (transform == null)
                        continue;

                    var size = UnityEngine.Random.Range(MinSize, MaxSize);
                    transform.localScale = new Vector3(size, size, size);

                }

            }

        }


        void ResetPlayerPrefs()
        {
            if (GUILayout.Button("Reset Player Prefs"))
                PlayerPrefs.DeleteAll();
        }

        public void TakeScreenShot()
        {
            GUILayout.BeginVertical("Box");

            screenshotName = EditorGUILayout.TextField("filename:", screenshotName);


            if (GUILayout.Button("Take Screenshot"))
            {
                var filename = screenshotName;
                if (string.IsNullOrEmpty(screenshotName))
                    filename = "ScreenCapture" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

                Debug.Log("taking screenshot");
                ScreenCapture.CaptureScreenshot("Assets/Screenshots/" + filename + "ScreenCapture" + ".png", 4);
                Debug.Log("done taking screenshot");

            }

            GUILayout.EndVertical();
        }

        void NameIteratively()
        {
            GUILayout.BeginVertical("Box");
            rename = EditorGUILayout.TextField("New name", rename);

            if (GUILayout.Button("Rename selection iteratively"))
            {

                Transform[] selected = GetAllSelected();
                int i = 0;
                foreach (Transform transform in selected)
                {
                    if (transform == null)
                        continue;

                    i++;
                    transform.gameObject.name = rename + " " + i;
                }
            }

            GUILayout.EndVertical();

        }

        void RemoveDuplicates()
        {
            GUILayout.BeginVertical("Box");

            radius = EditorGUILayout.FloatField("Radius", radius);
            if (GUILayout.Button("Remove Selected Duplicates"))
            {
                Transform[] selected = GetAllSelected();
                foreach (Transform transform in selected)
                {
                    if (transform == null)
                        continue;

                    foreach (Transform trans in selected)
                    {
                        if (trans == null || transform == trans)
                            continue;

                        if (Vector3.SqrMagnitude(trans.position - transform.position) < (radius * radius))
                            DestroyImmediate(trans.gameObject);
                    }
                }
            }

            GUILayout.EndVertical();
        }

        void DrawSelectionUI()
        {
            GUILayout.BeginVertical("Box");
            ReplacementObj = (GameObject)EditorGUILayout.ObjectField("Replacement", ReplacementObj, typeof(GameObject), false);

            GUILayout.BeginHorizontal();
            eOffsetRotationX = (OffsetSettings)EditorGUILayout.EnumPopup("Offset Rotation X", eOffsetRotationX);
            offsetRotationX = EditorGUILayout.FloatField(offsetRotationX);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            eOffsetRotationY = (OffsetSettings)EditorGUILayout.EnumPopup("Offset Rotation Y", eOffsetRotationY);
            offsetRotationY = EditorGUILayout.FloatField(offsetRotationY);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            eOffsetRotationZ = (OffsetSettings)EditorGUILayout.EnumPopup("Offset Rotation Z", eOffsetRotationZ);
            offsetRotationZ = EditorGUILayout.FloatField(offsetRotationZ);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Replace Selected"))
            {
                Transform[] selected = GetAllSelected();

                if (ReplacementObj == null)
                    EditorUtility.DisplayDialog("No Replace object", "There is no Replace object", "Ok");
                else if (selected.Length == 0)
                    EditorUtility.DisplayDialog("No objects are selected", "There are no selected", "Ok");
                else
                    ReplaceSelection(GetAllSelected());
            }

            GUILayout.EndVertical();
        }

        void SelectAllObjectByName()
        {
            GUILayout.BeginVertical("Box");
            objectName = EditorGUILayout.TextField("Name", objectName);

            if (GUILayout.Button("Select All Object By Name"))
            {
                GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
                List<GameObject> objs = new List<GameObject>();

                foreach (GameObject obj in gameObjects)
                {
                    if (obj.name == objectName)
                        objs.Add(obj);
                }

                Selection.objects = objs.ToArray();
            }
            GUILayout.EndVertical();
        }

        void SelectAllObjectsByMaterialName()
        {

            GUILayout.BeginVertical("Box");
            materialObjectName = EditorGUILayout.TextField("material Name", materialObjectName);

            if (GUILayout.Button("Select All Object By Name"))
            {
                GameObject[] gameObjects = FindObjectsOfType<GameObject>();
                List<GameObject> objs = new List<GameObject>();
                //Debug.Log("objs count : " + gameObjects.Length);
                foreach (GameObject obj in gameObjects)
                {
                    MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        //Debug.Log("renderer has materials :  " + renderer.sharedMaterials.Length);

                        foreach (Material mat in renderer.sharedMaterials)
                        {

                            //Debug.Log(mat.name);
                            if (mat != null && mat.name == materialObjectName)
                            {
                                //Debug.Log("object added :  " + obj.name);

                                objs.Add(obj);
                                break;
                            }
                        }
                    }
                }

                Selection.objects = objs.ToArray();
            }
            GUILayout.EndVertical();
        }

        void SelectAllObjectsByMeshProperties()
        {

            GUILayout.BeginVertical("Box");
            meshObjectName = EditorGUILayout.TextField("Name (contains)", meshObjectName);
            meshVerts = EditorGUILayout.IntField("Verts", meshVerts);
            meshTris = EditorGUILayout.IntField("Tris", meshTris);
            groupSelected = EditorGUILayout.Toggle("Group result", groupSelected);
            groupGameObjectName = EditorGUILayout.TextField("Group to name", groupGameObjectName);
            meshSetSelected = EditorGUILayout.Toggle("Set mesh selected items", meshSetSelected);
            meshNewMesh = (Mesh)EditorGUILayout.ObjectField("Replacement", meshNewMesh, typeof(Mesh), false);

            if (GUILayout.Button("Select All Object By Mesh Properties"))
            {
                GameObject[] gameObjects = FindObjectsOfType<GameObject>();
                List<GameObject> objs = new List<GameObject>();
                Debug.Log("objs count : " + gameObjects.Length);
                Debug.Log("meshObjectName: " + meshObjectName);
                Debug.Log("meshVerts : " + meshVerts);
                Debug.Log("meshTris : " + meshTris);
                Debug.Log("Group result : " + groupSelected);
                Debug.Log("Group to (name) : " + groupGameObjectName);

                foreach (GameObject obj in gameObjects)
                {
                    MeshFilter filter = obj.GetComponent<MeshFilter>();
                    if (filter != null)
                    {
                        Debug.Log("renderer mesh:  " + obj.name);

                        if (obj.name.ToLower().Contains(meshObjectName.ToLower()))
                        {
                            if ((meshVerts == 0 || filter.sharedMesh.vertices.Length == meshVerts)
                                && (meshTris == 0 || (filter.sharedMesh.triangles.Length / 3) == meshTris))
                            {
                                objs.Add(obj);
                            }
                            // break;
                        }
                    }
                }

                Selection.objects = objs.ToArray();

                if (Selection.objects.Length > 0)
                {

                    if (groupSelected)
                    {
                        Debug.Log("Logging selected items(" + Selection.objects.Length + "):" + groupGameObjectName);
                        if (string.IsNullOrEmpty(groupGameObjectName))
                        {
                            groupGameObjectName = meshObjectName + "_" + meshVerts + "_" + meshTris;
                        }
                        GameObject newgo = new GameObject(groupGameObjectName);
                        newgo.transform.SetParent(((GameObject)Selection.objects[0]).transform.parent);
                        for (int i = 0; i < Selection.objects.Length; i++)
                        {
                            ((GameObject)Selection.objects[i]).transform.SetParent(newgo.transform);
                        }
                    }

                    if (meshSetSelected)
                    {
                        if (meshNewMesh == null)
                        {
                            meshNewMesh = ((GameObject)Selection.objects[0]).GetComponent<MeshFilter>().sharedMesh;
                        }

                        for (int i = 0; i < Selection.objects.Length; i++)
                        {
                            //if (i> 0 && ((GameObject)Selection.objects[0]).GetComponent<MeshFilter>().sharedMesh == ((GameObject)Selection.objects[i]).GetComponent<MeshFilter>().sharedMesh.f)
                            //{
                            ((GameObject)Selection.objects[i]).GetComponent<MeshFilter>().sharedMesh = meshNewMesh;
                            //}
                            //else
                            //{
                            //    Debug.Log("Different uvs");
                            //}

                        }
                    }
                }

            }
            GUILayout.EndVertical();
        }

        void RevertMesh()
        {

            GUILayout.BeginVertical("Box");
            //meshObjectName = EditorGUILayout.TextField("Name (contains)", meshObjectName);
            //meshVerts = EditorGUILayout.IntField("Verts", meshVerts);
            //meshTris = EditorGUILayout.IntField("Tris", meshTris);
            //groupSelected = EditorGUILayout.Toggle("Group result", groupSelected);
            //groupGameObjectName = EditorGUILayout.TextField("Group to name", groupGameObjectName);
            //meshSetSelected = EditorGUILayout.Toggle("Set mesh selected items", meshSetSelected);
            //meshNewMesh = (Mesh)EditorGUILayout.ObjectField("Replacement", meshNewMesh, typeof(Mesh), false);

            if (GUILayout.Button("Revert Mesh Selected items"))
            {
                //Debug.Log("Start reverting");
                //foreach (GameObject obj in Selection.objects)
                //{
                //    MeshFilter filter = obj.GetComponent<MeshFilter>();
                //    string[] results;
                //    Debug.Log("Start reverting:" + obj.name);
                //    results = AssetDatabase.FindAssets(obj.name);
                //    foreach (string guid in results)
                //    {
                //        Debug.Log("testI: " + AssetDatabase.GUIDToAssetPath(guid));
                //        //Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid));
                //        Debug.Log("Aantal objecten" + objects.Length);
                //        for (int i = 0; i < objects.Length; i++)
                //        {
                //            if (objects[i] is Mesh)
                //            {
                //                // Debug.Log(objects[i].name);
                //                if (objects[i].name == obj.name)
                //                {
                //                    obj.GetComponent<MeshFilter>().sharedMesh = (Mesh)objects[i];
                //                }

                //            }
                //        }

                //    }

                //}
            }
            GUILayout.EndVertical();
        }


        void ReplaceSelection(Transform[] selected)
        {
            Undo.RecordObjects(selected, "ReplaceSelection");

            for (int i = 0; i < selected.Length; i++)
            {
                if (selected[i] != null)
                {
                    Transform replacement = Instantiate(ReplacementObj.transform, selected[i].position, selected[i].rotation) as Transform;
                    replacement.parent = selected[i].parent;
                    //replacement.name = ReplacementObj.name;

                    Vector3 eulerAngles = replacement.eulerAngles;

                    if (eOffsetRotationX == OffsetSettings.Local)
                        eulerAngles.x += offsetRotationX;
                    else if (eOffsetRotationX == OffsetSettings.World)
                        eulerAngles.x = offsetRotationX;

                    if (eOffsetRotationY == OffsetSettings.Local)
                        eulerAngles.y += offsetRotationY;
                    else if (eOffsetRotationY == OffsetSettings.World)
                        eulerAngles.y = offsetRotationY;

                    if (eOffsetRotationZ == OffsetSettings.Local)
                        eulerAngles.z += offsetRotationZ;
                    else if (eOffsetRotationZ == OffsetSettings.World)
                        eulerAngles.z = offsetRotationZ;

                    replacement.eulerAngles = eulerAngles;
                }
            }
            for (int i = 0; i < selected.Length; i++)
            {
                DestroyImmediate(selected[i].gameObject);
            }
        }

        Transform ValidateSelection()
        {
            return Selection.activeTransform;
        }

        Transform[] GetAllSelected()
        {
            GameObject[] selected = Selection.gameObjects;
            Transform[] selectedTransforms = new Transform[selected.Length];
            for (int i = 0; i < selected.Length; i++)
            {
                if (selected[i])
                {
                    selectedTransforms[i] = selected[i].transform;
                }
            }
            return selectedTransforms;
        }
    }
}