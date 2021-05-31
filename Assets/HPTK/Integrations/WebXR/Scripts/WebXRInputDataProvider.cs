using Rufus31415.WebXR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPhysicsToolkit.Input
{

    public class WebXRInputDataProvider : InputDataProvider
    {

        /*
 * 0 - wrist
 * 1 - forearm
 * 
 * 2 - thumb0
 * 3 - thumb1
 * 4 - thumb2
 * 5 - thumb3
 * 
 * 6 - index1
 * 7 - index2
 * 8 - index3
 * 
 * 9 - middle1
 * 10 - middle2
 * 11 - middle3
 * 
 * 12 - ring1
 * 13 - ring2
 * 14 - ring3
 * 
 * 15 - pinky0
 * 16 - pinky1
 * 17 - pinky2
 * 18 - pinky3
 */




        public override void InitData()
        {
            base.InitData();
        }

        public override void UpdateData()
        {
            base.UpdateData();

            if (!SimpleWebXR.InSession)
            {
                confidence = 0;
                return;
            }

            var hand = side == Helpers.Side.Left ? SimpleWebXR.LeftInput.Hand : SimpleWebXR.RightInput.Hand;

            if (!hand.Available) {
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
        bones[i].rotation = joint.Rotation;
    }
    }



}

   