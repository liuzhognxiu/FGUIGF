using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace MetaArea
{
    public class MainPlayerLogic : EntityLogicBase
    {
        private MainPlayerData _mainPlayerData;

        private ThirdPersonCharacter m_Character;

        private Animator animator;

        private Transform m_Cam;
        private FreeLookCam m_FreeLookCam;


        public Transform CurBuild;
        private Transform BuildItemTarget;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.DefaultLayerId);
            _mainPlayerData = (MainPlayerData)userData;

            Log.Info("33333");

            GameEntry.Event.Subscribe(CreateBuildItemArgs.EventId, CreateBuildGhostItem);
            GameEntry.Event.Subscribe(CreateBuildEntityArgs.EventId, CreateBuildItem);
            if (_mainPlayerData == null)
            {
                Log.Error("myMainPlayerData object data is invalid.");
                return;
            }

            m_Character = GetComponent<ThirdPersonCharacter>();
            animator = GetComponent<Animator>();
            GameEntry.Resource.LoadAsset(AssetUtility.GetPlayerAsset(_mainPlayerData.Name),
                Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                    (assetName, asset, duration, userTempData) =>
                    {
                        var MainPlayer = Object.Instantiate((GameObject)asset, this.transform);
                        MainPlayer.GetComponent<Animator>().runtimeAnimatorController = null;
                        animator.avatar = MainPlayer.GetComponent<Animator>().avatar;
                        animator.Rebind();
                        BuildItemTarget = MainPlayer.transform.Find("BuildItemTarget");
                        if (!(Camera.main is null)) m_Cam = Camera.main.transform;
                    },
                    (assetName, status, errorMessage, userTempData) =>
                    {
                        Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", _mainPlayerData.Name,
                            assetName, errorMessage);
                    }));

            GameEntry.Resource.LoadAsset(AssetUtility.GetPlayerAsset("FreeLookCameraRig"),
                Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                    (assetName, asset, duration, userData1) =>
                    {
                        if (!(Camera.main is null))
                        {
                            Camera.main.gameObject.SetActive(false);
                            var CameraRig = Object.Instantiate((GameObject)asset);
                            CameraRig.transform.position = transform.position;
                            m_FreeLookCam = CameraRig.GetComponent<FreeLookCam>();
                            m_FreeLookCam.SetTarget(transform);
                            m_Cam = m_FreeLookCam.transform;

                            m_FreeLookCam.SwitchLock(true);
                        }
                    },
                    (assetName, status, errorMessage, userData1) =>
                    {
                        Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", "FreeLookCameraRig",
                            assetName, errorMessage);
                    }));
        }

        private void CreateBuildItem(object sender, GameEventArgs e)
        {
            CreateBuildEntityArgs eventArgs = (CreateBuildEntityArgs)e;
            CreateBuild(new BuildItemData(eventArgs.EntityId, drEntity.Id, "cdk21ib4h5i1f3tf3df3")
            {
                Position = CurBuild.position,
            });
        }

        private DREntity drEntity;

        private void CreateBuildGhostItem(object sender, GameEventArgs e)
        {
            CreateBuildItemArgs eventArgs = (CreateBuildItemArgs)e;
            drEntity = eventArgs.DrEntity;
            CreateBuildGhost(new BuildItemData(drEntity.Id + 21, drEntity.Id + 21, "cdk21ib4h5i1f3tf3df3")
            {
                Rotation = BuildItemTarget.rotation,
            });
        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }

        private async void CreateBuild(BuildItemData data)
        {
            var BuildItem = await GameEntry.Entity.ShowEntityAsync(typeof(BuildItem), Constant.AssetPriority.BuildItem,
                "BuildItem", data);
            var BuildItemTransform = BuildItem.transform;
            BuildItemTransform.position = CurBuild.position;
            BuildItemTransform.rotation = CurBuild.rotation;
            BuildItemTransform.localScale = CurBuild.localScale;
            GameEntry.Entity.HideEntity(drEntity.Id + 21);
            CurBuild = null;
        }


        private async void CreateBuildGhost(BuildItemData data)
        {
            var buildItem = await GameEntry.Entity.ShowEntityAsync(typeof(BuildItem), Constant.AssetPriority.BuildItem,
                "BuildItem", data);
            var buildItemTransform = buildItem.transform;
            CurBuild = buildItemTransform;
            var position = BuildItemTarget.transform.position;
            CurBuild.position = new Vector3(position.x, buildItemTransform.position.y, position.z);
            GameEntry.Event.Fire(this, CreateBuildSuccessArgs.Create(buildItem));
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            //control Cursor
            void ControlFreeLook()
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    m_FreeLookCam.SwitchLock(false);
                }
                else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    m_FreeLookCam.SwitchLock(true);
                }
            }

            //control build
            void ControlCurBuild()
            {
                if (CurBuild != null)
                {
                    var position = BuildItemTarget.transform.position;
                    CurBuild.position = new Vector3(position.x, CurBuild.position.y, position.z);
                }
            }

            //control player move
            void ControlMove()
            {
                // read inputs
                var jump = CrossPlatformInputManager.GetButtonDown("Jump");
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Vertical");
                bool crouch = Input.GetKey(KeyCode.C);

                Vector3 move = default(Vector3);
                if (m_Cam != null)
                {
                    var forward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    move = v * forward + h * m_Cam.right;
                }
                else
                {
                    move = v * Vector3.forward + h * Vector3.right;
                }
#if !MOBILE_INPUT
                // walk speed multiplier
                if (Input.GetKey(KeyCode.LeftShift)) move *= 0.5f;
#endif
                m_Character.Move(move, crouch, jump);
            }


            base.OnUpdate(elapseSeconds, realElapseSeconds);

            ControlFreeLook();
            ControlCurBuild();
            ControlMove();
        }
    }
}