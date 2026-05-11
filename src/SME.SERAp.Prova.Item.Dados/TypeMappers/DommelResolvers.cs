using Dommel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SME.SERAp.Prova.Item.Dados.TypeMappers
{
    public class ColumnAttributeColumnNameResolver : IColumnNameResolver
    {
        public string ResolveColumnName(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            return attr?.Name ?? propertyInfo.Name;
        }
    }

    public class ColumnAttributeTableNameResolver : ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            var attr = type.GetCustomAttribute<TableAttribute>();
            return attr?.Name ?? type.Name.ToLower();
        }
    }

    public class ColumnAttributeKeyPropertyResolver : IKeyPropertyResolver
    {
        // ← retorno é ColumnPropertyInfo[] (array), não IEnumerable
        public ColumnPropertyInfo[] ResolveKeyProperties(Type type)
        {
            var keys = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
                .Select(p => new ColumnPropertyInfo(p, isKey: true)) // ← isKey, não isGenerated
                .ToArray();

            return keys.Length > 0 ? keys : FallbackParaId(type);
        }

        private static ColumnPropertyInfo[] FallbackParaId(Type type)
        {
            var prop = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

            return prop != null
                ? new[] { new ColumnPropertyInfo(prop, isKey: true) }
                : Array.Empty<ColumnPropertyInfo>();
        }
    }
}