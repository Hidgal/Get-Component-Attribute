namespace GetAttribute
{
    public class GetInParentAttribute : BaseGetAttribute
    {
        public GetInParentAttribute(bool includeInactive = false) : base(includeInactive) { }
    }
}