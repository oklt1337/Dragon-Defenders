namespace Abilities.VisitorPattern.Scripts
{
    public interface IElement
    {
        public void Accept(IVisitor visitor);
    }
}
