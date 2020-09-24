using UnityAtoms;

namespace ScriptableObjects.Atoms.Transform.Actions
{
    /// <summary>
    ///     Action of type `Transform`. Inherits from `AtomAction&lt;Transform&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    public abstract class TransformAction : AtomAction<UnityEngine.Transform>
    {
    }
}