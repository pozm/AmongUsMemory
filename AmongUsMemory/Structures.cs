﻿using System;
using System.Runtime.InteropServices;

namespace HamsterCheese.AmongUsMemory
{
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public struct PlayerControll
    {
        [System.Runtime.InteropServices.FieldOffset(8)]         public uint m_CachedPtr;
        [System.Runtime.InteropServices.FieldOffset(12)]        public uint SpawnId;
        [System.Runtime.InteropServices.FieldOffset(16)]        public uint NetId;
        [System.Runtime.InteropServices.FieldOffset(20)]        public uint DirtyBits;
        [System.Runtime.InteropServices.FieldOffset(24)]        public uint SpawnFlags;
        [System.Runtime.InteropServices.FieldOffset(25)]        public uint sendMode;
        [System.Runtime.InteropServices.FieldOffset(28)]        public uint OwnerId;
        [System.Runtime.InteropServices.FieldOffset(32)]        public byte DespawnOnDestroy;
        [System.Runtime.InteropServices.FieldOffset(36)]        public uint LastStartCounter;
        [System.Runtime.InteropServices.FieldOffset(40)]        public byte PlayerId;
        [System.Runtime.InteropServices.FieldOffset(44)]        public float MaxReportDistance;
        [System.Runtime.InteropServices.FieldOffset(48)]        public byte moveable;
        [System.Runtime.InteropServices.FieldOffset(49)]        public byte inVent;
        [System.Runtime.InteropServices.FieldOffset(52)]        public IntPtr _cachedData;
        [System.Runtime.InteropServices.FieldOffset(56)]        public IntPtr FootSteps;
        [System.Runtime.InteropServices.FieldOffset(60)]        public IntPtr KillSfx;
        [System.Runtime.InteropServices.FieldOffset(64)]        public IntPtr KillAnimations;
        [System.Runtime.InteropServices.FieldOffset(68)]        public float killTimer;
        [System.Runtime.InteropServices.FieldOffset(72)]        public uint RemainingEmergencies;
        [System.Runtime.InteropServices.FieldOffset(76)]        public IntPtr nameText;
        [System.Runtime.InteropServices.FieldOffset(80)]        public IntPtr LightPrefab;
        [System.Runtime.InteropServices.FieldOffset(84)]        public IntPtr myLight;
        [System.Runtime.InteropServices.FieldOffset(88)]        public IntPtr Collider;
        [System.Runtime.InteropServices.FieldOffset(92)]        public IntPtr MyPhysics;
        [System.Runtime.InteropServices.FieldOffset(96)]        public IntPtr NetTransform;
        [System.Runtime.InteropServices.FieldOffset(100)]       public IntPtr CurrentPet;
        [System.Runtime.InteropServices.FieldOffset(104)]       public IntPtr HatRenderer;
        [System.Runtime.InteropServices.FieldOffset(108)]       public IntPtr myRend;
        [System.Runtime.InteropServices.FieldOffset(112)]       public IntPtr hitBuffer;
        [System.Runtime.InteropServices.FieldOffset(116)]       public IntPtr myTasks;
        [System.Runtime.InteropServices.FieldOffset(120)]       public IntPtr ScannerAnims;
        [System.Runtime.InteropServices.FieldOffset(124)]       public IntPtr ScannersImages;
        [System.Runtime.InteropServices.FieldOffset(128)]       public IntPtr closest;
        [System.Runtime.InteropServices.FieldOffset(132)]       public byte isNew;
        [System.Runtime.InteropServices.FieldOffset(136)]       public IntPtr cache;
        [System.Runtime.InteropServices.FieldOffset(140)]       public IntPtr itemsInRange;
        [System.Runtime.InteropServices.FieldOffset(144)]       public IntPtr newItemsInRange;
        [System.Runtime.InteropServices.FieldOffset(148)]       public byte scannerCount;
        [System.Runtime.InteropServices.FieldOffset(149)]       public byte infectedSet;
    }

    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public struct PlayerInfo
    {
        [System.Runtime.InteropServices.FieldOffset(8)]         public byte PlayerId;
        [System.Runtime.InteropServices.FieldOffset(12)]        public IntPtr PlayerName;
        [System.Runtime.InteropServices.FieldOffset(16)]        public byte ColorId;
        [System.Runtime.InteropServices.FieldOffset(20)]        public uint HatId;
        [System.Runtime.InteropServices.FieldOffset(24)]        public uint PetId;
        [System.Runtime.InteropServices.FieldOffset(28)]        public uint SkinId;
        [System.Runtime.InteropServices.FieldOffset(32)]        public byte Disconnected;
        [System.Runtime.InteropServices.FieldOffset(36)]        public IntPtr Tasks;
        [System.Runtime.InteropServices.FieldOffset(40)]        public byte IsImpostor;
        [System.Runtime.InteropServices.FieldOffset(41)]        public byte IsDead;
        [System.Runtime.InteropServices.FieldOffset(44)]        public IntPtr _object;
    }



    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        public float x,y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsZero()
        {
            if (this.x == 0 && this.y == 0) return true;
            else return false;
        }
        public bool IsGarbage()
        {
            if (this.x == float.MaxValue && this.y == float.MaxValue) return true;
            else return false;
        }
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }
    }
}