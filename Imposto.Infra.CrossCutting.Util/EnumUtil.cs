using System.Collections.Generic;
using System.ComponentModel;
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
        /// Método que retorno uma lista do tipo chave/valor de todos elementos do enum.
        /// </summary>
        /// <typeparam name="T">É preciso passar o tipo do enum.</typeparam>
        /// <returns>Lista o valor e descrição de todos os itens do enum.</returns>
        public static List<KeyValuePair<string, string>> GetEnumSelectList<T>()
        {
            var listSelectItens = (System.Enum.GetNames(typeof(T)).Select(e => new KeyValuePair<string, string>(GetDescription((System.Enum)System.Enum.Parse(typeof(T), e.ToString())), e.ToString() )   )).ToList();

            return listSelectItens;
        }

    }
}
