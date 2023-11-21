using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace BlueMoon
{
    public partial class Interfaz : Form
    {
        private AnLex AL;
        private AnSin ASin;
        private AnSem AnSem;
        private CodInter CodInter;


        public Interfaz()
        {
            InitializeComponent();
            AL = new AnLex();
            ASin = new AnSin();
            AnSem = new AnSem();
            CodInter = new CodInter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Interfaz_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tTSB3_Click(object sender, EventArgs e)
        {
          

         //Analizador Lexico
         string sourceCode = txtCod.Text;
         List<AnLex.Token> tokens = AL.Analyze(sourceCode);

         txtResult.Text = string.Empty;
         foreach (AnLex.Token token in tokens)
         {

              txtResult.Text += $"Tipo: {token.Type}, Valor: {token.Value}\r\n";
         }

         //Analizador Sintactico
         string result = ASin.Analyze(tokens);

         txtResult2.Text = result;

         if (!result.StartsWith("Error de sintaxis"))
         {
             //Analizador Semantico
             double semanticResult = AnSem.Evaluate(tokens);
                //string semanticResult = AnSem.Analyze(tokens);

                txtResult3.Text = semanticResult.ToString();
/*
             if (!semanticResult.Evaluate("Error semántico"))
             {
                 txtResult4.Text = semanticResult.ToString(); // Muestra el resultado en el TextBox 4
             }*/
             
             // Generar el código intermedio
             string intermediateCode = CodInter.Generate(tokens);

             // Mostrar el código intermedio en el tercer TextBox
             txtResult4.Text = intermediateCode;
         }
         else
         {
             // Mostrar el resultado del análisis sintáctico en el tercer TextBox
             txtResult2.Text = result;
         }



    }

         


        private void txtResult2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}