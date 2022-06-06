using HotChocolate.Types.Descriptors;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Giantnodes.Dashboard.Api
{
    public class SnakeCaseNamingConventions : DefaultNamingConventions
    {
        public override NameString GetMemberName(MemberInfo member, MemberKind kind)
        {
            if (kind == MemberKind.ObjectField)
            {
                var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
                return string.Join("_", pattern.Matches(member.Name)).ToLower();
            }

            return base.GetMemberName(member, kind);
        }

        public override NameString GetEnumValueName(object value)
        {
            string? input = value.ToString();
            if (input is null)
            {
                return base.GetEnumValueName(value);
            }

            return input.ToUpper();
        }
    }
}
