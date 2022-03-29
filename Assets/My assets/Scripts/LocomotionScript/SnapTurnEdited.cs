// Copyright (c) Valve Corporation, All rights reserved. ======================================================================================================



using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-----------------------------------------------------------------------------
    public class SnapTurnEdited : MonoBehaviour
    {
        public float velocity = 1;

        public SteamVR_Action_Vector2 TouchpadRight;

        public Vector3 additionalOffset = new Vector3(0, -0.3f, 0);

        private void Update()
        {        
            if(TouchpadRight.axis.x!=0)
            {
                RotatePlayer(TouchpadRight.axis.x*velocity);
            }
        }


        private Coroutine rotateCoroutine;
        public void RotatePlayer(float angle)
        {
            DoRotatePlayer(angle);
        }

        //-----------------------------------------------------
        private void DoRotatePlayer(float angle)
        {
            Player player = Player.instance;
            Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position -= playerFeetOffset;
            player.transform.Rotate(Vector3.up, angle);
            playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
            player.trackingOriginTransform.position += playerFeetOffset;            
        }
    }
}