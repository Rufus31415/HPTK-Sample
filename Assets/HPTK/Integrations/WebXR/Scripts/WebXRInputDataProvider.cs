using Rufus31415.WebXR;
using UnityEngine;

namespace HandPhysicsToolkit.Input
{

    public class WebXRInputDataProvider : InputDataProvider
    {
        private Quaternion _webxrToHPTKRotation;


        public override void InitData()
        {
            base.InitData();

            // convert rotation from WebXR to OVR/HPTK
            _webxrToHPTKRotation = side == Helpers.Side.Right ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, -90, -180);
        }

        public override void UpdateData()
        {
            base.UpdateData();

#if UNITY_EDITOR
            var hand = GetTestHand(side);
#else 
            if (!SimpleWebXR.InSession)
            {
                confidence = 0;
                return;
            }

            var hand = side == Helpers.Side.Left ? SimpleWebXR.LeftInput.Hand : SimpleWebXR.RightInput.Hand;
#endif

            if (!hand.Available)
            {
                confidence = 0;
                return;
            }


            SetBone(0, hand.Joints[WebXRHand.WRIST]); //wrist
            SetBone(1, hand.Joints[WebXRHand.WRIST]); //forearm

            wrist = bones[0];
            forearm = bones[1];

            SetBone(2, hand.Joints[WebXRHand.THUMB_METACARPAL]); //thumb0
            SetBone(3, hand.Joints[WebXRHand.THUMB_PHALANX_PROXIMAL]); //thumb1
            SetBone(4, hand.Joints[WebXRHand.THUMB_PHALANX_DISTAL]); //thumb2
            SetBone(5, hand.Joints[WebXRHand.THUMB_PHALANX_TIP]); //thumb3

            SetBone(6, hand.Joints[WebXRHand.INDEX_PHALANX_INTERMEDIATE]); //index1
            SetBone(7, hand.Joints[WebXRHand.INDEX_PHALANX_DISTAL]); //index2
            SetBone(8, hand.Joints[WebXRHand.INDEX_PHALANX_TIP]); //index3

            SetBone(9, hand.Joints[WebXRHand.MIDDLE_PHALANX_INTERMEDIATE]); //middle1
            SetBone(10, hand.Joints[WebXRHand.MIDDLE_PHALANX_DISTAL]); //middle2
            SetBone(11, hand.Joints[WebXRHand.MIDDLE_PHALANX_TIP]); //middle3

            SetBone(12, hand.Joints[WebXRHand.MIDDLE_PHALANX_INTERMEDIATE]); //ring1
            SetBone(13, hand.Joints[WebXRHand.RING_PHALANX_DISTAL]); //ring2
            SetBone(14, hand.Joints[WebXRHand.MIDDLE_PHALANX_TIP]); //ring3

            SetBone(15, hand.Joints[WebXRHand.LITTLE_METACARPAL]); //thumb0
            SetBone(16, hand.Joints[WebXRHand.LITTLE_PHALANX_PROXIMAL]); //thumb1
            SetBone(17, hand.Joints[WebXRHand.LITTLE_PHALANX_DISTAL]); //thumb2
            SetBone(18, hand.Joints[WebXRHand.LITTLE_PHALANX_TIP]); //thumb3

            UpdateFingerPosesFromBones();

            confidence = 1;
        }

        private void SetBone(int i, WebXRJoint joint)
        {
            bones[i].space = Space.World;
            bones[i].position = joint.Position;

            var rotation = joint.Rotation * _webxrToHPTKRotation;

            bones[i].rotation = rotation;
        }

#if UNITY_EDITOR
        WebXRHand _leftHand;
        WebXRHand _rightHand;

        private WebXRHand GetTestHand(Helpers.Side side)
        {
            if (_leftHand == null)
            {
                var root = System.IO.Path.Combine(Application.dataPath, "HPTK", "Integrations", "WebXR", "Editor", "Tests");
                _leftHand = JsonUtility.FromJson<WebXRHand>(System.IO.File.ReadAllText(System.IO.Path.Combine(root, "left1.json")));
                _rightHand = JsonUtility.FromJson<WebXRHand>(System.IO.File.ReadAllText(System.IO.Path.Combine(root, "right1.json")));
            }

            return side == Helpers.Side.Left ? _leftHand : _rightHand;
        }
#endif

    }



}

