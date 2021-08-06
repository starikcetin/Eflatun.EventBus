using System.Text;

namespace Eflatun.EventBus.Dev
{
    public static class Utils
    {
        public static string ToObjectSummaryString(this object obj)
        {
            var objType = obj.GetType();
            var objProps = objType.GetProperties();
            var objFields = objType.GetFields();
            var objName = objType.Name;

            var sb = new StringBuilder();
            sb.Append($"{objName} {{ ");

            foreach (var info in objProps)
            {
                var value = info.GetValue(obj, null) ?? "(null)";
                sb.Append(info.Name + ": " + value + ", ");
            }

            foreach (var info in objFields)
            {
                var value = info.GetValue(obj) ?? "(null)";
                sb.Append(info.Name + ": " + value + ", ");
            }

            sb.Length -= 2;
            sb.Append(" }");

            return sb.ToString();
        }
    }
}
