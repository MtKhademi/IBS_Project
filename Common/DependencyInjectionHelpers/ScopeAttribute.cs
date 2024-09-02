namespace Common.DependencyInjectionHelpers
{

    public enum EScopetype
    {
        Transiant = 0,
        Scope = 1,
        Singleton = 2
    }

    public class ScopeAttribute : Attribute
    {
        public EScopetype ScopeType { get; set; } = EScopetype.Scope;

        public ScopeAttribute(EScopetype scope = EScopetype.Scope)
        {
            ScopeType = scope;
        }
    }
}
