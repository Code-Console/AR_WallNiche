//=============================================================================================================================
//
// Copyright (c) 2015-2019 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using easyar;

namespace Sample
{
    public class ImageTargetManager : MonoBehaviour
    {
        private Dictionary<string, ImageTargetController> imageTargetDic = new Dictionary<string, ImageTargetController>();
        public FilesManager pathManager;
        public ImageTrackerBehaviour Tracker;
        [HideInInspector]
        public List<GameObject> imageTargets = new List<GameObject>();
        void Start()
        {
            if (!pathManager)
                pathManager = FindObjectOfType<FilesManager>();
            LoadTarget();
        }

        public void LoadTarget()
        {
            int design = PlayerPrefs.GetInt("design", 0);
            var imageTargetName_FileDic = pathManager.GetDirectoryName_FileDic();
            imageTargets.Clear();
            foreach (var obj in imageTargetName_FileDic.Where(obj => !imageTargetDic.ContainsKey(obj.Key)))
            {
                Debug.Log("!!!!!!!!!imageTargetName_FileDic = " + obj.Key);
                GameObject imageTarget = new GameObject(obj.Key);
                imageTargets.Add(imageTarget);
                var behaviour = imageTarget.AddComponent<ImageTargetController>();
                behaviour.TargetName = obj.Key;
                behaviour.TargetPath = obj.Value.Replace(@"\", "/");
                behaviour.Type = PathType.Absolute;
                behaviour.ImageTracker = Tracker;
                imageTargetDic.Add(obj.Key, behaviour);
                var cube = Instantiate(Resources.Load("HelloAR_TargetOnFly/Prefabs/Cube")) as GameObject;
                cube.transform.parent = imageTarget.transform;
                cube.transform.GetChild(design == 0 ? 1 : 0 ).gameObject.SetActive(false);

                //cube.AddComponent<WallCube>();
                ImageOnFly canvas = GameObject.Find("Canvas").GetComponent<ImageOnFly>();
                float mul = 0.5f;
                cube.transform.localPosition = new Vector3(canvas.valx * mul, canvas.valy * mul, cube.transform.localPosition.z);
                Debug.Log(canvas.valx + ", " + canvas.valy + " !!!!!!!!!cube Name~~~  " + cube.name);

            }
        }

        public void ClearAllTarget()
        {
            
            foreach (var obj in imageTargetDic)
                Destroy(obj.Value.gameObject);
            imageTargetDic = new Dictionary<string, ImageTargetController>();
        }
    }
}
