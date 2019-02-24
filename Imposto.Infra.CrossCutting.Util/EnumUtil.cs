using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Imposto.Infra.CrossCutting.Util
{
    /// <summary>
    /// Classe com funcionalidades utilitárias para ENUM.
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Métedo que retorna a string contida na descrição a partir do valor do enum. 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        /// <summary>
        /// Método que retorna uma lista do tipo chave/valor de todos elementos do enum.
        /// </summary>
        /// <typeparam name="T">É preciso passar o tipo do enum.</typeparam>
        /// <returns>Lista a Descrição/Valor de todos os itens do enum.</returns>
        public static List<KeyValuePair<string, string>> GetEnumSelectListDescriptionAndKey<T>()
        {
            var listSelectItens = (Enum.GetNames(typeof(T)).Select(e => 
                new KeyValuePair<string, string>(
                    GetDescription((Enum)Enum.Parse(typeof(T), e.ToString())), 
                    e.ToString())))
                .ToList();

            return listSelectItens;
        }

        /// <summary>
        /// Método que retorna uma lista do tipo chave/valor de todos elementos do enum.
        /// </summary>
        /// <typeparam name="T">É preciso passar o tipo do enum.</typeparam>
        /// <returns>Lista a Chave/Valor de todos os itens do enum.</returns>
        public static List<KeyValuePair<string, string>> GetEnumSelectListKeyAndValue<T>(string format = null, CultureInfo culture = null)
        {
            var listSelectItens = (Enum.GetValues(typeof(T))
                .Cast<T>()
                    .Select(e => new KeyValuePair<string, string>(
                        Enum.GetName(typeof(T), e), 
                        string.IsNullOrEmpty(format) ? e.GetHashCode().ToString() : e.GetHashCode().ToString(format, culture))))
                .ToList();

            return listSelectItens;
        }

    }
}
