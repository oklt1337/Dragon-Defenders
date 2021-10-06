using UnityEngine;

namespace _Project.SkillSystem.SkillDataBases
{
    public abstract class SkillDataBase : ScriptableObject
    {
        

        [SerializeField]protected Sprite sprite;
        [SerializeField] protected string skillName;
        [SerializeField][TextArea] protected string description;
        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }
        public string SkillName 
        {
            get => skillName;
            set => skillName = value;
        }
         
        public string Description
        {
            get => description;
            set => description = value;
        }
    }
}
