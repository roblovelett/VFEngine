using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Character.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;

    [CreateAssetMenu(fileName = "CharacterSettings", menuName = CharacterSettingsPath, order = 0)]
    public class CharacterSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public FacingDirection initialFacingDirection;
        [SerializeField] public SpawnFacingDirection spawnFacingDirection;
        [SerializeField] public CharacterType characterControlType;
        [SerializeField] public Animator characterAnimator;
        [SerializeField] public bool useDefaultMecanim = true;
        [SerializeField] public GameObject characterModel;
        [SerializeField] public GameObject cameraTarget;
        [SerializeField] public float cameraTargetSpeed = 5f;
        [SerializeField] public bool flipModelOnDirectionChange = true;
        [SerializeField] public Vector3 transformScaleOnModelFlip = new Vector3(-1, 1, 1);
        [SerializeField] public bool rotateModelOnDirectionChange;
        [SerializeField] public Vector3 modelRotationAngleOnDirectionChange = new Vector3(0f, 180f, 0f);
        [SerializeField] public float modelRotationSpeed;
        [SerializeField] public bool sendStateChangeEvents = true;
        [SerializeField] public bool sendStateUpdateEvents = true;
        [SerializeField] public float minimumAirborneDistance = 0.5f;

        public enum FacingDirection
        {
            Left,
            Right
        }

        public enum SpawnFacingDirection
        {
            Default,
            Left,
            Right
        }

        public enum CharacterType
        {
            Player,
            AI
        }

        #endregion
    }
}