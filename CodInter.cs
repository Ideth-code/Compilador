using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMoon
{
    public class CodInter
    {

        public string Generate(List<AnLex.Token> tokens)
        {
            string intermediateCode = "";
            bool calculaKeywordEncountered = false;

            foreach (var token in tokens)
            {
                if (token.Type == AnLex.TokenType.Calcula)
                {
                    calculaKeywordEncountered = true;
                    continue; // Skip Calcula keyword token
                }

                if (!calculaKeywordEncountered)
                {
                    // Other processing here or throw an error if Calcula keyword is mandatory before any expression
                    throw new InvalidOperationException("La palabra clave 'Calcula' debe preceder a la expresión matemática.");
                }

                if (token.Type == AnLex.TokenType.Numero)
                {
                    intermediateCode += $"PUSH {token.Value}\n"; // Changed from 'LOAD' to 'PUSH' for stack-based implementation
                }
                else if (token.Type == AnLex.TokenType.Operador)
                {
                    intermediateCode += $"{GetOperatorInstruction(token.Value)}\n";
                }
                // Include additional token types as necessary
                else if (token.Type == AnLex.TokenType.Desconocido)
                {
                    // Additional logic for unknown tokens if required
                }
            }

            if (!calculaKeywordEncountered)
            {
                throw new InvalidOperationException("No se encontró la palabra clave 'Calcula'.");
            }

            return intermediateCode;
        }

        // Método auxiliar para obtener la instrucción de operador correspondiente
        private string GetOperatorInstruction(string operatorToken)
        {
            switch (operatorToken)
            {
                case "+":
                    return "ADD";
                case "-":
                    return "SUB";
                case "*":
                    return "MUL";
                case "/":
                    return "DIV";
                default:
                    throw new InvalidOperationException($"Operador desconocido: {operatorToken}");
            }
        }


        /*
        // CODIGO ORIGINAL

        public string Generate(List<AnLex.Token> tokens)
                {
                    string intermediateCode = "";

                    foreach (var token in tokens)
                    {
                        if (token.Type == AnLex.TokenType.Numero)
                        {
                            intermediateCode += $"LOAD {token.Value}\n";
                        }
                        else if (token.Type == AnLex.TokenType.Operador)
                        {
                            intermediateCode += $"{GetOperatorInstruction(token.Value)}\n";
                        }
                        else if (token.Type == AnLex.TokenType.Desconocido)
                        {
                            intermediateCode += "STORE\n";
                        }
                    }

                    return intermediateCode;
                }

                // Método auxiliar para obtener la instrucción de operador correspondiente
                private string GetOperatorInstruction(string operatorToken)
                {
                    switch (operatorToken)
                    {
                        case "+":
                            return "SUM";
                        case "-":
                            return "RES";
                        case "*":
                            return "MUL";
                        case "/":
                            return "DIV";
                        default:
                            return "";
                    }
                }


        */



    }
}










/*
 modifica el codigo del analizador lexico para que se creen dos sintaxis: "Imprime" 
 para mostrar cadenas de texto y "Calcula" para realizar operaciones matematicas basicas. ejemplo (Imprime Hola mundo), (Calcula 5+ 5)
     */
