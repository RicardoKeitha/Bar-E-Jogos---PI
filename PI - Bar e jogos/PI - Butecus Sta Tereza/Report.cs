using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI___Butecus_Sta_Tereza
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        // Função para visualizar o pdf na tela 
        public void getPDF(string local, string tittle)
        {
            this.Text = tittle;
            axAcroPDF.src = local;
            axAcroPDF.Show();
        }
    }
}
