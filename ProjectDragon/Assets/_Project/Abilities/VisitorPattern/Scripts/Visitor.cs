using SkillSystem.Nodes.BaseNodes.Scripts;
using SkillSystem.SkillTree.Scripts;

namespace Abilities.VisitorPattern.Scripts
{
    public interface IVisitor
    {
        public void Visit(Node node);
    }
}
