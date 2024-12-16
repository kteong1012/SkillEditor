using UnityEngine;

namespace SkillEditor
{
    public interface IMovableDrawer
    {
        bool IsMoving { get; set; }
        ScalableRect ItemRect { get; set; }
    }
}