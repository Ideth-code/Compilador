using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlueMoon
{
    public class AnLex
    {


        // Definir unas estructura para representar los tokens
        public struct Token
        {
            public TokenType Type;
            public string Value;

            public static object AnLex { get; internal set; }
        }

        // Extensión del enum TokenType para agregar Calcula
        public enum TokenType
        {
            Numero,
            Operador,
            Calcula, // Token para la palabra clave Calcula
            Desconocido
        }

        // Lista de operadores válidos
        private readonly List<string> operators = new List<string> { "+", "-", "*", "/" };

        // Método para analizar el código fuente
        public List<Token> Analyze(string sourceCode)
        {
            List<Token> tokens = new List<Token>();

            // Usar expresiones regulares para identificar patrones de tokens
            //string pattern = @"(Calcula)|(\d+)|([+*/-])";
            string pattern = @"(Calcula)|\s+|(\d+)|([+*/-])";

            var matches = Regex.Matches(sourceCode, pattern);

            foreach (Match match in matches)
            {
                Token token = new Token();

                // Grupo para la palabra clave Calcula
                if (match.Groups[1].Success)
                {
                    token.Type = TokenType.Calcula;
                    token.Value = match.Value;
                }
                // Grupo para números
                else if (match.Groups[2].Success && int.TryParse(match.Value, out _))
                {
                    token.Type = TokenType.Numero;
                    token.Value = match.Value;
                }
                // Grupo para operadores
                else if (match.Groups[3].Success && operators.Contains(match.Value))
                {
                    token.Type = TokenType.Operador;
                    token.Value = match.Value;
                }
                else
                {
                    token.Type = TokenType.Desconocido;
                    token.Value = match.Value;
                }
                tokens.Add(token);
            }

            return tokens;
        }
    






    /*
        //CODIGO ORIGINAL


        // Definir unas estructura para representar los tokens
        public struct Token
        {
          public TokenType Type;
          public string Value;
        }

        // Definir los tipos de tokens posibles
        public enum TokenType
        {
         Numero,
         Operador,
         Desconocido
        }

        // Definir los operadores válidos
        private List<string> operators = new List<string> { "+", "-", "*", "/" };

        // Método para analizar el código fuente
        public List<Token> Analyze(string sourceCode)
        {
         List<Token> tokens = new List<Token>();

        // Dividir el código en palabras separadas
          string[] lines = sourceCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

         foreach (string line in lines)
           {
        string[] words = line.Split(' ');

         foreach (string word in words)
         {
          Token token;
          switch (word)
          {
          case "+":
           case "-":
          case "*":
          case "/":
             token.Type = TokenType.Operador;
               break;
           default:
            if (IsNumeric(word))
           {
               token.Type = TokenType.Numero;
            }
              else
              {
                 token.Type = TokenType.Desconocido;
             }
               break;
           }
           token.Value = word;
             tokens.Add(token);
           }
          }
           return tokens;
        }

        // Método auxiliar para verificar si una cadena es un número
        private bool IsNumeric(string input)
        {
         return Regex.IsMatch(input, @"^\d+$");
        }


        */


}
    
}



