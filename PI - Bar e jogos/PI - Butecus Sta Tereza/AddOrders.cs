using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Access
using System.Data.OleDb;

// iTextSharp
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PI___Butecus_Sta_Tereza
{
    public partial class AddOrders : Form
    {
        // Conexão com banco de dados
        private static string localConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/PI - Banco de Dados.mdb;";
        readonly OleDbConnection connection = new OleDbConnection(Convert.ToString(localConnection));

        public AddOrders()
        {
            InitializeComponent();

            // Total da comanda
            TotaldaComanda(labelValorTotal, listViewOPBC);
        }

        // ####################################################################################################################################################################################################### //
        // ####################################################################################### Bebidas e Comidas ############################################################################################# //
        // ####################################################################################################################################################################################################### //
        // DataTables de ListViewBebidaseComidas
        readonly DataTable dataTableListViewOBebidaseComidas = new DataTable();
        // DataTables de ComboBox
        readonly DataTable dataTableComboBoxOBebidaseComidas = new DataTable();
        // Setup
        public void SetupListViewOBebidaseComidas()
        {
            listViewOBebidaseComidas.View = View.Details;
            listViewOBebidaseComidas.FullRowSelect = true;
            listViewOBebidaseComidas.MultiSelect = false;
            listViewOBebidaseComidas.Columns.Add("Código", 100);
            listViewOBebidaseComidas.Columns.Add("Nome", 100);
            listViewOBebidaseComidas.Columns.Add("Preço", 100);
            listViewOBebidaseComidas.Columns.Add("Categoria", 100);
            listViewOBebidaseComidas.Columns.Add("Descrição", 100);

            // Listar
            ListarOBebidaseComidas();
            ListarOCategoriasBebidaseComidas();
        }

        // Validação do campo preço
        private void textBoxOBCQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        // Função para listar as bebidas e comidas 
        private void ListarOBebidaseComidas()
        {
            int[] items = { 0, 1, 2, 3, 4 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `BebidaseComidas`", 1, listViewOBebidaseComidas, null, null, connection, dataTableListViewOBebidaseComidas, items);
        }

        // Função para ordenar pela categoria selecionada
        private void comboBoxOBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOBCSearch.Text = "";
            if (comboBoxOBC.Text == "Todos")
            {
                ListarOBebidaseComidas();
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `BebidaseComidas` WHERE Categoria LIKE '{comboBoxOBC.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewOBebidaseComidas, null, null, connection, dataTableListViewOBebidaseComidas, items);
            }
        }
        // Listar as categorias para bebidas e comidas
        private void ListarOCategoriasBebidaseComidas()
        {
            int[] items = { 1 };
            string sql = "SELECT * FROM `Categorias` WHERE Tipo LIKE 'Cardapio'";
            comboBoxOBC.SelectedIndex = 0;
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxOBC, null, connection, dataTableComboBoxOBebidaseComidas, items);
        }
        // Função para pesquisar comidas ou bebidas 
        private void buttonOBCSearch_Click(object sender, EventArgs e)
        {
            comboBoxOBC.SelectedIndex = 0;
            if (textBoxOBCSearch.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `BebidaseComidas` WHERE CodigodaBebidaseComidas LIKE '{textBoxOBCSearch.Text}' OR Nome LIKE '{textBoxOBCSearch.Text}' OR Preco LIKE '{textBoxOBCSearch.Text}' OR Categoria LIKE '{textBoxOBCSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewOBebidaseComidas, null, null, connection, dataTableListViewOBebidaseComidas, items);
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### JOGOS ######################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // DataTables do ListView
        readonly DataTable dataTableListViewOJogos = new DataTable();

        // DataTables do ComboBox
        readonly DataTable dataTableComboBoxOJogos = new DataTable();

        // Setup do ListViewJogos
        public void SetupListViewOJogos()
        {
            listViewOJogos.View = View.Details;
            listViewOJogos.FullRowSelect = true;
            listViewOJogos.MultiSelect = false;
            listViewOJogos.Columns.Add("Código", 100);
            listViewOJogos.Columns.Add("Nome", 100);
            listViewOJogos.Columns.Add("Categoria", 100);
            listViewOJogos.Columns.Add("Descrição", 100);

            //Listar contéudo do database
            ListarOJogos();
            listarOCategoriaJogos();
        }


        // Função para listar os jogos no ListViewJogos
        private void ListarOJogos()
        {
            int[] items = { 0, 1, 2, 3, 4 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Jogos` WHERE Status LIKE 'Livre'", 1, listViewOJogos, null, null, connection, dataTableListViewOJogos, items);
        }

        // Função para listar as categoria de jogos
        private void listarOCategoriaJogos()
        {
            int[] items = { 1 };
            string sql = "SELECT * FROM `Categorias` WHERE Tipo LIKE 'Jogos'";
            comboBoxOJ.SelectedIndex = 0;
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxOJ, null, connection, dataTableComboBoxOJogos, items);
        }

        // Função para ordernar os jogos de acordo com a categoria selecionada
        private void comboBoxOJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOJSearch.Text = "";
            if (comboBoxOJ.Text == "Todos")
            {
                ListarOJogos();
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `Jogos` WHERE Categoria LIKE '{comboBoxOJ.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewOJogos, null, null, connection, dataTableListViewOJogos, items);
            }
        }
        // Função para pesquisar Jogos
        private void buttonOJSearch_Click(object sender, EventArgs e)
        {
            comboBoxOJ.SelectedIndex = 0;
            if (textBoxOJSearch.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1, 3, 4 };
                string sql = $"SELECT * FROM `Jogos` WHERE CodigodoJogo LIKE '{textBoxOJSearch.Text}' OR Nome LIKE '{textBoxOJSearch.Text}' OR Status LIKE '{textBoxOJSearch.Text}' OR Categoria LIKE '{textBoxOJSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewOJogos, null, null, connection, dataTableListViewOJogos, items);
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### FUNÇÕES ####################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // Função que listar os dados do database no ListView ou comboBox
        private void ListarConteudoListView_OR_ComboBox(string sql, int opcao, ListView listView, ComboBox comboBox, Label label, OleDbConnection connection, DataTable dataTable, int[] Items)
        {
            // Opções
            // opcao == 1 == ListView
            // opcao == 2 == ComboBox
            // opcao == 3 == Label

            // Variáveis de configuração inicial
            OleDbCommand command;
            OleDbDataAdapter dataAdapter;

            // Limpar listView
            if (opcao == 1)
            {
                listView.Items.Clear();
            }

            // Executar o SQL
            command = new OleDbCommand(sql, connection);

            // Verificar se operação ocorreu de forma correta
            try
            {
                connection.Open();
                dataAdapter = new OleDbDataAdapter(command);
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    if (opcao == 1)
                    {
                        switch (Items.Count())
                        {
                            case 1:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString() }));
                                break;
                            case 2:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString() }));
                                break;
                            case 3:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString(), row[Items[2]].ToString() }));
                                break;
                            case 4:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString(), row[Items[2]].ToString(), row[Items[3]].ToString() }));
                                break;
                            case 5:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString(), row[Items[2]].ToString(), row[Items[3]].ToString(), row[Items[4]].ToString() }));
                                break;
                            case 6:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString(), row[Items[2]].ToString(), row[Items[3]].ToString(), row[Items[4]].ToString(), row[Items[5]].ToString() }));
                                break;
                            case 7:
                                listView.Items.Add(new ListViewItem(new[] { row[Items[0]].ToString(), row[Items[1]].ToString(), row[Items[2]].ToString(), row[Items[3]].ToString(), row[Items[4]].ToString(), row[Items[5]].ToString(), row[Items[6]].ToString() }));
                                break;
                        }
                    }
                    else if (opcao == 2)
                    {
                        comboBox.Items.Add(row[Items[0]].ToString());
                    }
                    else
                    {
                        label.Text = row[Items[0]].ToString();
                    }
                }
                connection.Close();
                dataTable.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }


        // Função de inserir registros do database
        private void CadastrarRegistros(string sql, int i, string msgOK, OleDbConnection connection)
        {
            // Variáveis de configuração inicial
            OleDbCommand command;

            // Executar o SQL
            command = new OleDbCommand(sql, connection);

            try
            {
                connection.Open();
                if (i == 1)
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(msgOK);
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();

            }
        }

        // Função para deletar registros do database
        private void DeletarRegistros(string sql, int opcao, string titleMsgAlert, string msgAlert, string msgOK, OleDbConnection connection)
        {
            // Variáveis de configuração inicial
            OleDbCommand command;
            OleDbDataAdapter dataAdapter;

            // Execultar o SQL
            command = new OleDbCommand(sql, connection);

            try
            {
                connection.Open();
                dataAdapter = new OleDbDataAdapter(command);
                dataAdapter.DeleteCommand = connection.CreateCommand();
                dataAdapter.DeleteCommand.CommandText = sql;

                if (opcao == 1)
                {
                    if (MessageBox.Show(msgAlert, titleMsgAlert, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(msgOK);
                        }
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        // Função de alterar registros do database
        private void EditarRegistro(string sql, int i, string msgAlert, string titleMsgAlert, string msgOK, OleDbConnection connection)
        {
            // Opções
            // i == 1 == Msg
            // i != 1 == Sem Msg

            // Variáveis de configuração inicial
            OleDbCommand command;
            OleDbDataAdapter dataAdapter;

            // Execultar o SQL
            command = new OleDbCommand(sql, connection);

            try
            {
                connection.Open();
                dataAdapter = new OleDbDataAdapter(command)
                {
                    UpdateCommand = connection.CreateCommand()
                };
                dataAdapter.UpdateCommand.CommandText = sql;

                if (i == 1)
                {
                    if (MessageBox.Show(msgAlert, titleMsgAlert, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (dataAdapter.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(msgOK);
                        }
                    }
                }
                else
                {
                    dataAdapter.UpdateCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }
        // Função que gerar os PDFs.
        private void GerarNotaFiscal(Document document, ListView listview, string tittleReport, string subtittleReport, string paragraph, int colunas)
        {
            //Criar o documento e configurar o tipo de página
            Document relatorioDoc = document;

            // O Caminho onde sera criado o pdf 
            string localDoc = Application.StartupPath + $@"\Relatorio-{tittleReport}.pdf";

            // Criar um arquivo pdf em branco
            PdfWriter relatorioWrite = PdfWriter.GetInstance(relatorioDoc, new FileStream(localDoc, FileMode.Create));

            // Abrir o PDF
            relatorioDoc.Open();

            // Título
            Paragraph header = new Paragraph();
            header.Add($"Butecu's Sta Tereza - Nota Fiscal - {DateTime.Now} \n\n");
            header.Alignment = Element.ALIGN_CENTER;

            // Conteúdo
            Paragraph conteudo = new Paragraph();
            conteudo.Alignment = Element.ALIGN_JUSTIFIED;
            conteudo.Add(paragraph);

            // Adicionar o header, imagem e conteudo no pdf
            relatorioDoc.Add(header);
            relatorioDoc.Add(conteudo);

            PdfPTable pdfTable = new PdfPTable(colunas);

            // Adicionando as colunas no documento
            foreach (ColumnHeader column in listview.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Text));
                pdfTable.AddCell(cell);
            }

            // Adicionar os valores do listview
            foreach (ListViewItem itemRow in listview.Items)
            {
                int j = 0;
                for (j = 0; j < itemRow.SubItems.Count; j++)
                {
                    pdfTable.AddCell(itemRow.SubItems[j].Text);
                }
            }
            relatorioDoc.Add(pdfTable);
            relatorioDoc.Close();

            // Abre o pdf em um form
            Report pdf = new Report();
            pdf.getPDF(Convert.ToString(localDoc), subtittleReport);
            pdf.Show();
        }
        // ####################################################################################################################################################################################################### //
        // ################################################################################################################ CONFIGURAÇÃO ######################################################################### //
        // ####################################################################################################################################################################################################### //
        // Função para atualizar pagina
        public void AtualizarPage()
        {
            SetupListViewOBebidaseComidas();
            SetupListViewOJogos();
            SetupListViewPedidosBebidaseComidas();
            SetupListViewPedidosJogos();
        }

        // Função para indentificação das mesa e comanda
        public void ViewMesaeComanda(string mesa, string comanda)
        {
            labelMesa.Text = mesa;
            labelComanda.Text = comanda;
        }

        // DataTable do Total de Comandas
        readonly DataTable dataTableTotalComandas = new DataTable();

        // Função de mostrar o total da comanda
        private void TotaldaComanda(Label label, ListView listview)
        {
            var total = 0m;
            for (int i = 0; i < listview.Items.Count; i++)
            {
                total += decimal.Parse(listview.Items[i].SubItems[4].Text);
            }

            label.Text = "";
            label.Text = "R$ " + total.ToString("N2");
        }

        // Função de sair da página de adicionar os pedidos
        private void buttonOExit_Click(object sender, EventArgs e)
        {
            if (listViewOBebidaseComidas.SelectedItems.Count > 0 & textBoxOBCQuantidade.Text != "" || textBoxOBCObservacao.Text != "")
            {
                if (MessageBox.Show("Tem certeza que deseja sair? \nTodas as informações preechidas ou selecionadas seram perdidas!", "SAIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        // Função de receber o usuário do sistema
        public void setUsuario(string usuario)
        {
            labelUser.Text = usuario;
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### Pedidos de Bebidas e Comidas ################################################################################## //
        // ####################################################################################################################################################################################################### //
        // DataTables de pedidos de bebidas e comidas 
        readonly DataTable datatablePBC = new DataTable();
        // Setup ListView de Pedidos de Bebidas e comidas
        private void SetupListViewPedidosBebidaseComidas()
        {
            listViewOPBC.View = View.Details;
            listViewOPBC.FullRowSelect = true;
            listViewOPBC.MultiSelect = false;
            listViewOPBC.Columns.Add("Código", 100);
            listViewOPBC.Columns.Add("Produto", 100);
            listViewOPBC.Columns.Add("Valor", 100);
            listViewOPBC.Columns.Add("Quantidade", 100);
            listViewOPBC.Columns.Add("Total", 100);
            listViewOPBC.Columns.Add("Responsável", 100);
            listViewOPBC.Columns.Add("Observação", 100);

            //Listar
            ListarPedidosBebidaseComidas();
            TotaldaComanda(labelValorTotal, listViewOPBC);
        }

        // Função de listar os pedidos de bebidas e comidas
        private void ListarPedidosBebidaseComidas()
        {
            int[] items = { 0, 1, 2, 3, 4, 5, 6 };
            int codigodacomanda = Convert.ToInt32(labelComanda.Text);
            ListarConteudoListView_OR_ComboBox($"SELECT pbc.CodigodoPedidosBebidaseComidas, bc.Nome, bc.Preco, pbc.Qtd, (pbc.Qtd * bc.Preco) AS TOTAL_VALOR, pbc.Responsavel, pbc.Observacao FROM `PedidosBebidaseComidas` AS pbc INNER JOIN `BebidaseComidas` AS bc ON pbc.CodigodaBebidaseComidas = bc.CodigodaBebidaseComidas WHERE CodigodaComanda = {codigodacomanda} AND Status LIKE 'Em uso'", 1, listViewOPBC, null, null, connection, datatablePBC, items);
        }

        // Função de limpar formulário 
        private void LimparFormularioBC()
        {
            textBoxOBCQuantidade.Text = "";
            textBoxOBCObservacao.Text = "";
        }

        // Função que adicionar pedidos de comidas e bebidas na comanda
        private void buttonOBCAdd_Click(object sender, EventArgs e)
        {
            if (textBoxOBCQuantidade.Text != "" & listViewOBebidaseComidas.SelectedItems.Count > 0)
            {
                int codigodaComanda = Convert.ToInt16(labelComanda.Text);
                int codigodaBebidaseComidas = Convert.ToInt16(listViewOBebidaseComidas.SelectedItems[0].SubItems[0].Text);
                string data = Convert.ToString(DateTime.Now.Date);
                CadastrarRegistros($"INSERT INTO `PedidosBebidaseComidas`(CodigodaComanda,CodigodaBebidaseComidas,Status,Qtd,Data,Responsavel,Observacao)VALUES({codigodaComanda},{codigodaBebidaseComidas},'Em uso',{textBoxOBCQuantidade.Text},'{data}','{labelUser.Text}','{textBoxOBCObservacao.Text}')", 1, "Adicionado com sucesso", connection);
                ListarPedidosBebidaseComidas();
                LimparFormularioBC();
                TotaldaComanda(labelValorTotal, listViewOPBC);
                listViewOBebidaseComidas.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Selecione um item do cardapio!");
            }
        }

        // Função para deltar pedidos de bebidas e comidas 
        private void buttonOPBC_Click(object sender, EventArgs e)
        {
            if (listViewOPBC.SelectedItems.Count > 0)
            {
                DeletarRegistros($"DELETE FROM `PedidosBebidaseComidas` WHERE CodigodoPedidosBebidaseComidas = {Convert.ToInt16(listViewOPBC.SelectedItems[0].SubItems[0].Text)}", 1, "RETIRAR", "Deseja retirar o pedido da comanda?", "Pedido retirado com sucesso!", connection);
                ListarPedidosBebidaseComidas();
                TotaldaComanda(labelValorTotal, listViewOPBC);
            }
            else
            {
                MessageBox.Show("Selecione um pedido do cliente!");
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### Pedidos de Jogos ############################################################################################## //
        // ####################################################################################################################################################################################################### //
        // DataTables de pedidos de jogos 
        readonly DataTable datatablePJ = new DataTable();

        // Setup ListView de Pedidos de Jogos
        private void SetupListViewPedidosJogos()
        {
            listViewOPJ.View = View.Details;
            listViewOPJ.FullRowSelect = true;
            listViewOPJ.MultiSelect = false;
            listViewOPJ.Columns.Add("Código", 100);
            listViewOPJ.Columns.Add("Código do Jogo", 100);
            listViewOPJ.Columns.Add("Jogo", 100);
            listViewOPJ.Columns.Add("Responsável", 100);

            //Listar
            ListarPedidosJogos();
        }

        // Função de listar os pedidos de jogos
        private void ListarPedidosJogos()
        {
            int[] items = { 0, 1, 2, 3};
            ListarConteudoListView_OR_ComboBox($"SELECT pj.CodigodoPedidosJogos, j.CodigodoJogo, j.Nome, pj.Responsavel FROM `PedidosJogos` AS pj INNER JOIN `Jogos` AS j ON pj.CodigodoJogo = j.CodigodoJogo WHERE pj.CodigodaComanda LIKE {Convert.ToInt16(labelComanda.Text)} AND pj.Status LIKE 'Em uso'", 1, listViewOPJ, null, null, connection, datatablePJ, items);
        }

        // Função de adicionar pedidos de jogos na comanda
        private void buttonOJAdd_Click(object sender, EventArgs e)
        {
            if (listViewOJogos.SelectedItems.Count > 0)
            {
                int codigodaComanda = Convert.ToInt16(labelComanda.Text);
                int codigoJogo = Convert.ToInt16(listViewOJogos.SelectedItems[0].SubItems[0].Text);
                string data = Convert.ToString(DateTime.Now.Date);
                string status = $"{labelMesa.Text} / {codigodaComanda}";
                CadastrarRegistros($"INSERT INTO `PedidosJogos`(CodigodaComanda,CodigodoJogo,Status,Data,Responsavel)VALUES({codigodaComanda},{codigoJogo},'Em uso','{data}','{labelUser.Text}')", 1, "Adicionado com sucesso", connection);
                EditarRegistro($"UPDATE `Jogos` SET Status = 'Ocupado ({status})' WHERE CodigodoJogo = {codigoJogo}", 2, "", "", "", connection);
                ListarPedidosJogos();
                ListarOJogos();
            }
            else
            {
                MessageBox.Show("Selecione um jogo!");
            }
        }
        // DataTable de pedidos de jogos
        readonly DataTable dataTableDeletePJ = new DataTable();
        // Função para deletar pedidos de jogos
        private void buttonOPJ_Click(object sender, EventArgs e)
        {
            if (listViewOPJ.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Deseja retirar o jogo da comanda?", "RETIRAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DeletarRegistros($"DELETE FROM `PedidosJogos` WHERE CodigodoPedidosJogos = {Convert.ToInt16(listViewOPJ.SelectedItems[0].SubItems[0].Text)}", 2, "", "", "", connection);
                    EditarRegistro($"UPDATE `Jogos` SET Status = 'Livre' WHERE CodigodoJogo = {Convert.ToInt16(listViewOPJ.SelectedItems[0].SubItems[1].Text)}", 2, "", "", "", connection);
                    ListarPedidosJogos();
                    ListarOJogos();
                }
            }
            else
            {
                MessageBox.Show("Selecione um pedido de jogo!");
            }
        }
        // DataTables Fechamento
        DataTable dataTableOFechamentoComanda = new DataTable();

        // Função 
        public void FechaComanda()
        {
            if (listViewOPBC.Items.Count != 0)
            {
                if (MessageBox.Show("Certeza que deseja fecha a comanda ?", "FECHAR COMANDA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    EditarRegistro($"UPDATE `PedidosBebidaseComidas` SET Status = 'Finalizado' WHERE CodigodaComanda IN (SELECT CodigodaComanda FROM `PedidosBebidaseComidas` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)} AND Status = 'Em uso')", 2, "", "", "", connection);
                    Label labelMesa = new Label();
                    int[] items = { 0 };
                    ListarConteudoListView_OR_ComboBox($"SELECT CodigodaMesa FROM `Comandas` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", 3, null, null, labelMesa, connection, dataTableOFechamentoComanda, items);

                    EditarRegistro($"UPDATE `Comandas` SET CodigodaMesa = {0}, Status = 'Livre' WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)} ", 2, "", "", "", connection);

                    // Verificar quantidade de comandas relacionada com a mesa caso a mesa tive nenhuma comanda relacionada a mesa fica com status livre
                    if (CountRegistros($"SELECT COUNT(*) FROM `Comandas` WHERE CodigodaMesa = {Convert.ToInt16(labelMesa.Text)}", connection) == 0)
                    {
                        EditarRegistro($"UPDATE `Mesas` SET Status = 'Livre' WHERE CodigodaMesa = {Convert.ToInt16(labelMesa.Text)}", 2, "", "", "", connection);
                    }
                    else
                    {
                        //
                    }
                    if (CountRegistros($"SELECT COUNT(*) FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", connection) != 0)
                    {
                        EditarRegistro($"UPDATE `Jogos` SET Status = 'Livre' WHERE CodigodoJogo IN (SELECT CodigodoJogo FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)})", 2, "", "", "", connection);
                        DeletarRegistros($"DELETE FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", 2, "", "", "", connection);
                    }
                    MessageBox.Show("Fechamento efetuado com sucesso!");
                    if (MessageBox.Show("Deseja imprimir a nota fiscal?", "NOTA FISCAL", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        // Gerar a nota fiscal 
                        Document notaFiscal = new Document(PageSize.A4);
                        string notaFiscalValorTotal = "";
                        notaFiscalValorTotal += "\nAv. Pres. Kennedy - 1615 - Cidade Nova I - Indaiatuba (SP) - CEP: 13334-120";
                        notaFiscalValorTotal += "\nCNPJ: 11.222.333/0001-44\n\n";
                        notaFiscalValorTotal += $"\nNº da Comanda: {labelComanda.Text} | Nº da Mesa: {labelMesa.Text}\n";
                        notaFiscalValorTotal += "\nO Valor Total da Comanda: " + labelValorTotal.Text + "\n\n\n";
                        GerarNotaFiscal(notaFiscal, listViewOPBC, "Nota Fiscal", $"Nota Fiscal: Comanda {labelComanda.Text} | Mesa {labelMesa.Text}", notaFiscalValorTotal, 7);
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nenhum pedido na comanda!");
            }
        }

        // Função que fecha a comanda 
        private void buttonOExitComanda_Click(object sender, EventArgs e)
        {
            FechaComanda();
        }

        // Função retorna quantidade de registros de determinado SQL
        private int CountRegistros(string sql, OleDbConnection connection)
        {

            // Variáveis de configuração inicial
            OleDbCommand command;

            command = new OleDbCommand(sql, connection);
            connection.Open();
            Int32 count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return count;
        }

        // Função para cancelar a comanda
        private void buttonOCancelComanda_Click(object sender, EventArgs e)
        {
            CancelarComanda();
        }

        // Função de cancelar a comanda
        public void CancelarComanda()
        {
            if (MessageBox.Show("Certeza que deseja cancelar a comanda ?", "CANCELAR COMANDA", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                DeletarRegistros($"DELETE FROM `PedidosBebidaseComidas` WHERE Status LIKE 'Em uso' AND CodigodaComanda IN (SELECT CodigodaComanda FROM `PedidosBebidaseComidas` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)})", 2, "", "", "", connection);
                Label labelMesa = new Label();
                int[] items = { 0 };
                ListarConteudoListView_OR_ComboBox($"SELECT CodigodaMesa FROM `Comandas` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", 3, null, null, labelMesa, connection, dataTableOFechamentoComanda, items);

                EditarRegistro($"UPDATE `Comandas` SET CodigodaMesa = {0}, Status = 'Livre' WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)} ", 2, "", "", "", connection);

                // Verificar quantidade de comandas relacionada com a mesa caso a mesa tive nenhuma comanda relacionada a mesa fica com status livre
                if (CountRegistros($"SELECT COUNT(*) FROM `Comandas` WHERE CodigodaMesa = {Convert.ToInt16(labelMesa.Text)}", connection) == 0)
                {
                    EditarRegistro($"UPDATE `Mesas` SET Status = 'Livre' WHERE CodigodaMesa = {Convert.ToInt16(labelMesa.Text)}", 2, "", "", "", connection);
                }
                else
                {
                    //
                }
                if (CountRegistros($"SELECT COUNT(*) FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", connection) != 0)
                {
                    EditarRegistro($"UPDATE `Jogos` SET Status = 'Livre' WHERE CodigodoJogo IN (SELECT CodigodoJogo FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)})", 2, "", "", "", connection);
                    DeletarRegistros($"DELETE FROM `PedidosJogos` WHERE CodigodaComanda = {Convert.ToInt16(labelComanda.Text)}", 2, "", "", "", connection);
                }
                MessageBox.Show("Comanda cancelada com sucesso!");
                this.Close();
            }
        }

        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
    }

}
