using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SME.SERAp.Prova.Item.Dados.TypeMappers
{
    /// <summary>
    /// Ensina o Dapper a resolver nomes de colunas através do atributo [Column].
    /// Substitui o Dapper.FluentMap + Dapper.FluentMap.Dommel (ambos depreciados).
    /// </summary>
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
            {
                new CustomPropertyTypeMap(typeof(T), ResolverColuna),
                new DefaultTypeMap(typeof(T))
            })
        { }

        private static PropertyInfo? ResolverColuna(Type tipo, string nomeColuna)
        {
            return tipo
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p =>
                    p.GetCustomAttribute<ColumnAttribute>()?.Name
                        ?.Equals(nomeColuna, StringComparison.OrdinalIgnoreCase) == true
                    || p.Name.Equals(nomeColuna, StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// Tenta cada mapeador na ordem; usa o primeiro que resolver a propriedade.
    /// </summary>
    public class FallbackTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }

        public ConstructorInfo? FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var ctor = mapper.FindConstructor(names, types);
                    if (ctor != null) return ctor;
                }
                catch { /* tenta o próximo */ }
            }
            return null;
        }

        public ConstructorInfo? FindExplicitConstructor()
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var ctor = mapper.FindExplicitConstructor();
                    if (ctor != null) return ctor;
                }
                catch { /* tenta o próximo */ }
            }
            return null;
        }

        public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var param = mapper.GetConstructorParameter(constructor, columnName);
                    if (param != null) return param;
                }
                catch { /* tenta o próximo */ }
            }
            return null;
        }

        public SqlMapper.IMemberMap? GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var member = mapper.GetMember(columnName);
                    if (member != null) return member;
                }
                catch { /* tenta o próximo */ }
            }
            return null;
        }
    }
}