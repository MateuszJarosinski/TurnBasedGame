namespace Interfaces
{
    public interface IAmGridUnit : IAmMovable, IHavePosition
    {
        public void MakeInteractable();
        public void MakeNonInteractable();
    }
}
