using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BlueMoon.AnLex;
//using static BlueMoon.AnLex;

namespace BlueMoon
{
    public class AnSem
    {

        /*
         actualiza el analizador semantico para que analice y pueda generar 
         el resultado de la sintaxis Calcula despues de que se le haya agregado una operacion matematica basica. 
          ej Calcula 5 + 5
         */

        /*       anterior

                public double Evaluate(List<AnLex.Token> tokens)
                {
                    Stack<double> evaluationStack = new Stack<double>();
                    bool calculaKeywordEncountered = false;

                    foreach (var token in tokens)
                    {
                        if (token.Type == AnLex.TokenType.Calcula)
                        {
                            calculaKeywordEncountered = true;
                            continue; // Skip the Calcula keyword
                        }

                        if (!calculaKeywordEncountered)
                            throw new InvalidOperationException("La expresión debe comenzar con la palabra clave 'Calcula'.");

                        if (token.Type == AnLex.TokenType.Numero)
                        {
                            evaluationStack.Push(Convert.ToDouble(token.Value));
                        }
                        else if (token.Type == AnLex.TokenType.Operador)
                        {
                            // if (evaluationStack.Count < 1)
                            //if (evaluationStack.Count == 0)
                            //      throw new InvalidOperationException("Expresión mal formada. Se esperaban suficientes operandos para una operación.");

                            //   double rightOperand = evaluationStack.Pop();
                            //   double leftOperand = evaluationStack.Pop();
                            //   double result = ApplyOperation(token.Value, leftOperand, rightOperand);
                            //  evaluationStack.Push(result);

                            if (evaluationStack.Count >= 2)
                            {
                                double rightOperand = evaluationStack.Pop();
                                double leftOperand = evaluationStack.Pop();
                                switch (token.Value)
                                {
                                    case "+":
                                        evaluationStack.Push(leftOperand + rightOperand);
                                        break;
                                    case "-":
                                        evaluationStack.Push(leftOperand - rightOperand);
                                        break;
                                    case "*":
                                        evaluationStack.Push(leftOperand * rightOperand);
                                        break;
                                    case "/":
                                        evaluationStack.Push(leftOperand / rightOperand);
                                        break;
                                }
                            }

                        }
                    }

                    if (!calculaKeywordEncountered)
                        throw new InvalidOperationException("No se encontró la palabra clave 'Calcula'.");

                    if (evaluationStack.Count != 1)
                        throw new InvalidOperationException("Expresión mal formada. Debería quedar un solo resultado tras evaluar.");

                    return evaluationStack.Peek(); 
                }

                private double ApplyOperation(string operatorToken, double leftOperand, double rightOperand)
                {
                    switch (operatorToken)
                    {
                        case "+":
                            return leftOperand + rightOperand;
                        case "-":
                            return leftOperand - rightOperand;
                        case "*":
                            return leftOperand * rightOperand;
                        case "/":
                            if (rightOperand == 0)
                                throw new DivideByZeroException("División por cero no permitida.");
                            return leftOperand / rightOperand;
                        default:
                            throw new InvalidOperationException($"Operador desconocido: {operatorToken}");
                    }
                }
            }



            */



        /*      reciente

                public double Evaluate(List<Token> tokens)
                {
                    if (tokens == null || tokens.Count == 0)
                        throw new InvalidOperationException("No hay tokens para evaluar.");

                    Stack<double> evaluationStack = new Stack<double>();

                    foreach (var token in tokens)
                    {
                        switch (token.Type)
                        {
                            case TokenType.Numero:
                                evaluationStack.Push(double.Parse(token.Value));
                                break;
                            case TokenType.Operador:
                                if (evaluationStack.Count == 0)
                                    throw new InvalidOperationException("Expresión mal formada. Se esperaban suficientes operandos para una operación.");

                                // Pop operands in reverse order: right operand first, then left operand.
                                double rightOperand = evaluationStack.Pop();
                                double leftOperand = evaluationStack.Pop();
                                // Assume ApplyOperation is a method that applies mathematical operations.
                                double result = ApplyOperation(rightOperand, leftOperand, token.Value);
                                evaluationStack.Push(result);
                                break;
                        }
                    }

                    // After evaluating all tokens, check if there's exactly one result left in the stack.
                    if (evaluationStack.Count != 1)
                        throw new InvalidOperationException("Expresión mal formada. Debería quedar un solo resultado tras evaluar.");

                    // Pop the final result from the stack and return it.
                    return evaluationStack.Pop();
                }

                private double ApplyOperation(double leftOperand, double rightOperand, string operatorToken)
                {
                    // This method should implement the logic to perform the operation based on the operatorToken.
                    switch (operatorToken)
                    {
                        case "+": return leftOperand + rightOperand;
                        case "-": return leftOperand - rightOperand;
                        case "*": return leftOperand * rightOperand;
                        case "/": return leftOperand / rightOperand;
                        default: throw new InvalidOperationException("Operador desconocido.");
                    }
                }



                */

        /*  otro intento

public double Evaluate(List<AnLex.Token> tokens)
{
    Stack<double> evaluationStack = new Stack<double>();
    bool calculaKeywordEncountered = false;

    foreach (var token in tokens)
    {
        switch (token.Type)
        {
            case AnLex.TokenType.Calcula:
                calculaKeywordEncountered = true;
                break; // Skip the Calcula keyword

            case AnLex.TokenType.Numero when calculaKeywordEncountered:
                evaluationStack.Push(Convert.ToDouble(token.Value));
                break;

            case AnLex.TokenType.Operador when calculaKeywordEncountered:
                if (evaluationStack.Count < 2)
                {
                    throw new InvalidOperationException("Expresión mal formada. Se esperaban suficientes operandos para una operación.");
                }
                double rightOperand = evaluationStack.Pop();
                double leftOperand = evaluationStack.Pop();
                switch (token.Value)
                {
                    case "+":
                        evaluationStack.Push(leftOperand + rightOperand);
                        break;
                    case "-":
                        evaluationStack.Push(leftOperand - rightOperand);
                        break;
                    case "*":
                        evaluationStack.Push(leftOperand * rightOperand);
                        break;
                    case "/":
                        if (rightOperand == 0)
                            throw new DivideByZeroException("División por cero no permitida.");
                        evaluationStack.Push(leftOperand / rightOperand);
                        break;
                    default:
                        throw new InvalidOperationException($"Operador desconocido: {token.Value}");
                }
                break;

            default:
                if (calculaKeywordEncountered)   // Unknown token after Calcula keyword
                    throw new InvalidOperationException($"Token desconocido o no permitido: {token.Value}");
                break;
        }
    }

    if (!calculaKeywordEncountered)
        throw new InvalidOperationException("No se encontró la palabra clave 'Calcula'.");

    if (evaluationStack.Count != 1)
        throw new InvalidOperationException("Expresión mal formada. Debería quedar un solo resultado tras evaluar.");

    return evaluationStack.Peek();
}



*/

        // ultimo
    public double Evaluate(List<AnLex.Token> tokens)
    {
        Stack<double> evaluationStack = new Stack<double>();
        bool calculaKeywordEncountered = false;

        foreach (var token in tokens)
        {
            if (token.Type == AnLex.TokenType.Calcula)
            {
                calculaKeywordEncountered = true;
                continue; // Skip the Calcula keyword
            }

            if (!calculaKeywordEncountered)
                throw new InvalidOperationException("La expresión debe comenzar con la palabra clave 'Calcula'.");

            if (token.Type == AnLex.TokenType.Numero)
            {
                evaluationStack.Push(Convert.ToDouble(token.Value));
            }
            else if (token.Type == AnLex.TokenType.Operador)
            {
                if (evaluationStack.Count < 2)
                    throw new InvalidOperationException("Expresión mal formada. Se esperaban suficientes operandos para una operación.");

                double rightOperand = evaluationStack.Pop();
                double leftOperand = evaluationStack.Pop();
                double result = ApplyOperation(token.Value, leftOperand, rightOperand);
                evaluationStack.Push(result);
            }
        }

        if (!calculaKeywordEncountered)
            throw new InvalidOperationException("No se encontró la palabra clave 'Calcula'.");

        if (evaluationStack.Count != 1)
            throw new InvalidOperationException("Expresión mal formada. Debería quedar un solo resultado tras evaluar.");

        return evaluationStack.Peek();
    }

        internal double Analyze(List<Token> tokens)
        {
            throw new NotImplementedException();
        }

        private double ApplyOperation(string operatorToken, double leftOperand, double rightOperand)
    {
        switch (operatorToken)
        {
            case "+":
                return leftOperand + rightOperand;
            case "-":
                return leftOperand - rightOperand;
            case "*":
                return leftOperand * rightOperand;
            case "/":
                if (rightOperand == 0)
                    throw new DivideByZeroException("División por cero no permitida.");
                return leftOperand / rightOperand;
            default:
                throw new InvalidOperationException($"Operador desconocido: {operatorToken}");
        }
    }




    




        /*

    private List<AnLex.Token> tokens;

    public string Analyze(List<AnLex.Token> tokens)
    {
        this.tokens = tokens;
        int result = 0;

        try
        {
            // Comprobamos que la primera instrucción es 'Calcula'
            if (tokens.FirstOrDefault().Type != TokenType.Calcula)
                throw new SemanticException("Error semántico: Se esperaba la palabra clave 'Calcula'.");

            // Removemos la primera instrucción 'Calcula'
            var filteredTokens = tokens.Skip(1).ToList();

            // Verificación semántica y evaluación de la expresión
            result = EvaluateExpression(filteredTokens);

            return "El resultado es: " + result.ToString();
        }
        catch (SemanticException ex)
        {
            return "Error semántico: " + ex.Message;
        }
        catch
        {
            return "Error semántico desconocido.";
        }
    }

    private int EvaluateExpression(List<Token> expressionTokens)
    {
        if (expressionTokens.Count == 0)
            throw new SemanticException("Error semántico: No hay tokens para evaluar.");

        // Inicializar los valores
        int currentIndex = 0;
        int result = EvaluateTerm(ref currentIndex, expressionTokens);

        while (currentIndex < expressionTokens.Count)
        {
            AnLex.Token currentToken = expressionTokens[currentIndex];

            // Verificamos que los siguientes tokens sean operadores matemáticos
            if (currentToken.Type == AnLex.TokenType.Operador &&
                (currentToken.Value == "+" || currentToken.Value == "-"))
            {
                currentIndex++;

                int termValue = EvaluateTerm(ref currentIndex, expressionTokens);

                // Actualizar resultado basándonos en el operador
                switch (currentToken.Value)
                {
                    case "+":
                        result += termValue;
                        break;
                    case "-":
                        result -= termValue;
                        break;
                }
            }
            else
            {
                throw new SemanticException("Error semántico: Se esperaba un operador.");
            }
        }

        return result;
    }

    private int EvaluateTerm(ref int currentIndex, List<Token> expressionTokens)
    {
        int factorValue = EvaluateFactor(ref currentIndex, expressionTokens);

        while (currentIndex < expressionTokens.Count)
        {
            AnLex.Token currentToken = expressionTokens[currentIndex];

            if (currentToken.Type == AnLex.TokenType.Operador &&
                (currentToken.Value == "*" || currentToken.Value == "/"))
            {
                currentIndex++;
                int nextFactorValue = EvaluateFactor(ref currentIndex, expressionTokens);

                switch (currentToken.Value)
                {
                    case "*":
                        factorValue *= nextFactorValue;
                        break;
                    case "/":
                        if (nextFactorValue == 0)
                            throw new SemanticException("Error semántico: División por cero.");
                        factorValue /= nextFactorValue;
                        break;
                }
            }
            else
            {
                break;
            }
        }

        return factorValue;
    }
    /*
    private int EvaluateFactor(ref int currentIndex, List<Token> expressionTokens)
    {
        if (currentIndex >= expressionTokens.Count || expressionTokens[currentIndex].Type != Token.AnLex.TokenType.Numero)
            throw new SemanticException("Error semántico: Se esperaba un número.");

        int value = int.Parse(expressionTokens[currentIndex].Value);
        currentIndex++;

        return value;
    }*/
        /*

        private int EvaluateFactor(ref int currentIndex, List<Token> expressionTokens)
        {
            if (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Numero)
                {
                    currentIndex++;
                    return int.Parse(currentToken.Value);
                }
            }

            throw new SemanticException("Factor inválido.");
        }

    }

    public class SemanticException : System.Exception
    {
        public SemanticException(string message) : base(message)
        {
        }
    }
    */








        // CODIGO ORIGINAL

        /*
                private List<AnLex.Token> tokens;

                public string Analyze(List<AnLex.Token> tokens)
                {
                    this.tokens = tokens;

                    try
                    {
                        // Realizar el análisis semántico
                        int result = EvaluateExpression();

                        return "El resultado es: " + result.ToString();
                    }
                    catch (SemanticException ex)
                    {
                        return "Error semántico: " + ex.Message;
                    }
                    catch
                    {
                        return "Error semántico desconocido.";
                    }
                }

            */
            /*
        private List<AnLex.Token> tokens;

        public string Analyze(List<AnLex.Token> tokens)
        {
            this.tokens = tokens;

            try
            {
                // Verificar si el primer token es el comando 'Calcula'
                if (tokens.Count > 0 && tokens[0].Type == AnLex.TokenType.Calcula)
                {
                    // Realizar el análisis semántico solo si se encuentra 'Calcula' al inicio
                    int result = EvaluateExpression();
                    return "El resultado es: " + result.ToString();
                }
                else
                {
                    return "Error semántico: Se esperaba el comando 'Calcula' al inicio de la expresión.";
                }
            }
            catch (SemanticException ex)
            {
                return "Error semántico: " + ex.Message;
            }
            catch
            {
                return "Error semántico desconocido.";
            }
        }

        
        private int EvaluateExpression()
        {
            // Inicializar los valores
            int currentIndex = 0;
            int result = 0;

            // Evaluar la expresión
            result = EvaluateTerm(ref currentIndex);

            while (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "+" || currentToken.Value == "-"))
                {
                    currentIndex++;

                    int termValue = EvaluateTerm(ref currentIndex);

                    if (currentToken.Value == "+")
                    {
                        result += termValue;
                    }
                    else if (currentToken.Value == "-")
                    {
                        result -= termValue;
                    }
                }
            }

            return result;
        }
        
        private int EvaluateTerm(ref int currentIndex)
        {
            int factorValue = EvaluateFactor(ref currentIndex);

            while (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "*" || currentToken.Value == "/"))
                {
                    currentIndex++;

                    int factor = EvaluateFactor(ref currentIndex);

                    if (currentToken.Value == "*")
                    {
                        factorValue *= factor;
                    }
                    else if (currentToken.Value == "/")
                    {
                        if (factor == 0)
                        {
                            throw new SemanticException("División por cero.");
                        }

                        factorValue /= factor;
                    }
                }
                else
                {
                    break;
                }
            }

            return factorValue;
        }

        /*
        private int EvaluateFactor(ref int currentIndex)
        {
            if (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Numero)
                {
                    currentIndex++;
                    return int.Parse(currentToken.Value);
                }
            }

            throw new SemanticException("Factor inválido.");
        }*/
        /*
        private int EvaluateFactor(ref int currentIndex)
        {
            if (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Numero)
                {
                    currentIndex++;
                    return int.Parse(currentToken.Value);
                }
                else
                {
                    throw new SemanticException("Factor inválido. Se esperaba un número pero se encontró otro tipo de token.");
                }
            }
            else
            {
                throw new SemanticException("Factor inválido. Se llegó al final de la expresión pero se esperaba un número.");
            }
        }
       

        


    }

    public class SemanticException : System.Exception
    {
        public SemanticException(string message) : base(message)
        {
        }


    */
        
    



    }

    }
