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

// Desativar botão fechar
using System.Runtime.InteropServices;

namespace PI___Butecus_Sta_Tereza
{
    public partial class MenuSystem : Form
    {
        // Conexão com banco de dados
        private static string localConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/PI - Banco de Dados.mdb;";
        readonly OleDbConnection connection = new OleDbConnection(Convert.ToString(localConnection));

        public MenuSystem()
        {
            InitializeComponent();
            mostarAjuda();

            // Gerar Mesas e Comandas
            CreateMesas();
            CreateComandas();

            ViewStatus();

            // Setups
            SetupListViewJogos();
            SetupListViewBebidaseComidas();
            SetupListViewResevas();
            SetupListViewMesas();
            SetupListViewComandas();
            SetupListViewMComandas();
            SetupListViewConfCategorias();
        }

        // ####################################################################################################################################################################################################### //
        // ####################################################################################### CONFIGURAÇÕES ######################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // Datatable de conf
        readonly DataTable dataTableConfCategorias = new DataTable();
        // Setup de categorias de conf
        private void SetupListViewConfCategorias()
        {
            listViewConfCategorias.View = View.Details;
            listViewConfCategorias.FullRowSelect = true;
            listViewConfCategorias.MultiSelect = true;
            listViewConfCategorias.Columns.Add("Código", 100);
            listViewConfCategorias.Columns.Add("Nome", 100);
            listViewConfCategorias.Columns.Add("Tipo", 100);

            ButtonsEditCategorias(true, false, false, true, true, true, true);

            // Listar
            ListarConfCategorias();
        }
        //Função para buttons edição visibilidade
        private void ButtonsEditCategorias(bool cadastro, bool save, bool cancel, bool delete, bool editar, bool list, bool search)
        {
            buttonConfCadastro.Visible = cadastro;
            buttonConfSave.Visible = save;
            buttonConfCancel.Visible = cancel;
            buttonConfDelete.Enabled = delete;
            buttonConfEditar.Enabled = editar;
            listViewConfCategorias.Enabled = list;
            buttonConfSearch.Enabled = search;
        }
        //Função para editar os dados de categorias
        private void buttonConfEditar_Click(object sender, EventArgs e)
        {
            if (listViewConfCategorias.SelectedItems.Count > 0)
            {
                if (CountRegistros($"SELECT COUNT(*) FROM `Jogos` WHERE Categoria = '{listViewConfCategorias.SelectedItems[0].SubItems[1].Text}'", connection) == 0 & CountRegistros($"SELECT COUNT(*) FROM `BebidaseComidas` WHERE Categoria = '{listViewConfCategorias.SelectedItems[0].SubItems[1].Text}'", connection) == 0)
                {
                    int codigoCategoria = Convert.ToInt16(listViewConfCategorias.SelectedItems[0].SubItems[0].Text);
                    textBoxConfCategoria.Text = listViewConfCategorias.SelectedItems[0].SubItems[1].Text;
                    comboBoxConfTipo.Text = listViewConfCategorias.SelectedItems[0].SubItems[2].Text;
                    ButtonsEditCategorias(false, true, true, false, false, false, false);
                }
                else
                {
                    MessageBox.Show("item está sendo utilizado!");
                }
            }
            else
            {
                MessageBox.Show("Selecione uma categoria!");
            }
        }
        // Função para cancelar a edição de categorias
        private void buttonConfCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar a edição? \nTodas as informações alteradas seram perdidas!", "CANCELAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ButtonsEditCategorias(true, false, false, true, true, true, true);
                listViewConfCategorias.SelectedItems[0].Selected = false;
                LimparFormulatioCategorias();
            }
        }
        // Função de salvar os dados alterados de categorias
        private void buttonConfSave_Click(object sender, EventArgs e)
        {
            if (textBoxConfCategoria.Text == "" || comboBoxConfTipo.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                EditarRegistro($"UPDATE `Categorias` SET Nome = '{textBoxConfCategoria.Text}', Tipo = '{comboBoxConfTipo.Text}' WHERE CodigodeCategoria = {Convert.ToInt16(listViewConfCategorias.SelectedItems[0].SubItems[0].Text)} ", 1, "Deseja salvar as alterações?", "EDIÇÃO", "Alterações efetuadas com sucesso!", connection);
                ButtonsEditCategorias(true, false, false, true, true, true, true);
                LimparFormulatioCategorias();
                ListarConfCategorias();
                ViewStatus();
            }
        }
        // Função de listar as categorias de conf
        private void ListarConfCategorias()
        {
            int[] items = { 0, 1, 2 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Categorias`", 1, listViewConfCategorias, null, null, connection, dataTableConfCategorias, items);
        }
        // Função para deletar as categorias
        private void buttonConfDelete_Click(object sender, EventArgs e)
        {
            if (listViewConfCategorias.SelectedIndices.Count > 0)
            {
                if (CountRegistros($"SELECT COUNT(*) FROM `Jogos` WHERE Categoria = '{listViewConfCategorias.SelectedItems[0].SubItems[1].Text}'", connection) == 0 & CountRegistros($"SELECT COUNT(*) FROM `BebidaseComidas` WHERE Categoria = '{listViewConfCategorias.SelectedItems[0].SubItems[1].Text}'", connection) == 0)
                {
                    DeletarRegistros($"DELETE FROM `Categorias` WHERE CodigodeCategoria = {Convert.ToInt16(listViewConfCategorias.SelectedItems[0].SubItems[0].Text)}", "DELETAR", "Você tem certeza que deseja deletar essa categoria ?", "Deletado com Sucesso!", connection);
                    ListarCategoriasBebidaseComidas();
                    ListarCategoriaJogos();
                    ListarConfCategorias();
                    ViewStatus();
                }
                else
                {
                    MessageBox.Show("item está sendo utilizado!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um jogo!");
            }
        }

        // Função para pesquisar as categorias
        private void buttonConfSearch_Click(object sender, EventArgs e)
        {
            if (textBoxConfSearch.Text == "")
            {
                ListarConfCategorias();
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `Categorias` WHERE Nome LIKE '{textBoxConfSearch.Text}' OR Tipo LIKE '{textBoxConfSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewConfCategorias, null, null, connection, dataTableConfCategorias, items);
            }
        }

        // Função para limpar as categorias de conf
        private void LimparFormulatioCategorias()
        {
            textBoxConfCategoria.Text = "";
            comboBoxConfTipo.Text = "";
        }

        // Função para cadastrar as categorias de conf
        private void buttonConfCadastro_Click(object sender, EventArgs e)
        {
            if (textBoxConfCategoria.Text == "" || comboBoxConfTipo.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                string sql = $"INSERT INTO `Categorias`(Nome, Tipo)VALUES('{textBoxConfCategoria.Text}','{comboBoxConfTipo.Text}')";
                CadastrarRegistros(sql, 1, "Cadastrado com sucesso!", connection);
                ListarCategoriasBebidaseComidas();
                ListarCategoriaJogos();
                ListarConfCategorias();
                ViewStatus();
                LimparFormulatioCategorias();
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### RELATÓRIOS #################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // Função que set o usuário
        public void UserLogin(string user)
        {
            labelUsername.Text = user;
        }

        // Função status do sistema
        public void ViewStatus()
        {
            // Count - Mesas
            int mesasTotal = CountRegistros("SELECT COUNT (*) FROM `Mesas`", connection);
            int mesasLivres = CountRegistros("SELECT COUNT (*) FROM `Mesas` WHERE Status LIKE 'Livre'", connection);
            int mesasReservado = CountRegistros("SELECT COUNT (*) FROM `Mesas` WHERE Status LIKE 'Reservado'", connection);
            int mesasOcupado = CountRegistros("SELECT COUNT (*) FROM `Mesas` WHERE Status LIKE 'Ocupado'", connection);
            labelViewCountMesas.Text = $"Quantidade de mesas em uso: {mesasOcupado} | {mesasTotal}";
            // Count - Comandas
            int comandaTotal = CountRegistros("SELECT COUNT (*) FROM `Comandas`", connection);
            int comandaLivres = CountRegistros("SELECT COUNT (*) FROM `Comandas` WHERE Status LIKE 'Livre'", connection);
            int comandaOcupado = CountRegistros("SELECT COUNT (*) FROM `Comandas` WHERE Status LIKE 'Ocupado'", connection);
            labelViewCountComandas.Text = $"Quantidade de comandas em uso: {comandaOcupado} | {comandaTotal}";
            // Count - Jogos
            int jogoTotal = CountRegistros("SELECT COUNT (*) FROM `Jogos`", connection);
            int jogoLivres = CountRegistros("SELECT COUNT (*) FROM `Jogos` WHERE Status LIKE 'Livre'", connection);
            int jogoOcupado = CountRegistros("SELECT COUNT (*) FROM `Jogos` WHERE Status LIKE 'Ocupado'", connection);
            labelViewCountJogos.Text = $"Quantidade de jogos em uso: {jogoOcupado} | {jogoTotal}";
            // Count - Bebidas e Comidas
            int bcTotal = CountRegistros("SELECT COUNT (*) FROM `BebidaseComidas`", connection);

            // RELATÓRIO - Status
            // Mesas
            labelRCountMesasLivres.Text = $"Quantidade de mesas livres: {Convert.ToString(mesasLivres)}";
            labelRCountMesasOcupadas.Text = $"Quantidade de mesas ocupadas: {Convert.ToString(mesasOcupado)}";
            labelRCountMesasReservadas.Text = $"Quantidade de mesas reservadas: {Convert.ToString(mesasReservado)}";
            labelRCountMesasTotal.Text = $"Quantidade total de mesas: {Convert.ToString(mesasTotal)}";
            // Comandas
            labelRCountComandasDisponivel.Text = $"Quantidade de comandas livres: {Convert.ToString(comandaLivres)}";
            labelRCountComandasUso.Text = $"Quantidade de comandas ocupadas: {Convert.ToString(comandaOcupado)}";
            labelRCountComandasTotal.Text = $"Quantidade total de comandas: {Convert.ToString(comandaTotal)}";
            // Bebidas e Comidas
            labelRCountBebidaseComidasTotal.Text = $"Quantidade total de Bebidas e Comidas cadastradas: {Convert.ToString(bcTotal)}";
            // Jogos
            labelRCountJogosCadastrados.Text = $"Quantidade total de jogos cadastrados: {Convert.ToString(jogoTotal)}";


        }
        // Função que gerar o pdf específico automatico
        private void buttonGerarPDFE_Click(object sender, EventArgs e)
        {
            if (comboBoxRelatorioE.Text != "")
            {
                if (comboBoxRelatorioE.Text == "Reservas")
                {
                    Document reservaDOC = new Document(PageSize.A4);
                    string paragraph = "\n" + labelRCountMesasReservadas.Text + " \n " + labelRCountMesasTotal.Text + "\n\n";
                    GerarPDF(reservaDOC, 1, listViewReservas, "Reservas", "Relatório de Reservas", paragraph, 7);
                }
                else if (comboBoxRelatorioE.Text == "Bebidas e Comidas")
                {
                    Document bebidasecomidasDOC = new Document(PageSize.A4);
                    string paragraph = "\n" + labelRCountBebidaseComidasTotal.Text + "\n\n";
                    GerarPDF(bebidasecomidasDOC, 1, listViewBebidaseComidas, "Bebidas e Comidas", "Relatório de Bebidas e Comidas", paragraph, 5);
                }
                else
                {
                    Document jogos = new Document(PageSize.A4);
                    string paragraph = "\n"+ labelRCountJogosCadastrados.Text + "\n\n";

                    GerarPDF(jogos, 1, listViewJogos, "Jogos", "Relatório de Jogos", paragraph, 5);
                }
            }
            else
            {
                MessageBox.Show("Selecione os campos obrigatórios!");
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //




        // ####################################################################################################################################################################################################### //
        // ####################################################################################### JOGOS ######################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // DataTables do ListView
        readonly DataTable dataTableListViewJogos = new DataTable();

        // DataTables do ComboBox
        readonly DataTable dataTableComboBoxJogos1 = new DataTable();
        readonly DataTable dataTableComboBoxJogos2 = new DataTable();

        // Setup do ListViewJogos
        public void SetupListViewJogos()
        {
            listViewJogos.View = View.Details;
            listViewJogos.FullRowSelect = true;
            listViewJogos.MultiSelect = false;
            listViewJogos.Columns.Add("Código", 100);
            listViewJogos.Columns.Add("Nome", 100);
            listViewJogos.Columns.Add("Status", 100);
            listViewJogos.Columns.Add("Categoria", 100);
            listViewJogos.Columns.Add("Descrição", 100);
            ButtonsEditJogos(true, false, false, true, true, true, true, true, true);

            //Listar contéudo do database
            ListarJogos();
            ListarCategoriaJogos();
        }

        // Validar campo código de jogos
        private void textBoxJCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarCampoNumeros(sender, e);
        }

        // Button de Edit Jogos
        private void ButtonsEditJogos(bool buttonSave, bool SaveEdit, bool CancelEdit, bool codigoJogo, bool buttonEdit, bool buttonDelete, bool listView, bool search, bool comboBox)
        {
            buttonJCadastrar.Visible = buttonSave;
            buttonJSaveEdit.Visible = SaveEdit;
            buttonJCancelEdit.Visible = CancelEdit;
            textBoxJCodigo.Enabled = codigoJogo;
            buttonJEdit.Enabled = buttonEdit;
            buttonJDelete.Enabled = buttonDelete;
            listViewJogos.Enabled = listView;
            buttonJSeach.Enabled = search;
            comboBoxJogos.Enabled = comboBox;
        }

        // Listar os jogos no ListViewJogos
        private void ListarJogos()
        {
            int[] items = { 0, 1, 2, 3, 4 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Jogos`", 1, listViewJogos, null, null, connection, dataTableListViewJogos, items);
        }

        // Listar as categoria de jogos
        private void ListarCategoriaJogos()
        {
            comboBoxJCategoria.Items.Clear();
            int[] items = { 1 };
            string sql = "SELECT * FROM `Categorias` WHERE Tipo LIKE 'Jogos'";
            comboBoxJogos.SelectedIndex = 0;
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxJCategoria, null, connection, dataTableComboBoxJogos1, items);
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxJogos, null, connection, dataTableComboBoxJogos1, items);
        }

        // Search do ListViewJogos
        private void buttonJSeach_Click(object sender, EventArgs e)
        {
            comboBoxJogos.SelectedIndex = 0;
            if (textBoxJSearch.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `Jogos` WHERE CodigodoJogo LIKE '{textBoxJSearch.Text}' OR Nome LIKE '{textBoxJSearch.Text}' OR Status LIKE '{textBoxJSearch.Text}' OR Categoria LIKE '{textBoxJSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewJogos, null, null, connection, dataTableListViewJogos, items);
            }
        }

        // Listar o ListViewJogos de acordo com categoria selecionada
        private void comboBoxJogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxJSearch.Text = "";
            if (comboBoxJogos.Text == "Todos")
            {
                ListarJogos();
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `Jogos` WHERE Categoria LIKE '{comboBoxJogos.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewJogos, null, null, connection, dataTableListViewJogos, items);
            }
        }

        // Limpar formulário de jogos
        private void LimparFormularioJogos()
        {
            textBoxJCodigo.Text = "";
            textBoxJNome.Text = "";
            textBoxJDescricao.Text = "";
        }

        // Função para inserir registros de jogos
        private void buttonJCadastrar_Click(object sender, EventArgs e)
        {
            if (textBoxJCodigo.Text == "" || textBoxJNome.Text == "" || comboBoxJCategoria.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                string sql = $"INSERT INTO `Jogos`(CodigodoJogo, Nome, Status, Categoria, Descricao)VALUES('{Convert.ToInt32(textBoxJCodigo.Text)}', '{textBoxJNome.Text}', 'Livre', '{comboBoxJCategoria.Text}', '{textBoxJDescricao.Text}')";
                CadastrarRegistros(sql, 1, "Cadastrado com sucesso!", connection);
                LimparFormularioJogos();
                ListarJogos();
                ViewStatus();
            }
        }

        // Função de deletar os registro de jogos
        private void buttonJDelete_Click(object sender, EventArgs e)
        {
            if (listViewJogos.SelectedIndices.Count > 0)
            {
                if (listViewJogos.SelectedItems[0].SubItems[2].Text == "Livre")
                {
                    String selectedJogo = listViewJogos.SelectedItems[0].SubItems[0].Text;
                    int codigoJogo = Convert.ToInt32(selectedJogo);
                    DeletarRegistros($"DELETE FROM `Jogos` WHERE CodigodoJogo = {codigoJogo}", "DELETAR", "Você tem certeza que deseja deletar esse jogo ?", "Deletado com Sucesso!", connection);
                    ListarJogos();
                    ViewStatus();
                }
                else
                {
                    MessageBox.Show("Jogos está sendo utilizado!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um jogo!");
            }
        }

        // Função selecionar o jogos que deseja editar
        private void buttonJEdit_Click(object sender, EventArgs e)
        {
            if (listViewJogos.SelectedIndices.Count > 0)
            {
                if (listViewJogos.SelectedItems[0].SubItems[2].Text == "Livre")
                {
                    textBoxJCodigo.Text = listViewJogos.SelectedItems[0].SubItems[0].Text;
                    textBoxJNome.Text = listViewJogos.SelectedItems[0].SubItems[1].Text;
                    comboBoxJCategoria.Text = listViewJogos.SelectedItems[0].SubItems[3].Text;
                    textBoxJDescricao.Text = listViewJogos.SelectedItems[0].SubItems[4].Text;
                    ButtonsEditJogos(false, true, true, false, false, false, false, false, false);
                }
                else
                {
                    MessageBox.Show("Jogos está sendo utilizado!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um jogo!");
            }
        }

        // Função de cancelar edição do jogo
        private void buttonJCancelEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar a edição? \nTodas as informações alteradas seram perdidas!", "CANCELAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                LimparFormularioJogos();
                listViewJogos.SelectedItems[0].Selected = false;
                ButtonsEditJogos(true, false, false, true, true, true, true, true, true);
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### BEBIDAS E COMIDAS ############################################################################################# //
        // ####################################################################################################################################################################################################### //
        // DataTables dos ListViews
        readonly DataTable dataTableListViewBebidaseComidas = new DataTable();

        // DataTables dos ComboBox
        readonly DataTable dataTableComboBoxBebidaseComidas1 = new DataTable();
        readonly DataTable dataTableComboBoxBebidaseComidas2 = new DataTable();

        // Setup do ListViewBebidaseComidas
        public void SetupListViewBebidaseComidas()
        {
            listViewBebidaseComidas.View = View.Details;
            listViewBebidaseComidas.FullRowSelect = true;
            listViewBebidaseComidas.MultiSelect = false;
            listViewBebidaseComidas.Columns.Add("Código", 100);
            listViewBebidaseComidas.Columns.Add("Nome", 100);
            listViewBebidaseComidas.Columns.Add("Preço", 100);
            listViewBebidaseComidas.Columns.Add("Categoria", 100);
            listViewBebidaseComidas.Columns.Add("Descrição", 100);
            ButtonsEditBebidaseComidas(true, false, false, true, true, true, true, true, true);

            //Listar contéudo do database
            ListarBebidaseComidas();
            ListarCategoriasBebidaseComidas();
        }

        // Validar campo código de bebidas e comidas
        private void textBoxBCCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarCampoNumeros(sender, e);
        }

        // Validação do campo de preço
        private void textBoxBCPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (textBoxBCPreco.Text.Contains(","))
                {
                    e.Handled = true; // Caso exista, aborte 
                }
            }

            //aceita apenas números, tecla backspace.
            else if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        // Função de salvar os dados alterados de Bebidas e Comidas 
        private void buttonBCSaveEdit_Click(object sender, EventArgs e)
        {
            if (textBoxBCCodigo.Text == "" || textBoxBCNome.Text == "" || textBoxBCPreco.Text == "" || comboBoxBCCategoria.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                EditarRegistro($"UPDATE `BebidaseComidas` SET Nome = '{textBoxBCNome.Text}', Preco = '{textBoxBCPreco.Text}', Categoria = '{comboBoxBCCategoria.Text}', Descricao = '{textBoxBCDescricao.Text}' WHERE CodigodaBebidaseComidas = {textBoxBCCodigo.Text} ", 1, "Certeza que deseja salvar as alterações?", "EDIÇÃO", "Alterações efetuadas com sucesso!", connection);
                ButtonsEditBebidaseComidas(true, false, false, true, true, true, true, true, true);
                LimparFormularioBebidasecomidas();
                ListarBebidaseComidas();
                ViewStatus();
            }
        }

        // Buttons Edit Bebidas e Comidas
        private void ButtonsEditBebidaseComidas(bool buttonSave, bool SaveEdit, bool CancelEdit, bool codigoJogo, bool buttonEdit, bool buttonDelete, bool listView, bool buttonSearch, bool comboBox)
        {
            buttonBCCadastrar.Visible = buttonSave;
            buttonBCSaveEdit.Visible = SaveEdit;
            buttonBCCancelEdit.Visible = CancelEdit;
            textBoxBCCodigo.Enabled = codigoJogo;
            buttonBCEdit.Enabled = buttonEdit;
            buttonBCDelete.Enabled = buttonDelete;
            listViewBebidaseComidas.Enabled = listView;
            buttonBCSearch.Enabled = buttonSearch;
            comboBoxBebidaseComidas.Enabled = comboBox;
        }

        // Listar as bebidas e comidas no ListViewBebidaseComidas
        private void ListarBebidaseComidas()
        {
            int[] items = { 0, 1, 2, 3, 4 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `BebidaseComidas`", 1, listViewBebidaseComidas, null, null, connection, dataTableListViewBebidaseComidas, items);
        }

        // Listar as categorias de bebidas e comidas
        private void ListarCategoriasBebidaseComidas()
        {
            comboBoxBCCategoria.Items.Clear();
            int[] items = { 1 };
            string sql = "SELECT * FROM `Categorias` WHERE Tipo LIKE 'Cardapio'";
            comboBoxBebidaseComidas.SelectedIndex = 0;
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxBCCategoria, null, connection, dataTableComboBoxBebidaseComidas1, items);
            ListarConteudoListView_OR_ComboBox(sql, 2, null, comboBoxBebidaseComidas, null, connection, dataTableComboBoxBebidaseComidas2, items);
        }

        // Search do ListViewBebidaseComidas
        private void buttonBCSearch_Click(object sender, EventArgs e)
        {
            comboBoxBebidaseComidas.SelectedIndex = 0;
            if (textBoxBCSearch.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `BebidaseComidas` WHERE CodigodaBebidaseComidas LIKE '{textBoxBCSearch.Text}' OR Nome LIKE '{textBoxBCSearch.Text}' OR Preco LIKE '{textBoxBCSearch.Text}' OR Categoria LIKE '{textBoxBCSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewBebidaseComidas, null, null, connection, dataTableListViewBebidaseComidas, items);
            }
        }

        // Listar o ListViewBebidaseComidas de acordo com categoria selecionada
        private void comboBoxBebidaseComidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxBCSearch.Text = "";
            if (comboBoxBebidaseComidas.Text == "Todos")
            {
                ListarBebidaseComidas();
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `BebidaseComidas` WHERE Categoria LIKE '{comboBoxBebidaseComidas.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewBebidaseComidas, null, null, connection, dataTableListViewBebidaseComidas, items);
            }
        }

        // Função de limpar formulário de bebidas e comidas
        private void LimparFormularioBebidasecomidas()
        {
            textBoxBCCodigo.Text = "";
            textBoxBCNome.Text = "";
            textBoxBCPreco.Text = "";
            textBoxBCDescricao.Text = "";
        }

        // Função de inserir registro de bebidas e comidas
        private void buttonBCCadastrar_Click_1(object sender, EventArgs e)
        {
            if (textBoxBCCodigo.Text == "" || textBoxBCNome.Text == "" || textBoxBCPreco.Text == "" || comboBoxBCCategoria.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                string sql = $"INSERT INTO `BebidaseComidas`(CodigodaBebidaseComidas, Nome, Preco, Categoria, Descricao)VALUES('{Convert.ToInt32(textBoxBCCodigo.Text)}', '{textBoxBCNome.Text}', '{textBoxBCPreco.Text}', '{comboBoxBCCategoria.Text}', '{textBoxBCDescricao.Text}')";
                CadastrarRegistros(sql, 1, "Cadastrado com sucesso!", connection);
                LimparFormularioBebidasecomidas();
                ListarBebidaseComidas();
                ViewStatus();
            }
        }

        // Função de deletar os registros de Bebidas e Comidas
        private void buttonBCDelete_Click(object sender, EventArgs e)
        {
            if (listViewBebidaseComidas.SelectedIndices.Count > 0)
            {
                if (CountRegistros($"SELECT * FROM `PedidosBebidaseComidas` WHERE CodigodaBebidaseComidas = {Convert.ToInt32(listViewBebidaseComidas.SelectedItems[0].SubItems[0].Text)} AND Status = 'Em uso'", connection) == 0)
                {
                    String selectedBebidaseComidas = listViewBebidaseComidas.SelectedItems[0].SubItems[0].Text;
                    int codigoBebidaseComidas = Convert.ToInt32(selectedBebidaseComidas);
                    DeletarRegistros($"DELETE FROM `BebidaseComidas` WHERE CodigodaBebidaseComidas = {codigoBebidaseComidas}", "DELETAR", "Você tem certeza que deseja deletar esse item do cardápio ?", "Deletado com Sucesso!", connection);
                    ListarBebidaseComidas();
                    ViewStatus();
                }
                else
                {
                    MessageBox.Show("Item sendo utilizado no momento!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um item do cardápio!");
            }
        }

        // Função que selecionar bebidas e comidas que deseja editar
        private void buttonBCEdit_Click(object sender, EventArgs e)
        {
            if (listViewBebidaseComidas.SelectedIndices.Count > 0)
            {
                if (CountRegistros($"SELECT * FROM `PedidosBebidaseComidas` WHERE CodigodaBebidaseComidas = {Convert.ToInt16(listViewBebidaseComidas.SelectedItems[0].SubItems[0].Text)} AND Status = 'Em uso'", connection) == 0)
                {
                    textBoxBCCodigo.Text = listViewBebidaseComidas.SelectedItems[0].SubItems[0].Text;
                    textBoxBCNome.Text = listViewBebidaseComidas.SelectedItems[0].SubItems[1].Text;
                    textBoxBCPreco.Text = listViewBebidaseComidas.SelectedItems[0].SubItems[2].Text;
                    comboBoxBCCategoria.Text = listViewBebidaseComidas.SelectedItems[0].SubItems[3].Text;
                    textBoxBCDescricao.Text = listViewBebidaseComidas.SelectedItems[0].SubItems[4].Text;
                    ButtonsEditBebidaseComidas(false, true, true, false, false, false, false, false, false);
                }
                else
                {
                    MessageBox.Show("Item sendo utilizado no momento!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um item do cardápio!");
            }
        }

        // Função para cancelar a edição de bebidas e comidas 
        private void buttonBCCancelEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar a edição? \nTodas as informações alteradas seram perdidas!", "CANCELAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                LimparFormularioBebidasecomidas();
                listViewBebidaseComidas.SelectedItems[0].Selected = false;
                ButtonsEditBebidaseComidas(true, false, false, true, true, true, true, true, true);
            }
        }

        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### RESERVAS ###################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // DataTables dos ListViews
        readonly DataTable dataTableListViewReservas = new DataTable();

        // DataTables dos ComboBox
        readonly DataTable dataTableComboBoxMesasLivres = new DataTable();

        // Setup do ListViewResevas
        public void SetupListViewResevas()
        {
            listViewReservas.View = View.Details;
            listViewReservas.FullRowSelect = true;
            listViewReservas.MultiSelect = false;
            listViewReservas.Columns.Add("Código", 100);
            listViewReservas.Columns.Add("Mesa", 100);
            listViewReservas.Columns.Add("Cliente", 100);
            listViewReservas.Columns.Add("Data/Hora", 150);
            listViewReservas.Columns.Add("Qtd. pessoas", 100);
            listViewReservas.Columns.Add("Observação", 100);
            listViewReservas.Columns.Add("Responsável", 100);
            dateTimePickerRData.Text = DateTime.Now.ToString();
            ButtonsEditReservas(true, false, false, true, true, true, true, true);

            //Listar contéudo do database
            ListarReservas();
            ListarMesasDisponiveis();
        }

        // Validação do campo quantidade de pessoas
        private void textBoxRQtdPessoas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        // Função para pesquisar reservas
        private void buttonRSearchReservas_Click(object sender, EventArgs e)
        {
            if (textBoxRSearchReservas.Text == "")
            {
                ListarReservas();
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1, 2, 3, 4 };
                string sql = $"SELECT * FROM `Reservas` WHERE CodigodaReserva LIKE '{textBoxRSearchReservas.Text}' OR CodigodaMesa LIKE '{textBoxRSearchReservas.Text}' OR Cliente LIKE '{textBoxRSearchReservas.Text}' OR Data LIKE '{textBoxRSearchReservas.Text}' OR QtdPessoas LIKE {textBoxRSearchReservas.Text} OR Responsavel LIKE {textBoxRSearchReservas.Text}";
                ListarConteudoListView_OR_ComboBox("SELECT * FROM `Reservas`", 1, listViewReservas, null, null, connection, dataTableListViewReservas, items);
            }
        }

        // Função de salvar os dados alterados de reserva
        private void buttonRSaveEdit_Click(object sender, EventArgs e)
        {
            if (comboBoxRMesa.Text == "" || textBoxRCliente.Text == "" || dateTimePickerRData.Text == "" || textBoxRQtdPessoas.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                if (listViewReservas.SelectedItems[0].SubItems[1].Text == comboBoxRMesa.Text)
                {
                    int codigoReserva = Convert.ToInt16(listViewReservas.SelectedItems[0].SubItems[0].Text);
                    EditarRegistro($"UPDATE `Reservas` SET  CodigodaMesa = {Convert.ToInt16(comboBoxRMesa.Text)}, Cliente = '{textBoxRCliente.Text}', Data = '{dateTimePickerRData.Text}', QtdPessoas = '{textBoxRQtdPessoas.Text}', Observacao = '{textBoxRObservacao.Text}' WHERE CodigodaReserva = {codigoReserva}", 1, "Certeza que deseja salvar as alterações?", "EDIÇÃO", "Alterações efetuadas com sucesso!", connection);
                }
                else
                {
                    int codigoReserva = Convert.ToInt16(listViewReservas.SelectedItems[0].SubItems[0].Text);
                    EditarRegistro($"UPDATE `Reservas` SET  CodigodaMesa = {Convert.ToInt16(comboBoxRMesa.Text)}, Cliente = '{textBoxRCliente.Text}', Data = '{dateTimePickerRData.Text}', QtdPessoas = '{textBoxRQtdPessoas.Text}', Observacao = '{textBoxRObservacao.Text}' WHERE CodigodaReserva = {codigoReserva}", 1, "Certeza que deseja salvar as alterações?", "ALTERAR", "Alterações efetuadas com sucesso!", connection);
                    EditarRegistro($"UPDATE `Mesas` SET Status = 'Livre' WHERE CodigodaMesa = {Convert.ToInt32(listViewReservas.SelectedItems[0].SubItems[1].Text)}", 2, "", "", "", connection);
                    EditarRegistro($"UPDATE `Mesas` SET Status = 'Reservado' WHERE CodigodaMesa = {Convert.ToInt32(comboBoxRMesa.Text)}", 2, "", "", "", connection);
                }
                ButtonsEditReservas(true, false, false, true, true, true, true, true);
                comboBoxRMesa.Items.Clear();
                LimparFormularioReservas();
                ListarMesasDisponiveis();
                ListarReservas();
                ViewStatus();

                // Mesa
                ListarMesas();
            }
        }

        // Buttons Edit de reservas 
        private void ButtonsEditReservas(bool buttonSave, bool SaveEdit, bool CancelEdit, bool buttonREdit, bool fecharReserva, bool buttonDelete, bool listView, bool buttonSearch)
        {
            buttonReservar.Visible = buttonSave;
            buttonRSaveEdit.Visible = SaveEdit;
            buttonRCancelEdit.Visible = CancelEdit;
            buttonREdit2.Enabled = buttonREdit;
            buttonRFechaReserva.Enabled = fecharReserva;
            buttonRDelete.Enabled = buttonDelete;
            listViewReservas.Enabled = listView;
            buttonRSearchReservas.Enabled = buttonSearch;
        }

        // Listar as reservas no ListViewReservas
        private void ListarReservas()
        {
            int[] items = { 0, 1, 2, 3, 4, 5, 6 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Reservas`", 1, listViewReservas, null, null, connection, dataTableListViewReservas, items);
        }

        // Listar as mesas disponivel para reserva
        private void ListarMesasDisponiveis()
        {
            int[] items = { 0 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Mesas` WHERE Status LIKE 'Livre' ORDER BY CodigodaMesa ASC", 2, null, comboBoxRMesa, null, connection, dataTableComboBoxMesasLivres, items);

            if (comboBoxRMesa.Items.Count > 0)
            {
                comboBoxRMesa.SelectedIndex = 0;
            }
        }
        // Função para deletar e fechar reserva 

        // Função de deletar os registros de Reservas
        private void button1_Click(object sender, EventArgs e)
        {
            if (listViewReservas.SelectedIndices.Count > 0)
            {
                String selectedReservas = listViewReservas.SelectedItems[0].SubItems[0].Text;
                int codigoReservas = Convert.ToInt32(selectedReservas);
                // !!!!!!!!!!!!!!!!!!!!!!!!!! Verificar logica
                DeletarRegistros($"DELETE FROM `Reservas` WHERE CodigodaReserva = {codigoReservas}", "DELETAR", "Você tem certeza que deseja deletar essa reserva ?", "Deletado com Sucesso!", connection);
                string sqlUpdate = $"UPDATE `Mesas` SET Status = 'Livre' WHERE CodigodaMesa = {Convert.ToInt32(listViewReservas.SelectedItems[0].SubItems[1].Text)}";
                EditarRegistro(sqlUpdate, 2, "", "", "", connection);
                comboBoxRMesa.Items.Clear();
                ListarMesasDisponiveis();
                ListarReservas();
                ViewStatus();

                // Mesa
                ListarMesas();
            }
            else
            {
                MessageBox.Show("Selecione uma reserva!");
            }
        }

        // Função que limpar o formulário de reservas
        private void LimparFormularioReservas()
        {
            comboBoxRMesa.Text = "";
            textBoxRCliente.Text = "";
            dateTimePickerRData.Text = "";
            textBoxRQtdPessoas.Text = "";
            textBoxRObservacao.Text = "";
        }

        // Função de inserir registros de reservas 
        private void buttonReservar_Click(object sender, EventArgs e)
        {
            if (comboBoxRMesa.Text == "" || textBoxRCliente.Text == "" || dateTimePickerRData.Text == "" || textBoxRQtdPessoas.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
               // string sql = $"INSERT INTO `Reservas` (CodigodaMesa, Cliente, Data, QtdPessoas, Observacao)VALUES()";
                string sqlInsert = $"INSERT INTO `Reservas`(CodigodaMesa, Cliente, Data, QtdPessoas, Observacao, Responsavel)VALUES('{Convert.ToInt32(comboBoxRMesa.Text)}', '{textBoxRCliente.Text}', '{Convert.ToDateTime(dateTimePickerRData.Text)}', '{textBoxRQtdPessoas.Text}', '{textBoxRObservacao.Text}', '{labelUsername.Text}')";
                string sqlUpdate = $"UPDATE `Mesas` SET Status = 'Reservado' WHERE CodigodaMesa = {Convert.ToInt32(comboBoxRMesa.Text)}";
                EditarRegistro(sqlUpdate, 2, "", "", "", connection);
                CadastrarRegistros(sqlInsert, 1, "Cadastrado com sucesso!", connection);
                LimparFormularioReservas();
                comboBoxRMesa.Items.Clear();
                ListarMesasDisponiveis();
                ListarReservas();
                ViewStatus();

                // Mesa
                ListarMesas();
            }
        }

        // Função de fecha a reserva
        private void buttonRFechaReserva_Click(object sender, EventArgs e)
        {
            if (listViewReservas.SelectedIndices.Count > 0)
            {
                String selectedReservas = listViewReservas.SelectedItems[0].SubItems[0].Text;
                int codigoReservas = Convert.ToInt32(selectedReservas);
                DeletarRegistros($"DELETE FROM `Reservas` WHERE CodigodaReserva = {codigoReservas}", "DELETAR", "Você tem certeza que deseja fecha essa reserva ?", "Reserva fechada com sucesso!", connection);
                string sqlUpdate = $"UPDATE `Mesas` SET Status = 'Ocupado' WHERE CodigodaMesa = {Convert.ToInt32(listViewReservas.SelectedItems[0].SubItems[1].Text)}";
                EditarRegistro(sqlUpdate, 2, "", "", "", connection);
                ListarMesasDisponiveis();
                ListarReservas();
                ViewStatus();

                //Mesa
                ListarMesas();
            }
            else
            {
                MessageBox.Show("Selecione uma reserva!");
            }
        }

        // Função que seleciona Reserva que deseja ser editada
        private void buttonREdit_Click(object sender, EventArgs e)
        {
            if (listViewReservas.SelectedIndices.Count > 0)
            {
                comboBoxRMesa.Items.Clear();
                comboBoxRMesa.Items.Add(listViewReservas.SelectedItems[0].SubItems[1].Text);
                ListarMesasDisponiveis();
                textBoxRCliente.Text = listViewReservas.SelectedItems[0].SubItems[2].Text;
                dateTimePickerRData.Text = listViewReservas.SelectedItems[0].SubItems[3].Text;
                textBoxRQtdPessoas.Text = listViewReservas.SelectedItems[0].SubItems[4].Text;
                textBoxRObservacao.Text = listViewReservas.SelectedItems[0].SubItems[5].Text;
                ButtonsEditReservas(false, true, true, false, false, false, false, false);
            }
            else
            {
                MessageBox.Show("Selecione uma reserva!");
            }
        }

        // Função de cancelar a edição da reserva 
        private void buttonRCancelEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja cancelar a edição? \nTodas as informações alteradas seram perdidas!", "CANCELAR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                comboBoxRMesa.Items.Clear();
                ListarMesasDisponiveis();
                LimparFormularioReservas();
                listViewReservas.SelectedItems[0].Selected = false;
                ButtonsEditReservas(true, false, false, true, true, true, true, true);
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### COMANDAS ###################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // DataTable do ListView
        readonly DataTable dataTableListViewComandas = new DataTable();

        // DataTable do ComboBox
        readonly DataTable dataTableComboBoxCMesas = new DataTable();

        // Setup do ListViewComandas
        public void SetupListViewComandas()
        {
            listViewComandas.View = View.Details;
            listViewComandas.FullRowSelect = true;
            listViewComandas.MultiSelect = false;
            listViewComandas.Columns.Add("Código", 100);
            listViewComandas.Columns.Add("Status", 100);
            listViewComandas.Columns.Add("Valor Total", 100);
            comboBoxCStatus.SelectedIndex = 0;

            //Listar contéudo do database
            ListarComandas();
            ListarComboBoxCMesas();
        }

        // Listar as comandas no ListViewComandas
        private void ListarComandas()
        {
            int[] items = { 0, 2 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Comandas` ORDER BY CodigodaComanda ASC", 1, listViewComandas, null, null, connection, dataTableListViewComandas, items);
        }

        // Search do ListViewComandas
        private void buttonCSearchComandas_Click(object sender, EventArgs e)
        {
            comboBoxCStatus.SelectedIndex = 0;
            if (textBoxCSearchComandas.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 2 };
                string sql = $"SELECT * FROM `Comandas` WHERE CodigodaComanda LIKE '{textBoxCSearchComandas.Text}' OR Status LIKE '{textBoxCSearchComandas.Text}' ORDER BY CodigodaComanda ASC";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewComandas, null, null, connection, dataTableListViewComandas, items);
            }
        }

        // Listar o ListViewComandas de acordo com categoria selecionada
        private void comboBoxCStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCSearchComandas.Text = "";
            if (comboBoxCStatus.Text == "Todos")
            {
                ListarComandas();
            }
            else
            {
                int[] items = { 0, 2 };
                string sql = $"SELECT * FROM `Comandas` WHERE Status LIKE '{comboBoxCStatus.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewComandas, null, null, connection, dataTableListViewComandas, items);
            }
        }

        // Listar as mesas para adicionar a comanda
        private void ListarComboBoxCMesas()
        {
            int[] items = { 0 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Mesas` ORDER BY CodigodaMesa ASC", 2, null, comboBoxCMesa, null, connection, dataTableComboBoxCMesas, items);
        }

        // Função para vincular comanda com uma mesa
        private void buttonCAddOrders_Click(object sender, EventArgs e)
        {
            if (listViewComandas.SelectedIndices.Count > 0)
            {
                if (comboBoxCMesa.Text == "")
                {
                    MessageBox.Show("Selecione uma mesa!");
                }
                else
                {
                    if (listViewComandas.SelectedItems[0].SubItems[1].Text == "Livre")
                    {
                        EditarRegistro($"UPDATE `Mesas` SET Status = 'Ocupado' WHERE CodigodaMesa = {Convert.ToInt32(comboBoxCMesa.Text)}", 2, "", "", "", connection);
                        EditarRegistro($"UPDATE `Comandas` SET CodigodaMesa = {Convert.ToInt32(comboBoxCMesa.Text)} , Status = 'Ocupado' WHERE CodigodaComanda = {Convert.ToInt32(listViewComandas.SelectedItems[0].SubItems[0].Text)}", 2, "", "", "", connection);

                        // AddOrders
                        AddOrders addOrders = new AddOrders();
                        addOrders.ViewMesaeComanda(comboBoxCMesa.Text, listViewComandas.SelectedItems[0].SubItems[0].Text);
                        addOrders.AtualizarPage();
                        addOrders.setUsuario(labelUsername.Text);
                        ListarComandasDisponiveis();
                        ListarMesas();
                        ListarComandas();
                        ViewStatus();
                        addOrders.Show();
                    }
                    else
                    {
                        MessageBox.Show("Selecione uma comanda livre!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma mesa e uma comanda!");
            }
        }

        // Função para abrir determinada comanda (Comandas).
        private void buttonCAbrirComanda_Click(object sender, EventArgs e)
        {
            if (listViewComandas.SelectedIndices.Count > 0)
            {
                if (listViewComandas.SelectedItems[0].SubItems[1].Text == "Livre")
                {
                    MessageBox.Show("Selecione uma comanda ocupada!");
                }
                else
                {
                    Label label = new Label();
                    int[] items = { 1 };
                    ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                    // AddOrders
                    AddOrders addOrders = new AddOrders();
                    addOrders.ViewMesaeComanda(label.Text, listViewComandas.SelectedItems[0].SubItems[0].Text);
                    addOrders.AtualizarPage();
                    addOrders.setUsuario(labelUsername.Text);
                    ListarComandasDisponiveis();
                    ListarMesas();
                    ListarComandas();
                    ViewStatus();
                    addOrders.Show();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }

        // Função de fechamento da comanda (Tela Comanda)
        private void buttonCFechaComanda_Click(object sender, EventArgs e)
        {
            if (listViewComandas.SelectedIndices.Count > 0)
            {
                if (listViewComandas.SelectedItems[0].SubItems[1].Text == "Livre")
                {
                    MessageBox.Show("Selecione uma comanda ocupada!");
                }
                else
                {
                    Label label = new Label();
                    int[] items = { 1 };
                    ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                    // AddOrders
                    AddOrders addOrders = new AddOrders();
                    addOrders.ViewMesaeComanda(label.Text, listViewComandas.SelectedItems[0].SubItems[0].Text);
                    addOrders.AtualizarPage();
                    addOrders.setUsuario(labelUsername.Text);
                    addOrders.Hide();
                    addOrders.FechaComanda();
                    addOrders.Close();
                    listViewMComandas.Items.Clear();
                    ListarMesasDisponiveis();
                    ListarMesas();
                    ListarComandas();
                    ViewStatus();
                    ListarComandasDisponiveis();
                    ListarMesasDisponiveis();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }

        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### MESAS ######################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // DataTables dos ListViews
        readonly DataTable dataTableListViewMesas = new DataTable();
        readonly DataTable dataTableListViewMComandas = new DataTable();
        readonly DataTable dataTableMNumeroMesa = new DataTable();
        readonly DataTable dataTableMFechamentoComanda = new DataTable();

        // DataTables dos ComboBox
        readonly DataTable dataTableComboBoxMComandas = new DataTable();

        // Setup do ListViewMComandas
        public void SetupListViewMComandas()
        {
            listViewMComandas.View = View.Details;
            listViewMComandas.FullRowSelect = true;
            listViewMComandas.MultiSelect = false;
            listViewMComandas.Columns.Add("Código", 100);
        }

        // Setup do ListViewMesa
        public void SetupListViewMesas()
        {
            listViewMesas.View = View.Details;
            listViewMesas.FullRowSelect = true;
            listViewMesas.MultiSelect = false;
            listViewMesas.Columns.Add("Código", 100);
            listViewMesas.Columns.Add("Status", 100);
            comboBoxMesas.SelectedIndex = 0;

            // Listar conteúdo do database
            ListarMesas();
            ListarComandasDisponiveis();
        }

        // Listar as Mesas do ListViewMesas
        private void ListarMesas()
        {
            int[] items = { 0, 1 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Mesas` ORDER BY CodigodaMesa ASC", 1, listViewMesas, null, null, connection, dataTableListViewMesas, items);
        }

        // Listar as comandas disponiveis
        private void ListarComandasDisponiveis()
        {
            comboBoxMComanda.Items.Clear();
            int[] items = { 0 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Comandas` WHERE Status LIKE 'Livre' ORDER BY CodigodaComanda ASC", 2, null, comboBoxMComanda, null, connection, dataTableComboBoxMComandas, items);
        }

        // Visualizar as comandas de acordo com a mesa selecionada
        private void listViewMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMesas.SelectedIndices.Count > 0)
            {
                String selectedMesa = listViewMesas.SelectedItems[0].SubItems[0].Text;
                int codigoMesa = Convert.ToInt32(selectedMesa);
                int[] items = { 0 };
                ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaMesa LIKE {codigoMesa} ORDER BY CodigodaComanda ASC", 1, listViewMComandas, null, null, connection, dataTableListViewMComandas, items);
            }
        }

        // Search do ListViewMesas
        private void buttonMSearch_Click(object sender, EventArgs e)
        {
            comboBoxMesas.SelectedIndex = 0;
            if (textBoxMSearch.Text == "")
            {
                MessageBox.Show("Campo de pesquisar vazio!");
            }
            else
            {
                int[] items = { 0, 1 };
                string sql = $"SELECT * FROM `Mesas` WHERE CodigodaMesa LIKE '{textBoxMSearch.Text}' OR Status LIKE '{textBoxMSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewMesas, null, null, connection, dataTableListViewMesas, items);
            }
        }

        // Listar o ListViewMesas de acordo com categoria selecionada
        private void comboBoxMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxMSearch.Text = "";
            if (comboBoxMesas.Text == "Todos")
            {
                ListarMesas();
            }
            else
            {
                int[] items = { 0, 1 };
                string sql = $"SELECT * FROM `Mesas` WHERE Status LIKE '{comboBoxMesas.Text}' ORDER BY CodigodaMesa ASC";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewMesas, null, null, connection, dataTableListViewMesas, items);
            }
        }

        // Vincular Mesa em uma comanda 
        private void buttonMAddOrders_Click(object sender, EventArgs e)
        {
            if (listViewMesas.SelectedIndices.Count > 0)
            {
                if (comboBoxMComanda.Text == "")
                {
                    MessageBox.Show("Selecione uma comanda!");
                }
                else
                {
                    if (listViewMesas.SelectedItems[0].SubItems[1].Text == "Reservado")
                    {
                        MessageBox.Show("Selecione uma comanda livre ou ocupada!");
                    }
                    else
                    {
                        EditarRegistro($"UPDATE `Mesas` SET Status = 'Ocupado' WHERE CodigodaMesa = {Convert.ToInt32(listViewMesas.SelectedItems[0].SubItems[0].Text)}", 2, "", "", "", connection);
                        EditarRegistro($"UPDATE `Comandas` SET CodigodaMesa = {Convert.ToInt32(listViewMesas.SelectedItems[0].SubItems[0].Text)} , Status = 'Ocupado' WHERE CodigodaComanda = {Convert.ToInt32(comboBoxMComanda.Text)}", 2, "", "", "", connection);

                        // AddOrders
                        AddOrders addOrders = new AddOrders();
                        addOrders.ViewMesaeComanda(listViewMesas.SelectedItems[0].SubItems[0].Text, comboBoxMComanda.Text);
                        addOrders.AtualizarPage();
                        addOrders.setUsuario(labelUsername.Text);
                        ListarComandasDisponiveis();
                        ListarMesas();
                        ListarComandas();
                        ViewStatus();
                        addOrders.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma mesa e uma comanda!");
            }
        }

        // Função para abrir determinada comanda (Mesas).
        private void buttonMOpenComanda_Click(object sender, EventArgs e)
        {
            if (listViewMComandas.SelectedIndices.Count > 0)
            {
                AddOrders addOrders = new AddOrders();
                Label label = new Label();
                int[] items = { 1 };
                ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewMComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                // AddOrders
                addOrders.ViewMesaeComanda(label.Text, listViewMComandas.SelectedItems[0].SubItems[0].Text);
                addOrders.AtualizarPage();
                addOrders.setUsuario(labelUsername.Text);
                ListarComandasDisponiveis();
                ListarMesas();
                ListarComandas();
                ViewStatus();
                addOrders.Show();
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }
        // Função de fecha a comanda (Tela de Mesa)
        private void buttonMExitComanda_Click(object sender, EventArgs e)
        {
            if (listViewMComandas.SelectedItems.Count > 0)
            {
                AddOrders addOrders = new AddOrders();
                Label label = new Label();
                int[] items = { 1 };
                ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewMComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                // AddOrders
                addOrders.ViewMesaeComanda(label.Text, listViewMComandas.SelectedItems[0].SubItems[0].Text);
                addOrders.AtualizarPage();
                addOrders.setUsuario(labelUsername.Text);
                addOrders.Hide();

                // Gerar nota fiscal
                addOrders.FechaComanda();
                addOrders.Close();
                listViewMComandas.Items.Clear();  
                ListarMesas();
                ListarComandas();
                ViewStatus();
                ListarComandasDisponiveis();
                ListarMesasDisponiveis();
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // ####################################################################################################################################################################################################### //



        // ####################################################################################################################################################################################################### //
        // ####################################################################################### FUNÇÕES ####################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // Função que listar os dados do database no ListView ou comboBox ou Label
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
        private void DeletarRegistros(string sql, string titleMsgAlert, string msgAlert, string msgOK, OleDbConnection connection)
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

                if (MessageBox.Show(msgAlert, titleMsgAlert, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(msgOK);
                    }
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

        // Função gerar as mesas 
        private void CreateMesas()
        {
            if (CountRegistros("SELECT COUNT (*) FROM `Mesas`", connection) == 0)
            {
                for (int i = 1; i <= 100; i++)
                {
                    CadastrarRegistros($"INSERT INTO `Mesas`(CodigodaMesa, Status)VALUES('{i}', 'Livre')", 2, "", connection);
                }
                ListarMesas();
                ListarMesasDisponiveis();
            }
        }

        // Função gerar as comandas
        private void CreateComandas()
        {
            if (CountRegistros("SELECT COUNT (*) FROM `Comandas`", connection) == 0)
            {
                for (int i = 101; i <= 500; i++)
                {
                    CadastrarRegistros($"INSERT INTO `Comandas`(CodigodaComanda, CodigodaMesa, Status)VALUES('{i}', {0}, 'Livre')", 2, "", connection);
                }
                ListarComandas();
                ListarComandasDisponiveis();
            }
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

        // Função para sair do sistema
        private void buttonCancelLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Certeza que deseja sair do sistema ?", "SAIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        // Verificar a base de dados
        private void buttonUpdateConnection_Click(object sender, EventArgs e)
        {
            connection.Open();
            connection.ResetState();
            connection.Close();
            ViewStatus();
            MessageBox.Show("Atualização efetuada com sucesso!");
        }

        // Verificar a conexão
        private void buttonCheckConnection_Click(object sender, EventArgs e)
        {
            connection.Open();
            connection.ResetState();
            connection.Close();
            MessageBox.Show("Verificação efetuada com sucesso!");
        }

        // Função de validar os campos 
        private void ValidarCampoNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        // Função de salvar os dados alterados de jogos
        private void buttonJSaveEdit_Click(object sender, EventArgs e)
        {
            if (textBoxJCodigo.Text == "" || textBoxJNome.Text == "" || comboBoxJCategoria.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                EditarRegistro($"UPDATE `Jogos` SET Nome = '{textBoxJNome.Text}', Status = '{listViewJogos.SelectedItems[0].SubItems[2].Text}', Categoria = '{comboBoxJCategoria.Text}', Descricao = '{textBoxJDescricao.Text}' WHERE CodigodoJogo = {Convert.ToInt32(textBoxJCodigo.Text)} ", 1, "Deseja salvar as alterações?", "EDIÇÃO", "Alteração efetuda com sucesso!", connection);
                ButtonsEditJogos(true, false, false, true, true, true, true, true, true);
                LimparFormularioJogos();
                ListarJogos();
                ViewStatus();
            }
        }
        public void mostarAjuda()
        {
            axAcroPDFAjuda2.src = "C:/Users/Isaías/Desktop/PI - Butecus Sta Tereza/Manual.pdf";
            axAcroPDFAjuda2.Show();
        }

        // Função que gerar os PDFs.
        private void GerarPDF(Document document, int opcao, ListView listview, string tittleReport, string subtittleReport, string paragraph, int colunas)
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
            header.Add($"Relatório - {tittleReport} - {DateTime.Now} \n\n");
            header.Alignment = Element.ALIGN_CENTER;

            // Logo
            var image = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\logo.png");
            image.Alignment = Element.ALIGN_CENTER;
            image.ScaleToFit(200, 100);

            // Conteúdo
            Paragraph conteudo = new Paragraph();
            conteudo.Alignment = Element.ALIGN_JUSTIFIED;
            conteudo.Add(paragraph);

            // Adicionar o header, imagem e conteudo no pdf
            relatorioDoc.Add(header);
            relatorioDoc.Add(image);
            relatorioDoc.Add(conteudo);

            // Com ou sem tabela
            if (opcao == 1)
            {
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
            }
            relatorioDoc.Close();

            // Abre o pdf em um form
            Report pdf = new Report();
            pdf.getPDF(Convert.ToString(localDoc), subtittleReport);
            pdf.Show();
        }


        // Função para atualizar
        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {
            listViewMComandas.Items.Clear();
            ListarMesasDisponiveis();
            ListarMesas();
            ListarComandas();
            ListarJogos();
            ViewStatus();
        }


        // Função para cancelar a comanda 
        private void buttonMCancelComanda_Click(object sender, EventArgs e)
        {
            if (listViewMComandas.SelectedItems.Count > 0)
            {
                AddOrders addOrders = new AddOrders();
                Label label = new Label();
                int[] items = { 1 };
                ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewMComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                // AddOrders
                addOrders.ViewMesaeComanda(label.Text, listViewMComandas.SelectedItems[0].SubItems[0].Text);
                addOrders.AtualizarPage();
                addOrders.setUsuario(labelUsername.Text);
                addOrders.Hide();

                // Gerar nota fiscal
                addOrders.CancelarComanda();
                addOrders.Close();
                listViewMComandas.Items.Clear();
                ListarMesas();
                ListarComandas();
                ViewStatus();
                ListarComandasDisponiveis();
                ListarMesasDisponiveis();
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }

        // Função para cancelar a comanda
        private void buttonCCancelarComanda_Click(object sender, EventArgs e)
        {
            if (listViewComandas.SelectedIndices.Count > 0)
            {
                if (listViewComandas.SelectedItems[0].SubItems[1].Text == "Livre")
                {
                    MessageBox.Show("Selecione uma comanda ocupada!");
                }
                else
                {
                    Label label = new Label();
                    int[] items = { 1 };
                    ListarConteudoListView_OR_ComboBox($"SELECT * FROM `Comandas` WHERE CodigodaComanda = {listViewComandas.SelectedItems[0].SubItems[0].Text}", 3, null, null, label, connection, dataTableMNumeroMesa, items);

                    // AddOrders
                    AddOrders addOrders = new AddOrders();
                    addOrders.ViewMesaeComanda(label.Text, listViewComandas.SelectedItems[0].SubItems[0].Text);
                    addOrders.AtualizarPage();
                    addOrders.setUsuario(labelUsername.Text);
                    addOrders.Hide();
                    addOrders.CancelarComanda();
                    addOrders.Close();
                    listViewMComandas.Items.Clear();
                    ListarMesasDisponiveis();
                    ListarMesas();
                    ListarComandas();
                    ViewStatus();
                    ListarComandasDisponiveis();
                    ListarMesasDisponiveis();
                }
            }
            else
            {
                MessageBox.Show("Selecione uma comanda!");
            }
        }
        // DataTables do relatório
        readonly DataTable dataTablerelatorio = new DataTable();

        // Função para gerar relátorio de vendas
        private void RelatorioGeral(string tittleRelatorio, string data)
        {
            // Quantidade de pedidos feito no dia
            int quantidadePedidos = CountRegistros($"SELECT COUNT(*) FROM `PedidosBebidaseComidas` WHERE Status LIKE 'Finalizado' AND Data LIKE '{data}' ", connection);
            // Valor total do dia
           // Label labelValorTotal = new Label();
           // int[] items = { 0 };
           // ListarConteudoListView_OR_ComboBox($"SELECT SUM(pbc.Qtd * bc.Preco) AS TOTAL_VALOR FROM `PedidosBebidaseComidas` AS pbc INNER JOIN `BebidaseComidas` AS bc ON pbc.CodigodaBebidaseComidas = bc.CodigodaBebidaseComidas WHERE Status = 'Finalizado' AND Data = '{data}'",3,null,null,labelValorTotal, connection, dataTablerelatorio, items);
            Document relatorio = new Document(PageSize.A4);
            string relatorioDOC = "";

            // Valor total dinheiro de pedidos
            var total = 0m;
            Label labelValorTotal = new Label();
            for (int i = 0; i < listViewRelatorio.Items.Count; i++)
            {
                total += decimal.Parse(listViewRelatorio.Items[i].SubItems[4].Text);
            }
            labelValorTotal.Text = "R$ " + total.ToString("N2");


            // Quantidade de produtos vendidos no dia
            int total2 = 0;
            Label labelValorTotal2 = new Label();
            for (int j = 0; j < listViewRelatorio.Items.Count; j++)
            {
                total2 += int.Parse(listViewRelatorio.Items[j].SubItems[3].Text);
            }
            labelValorTotal2.Text = total2.ToString();

            // Adicionando os valores no pdf 
            relatorioDOC += $"\nRelatório do dia: {data}";
            relatorioDOC += "\nA quantidade de pedidos realizado no dia: " + Convert.ToString(quantidadePedidos);
            relatorioDOC += "\nA quantidade de produtos vendido no dia: "+labelValorTotal2.Text;
            relatorioDOC += "\nO valor total vendido no dia: "+labelValorTotal.Text + "\n\n\n";
           // relatorioDOC += labelValorTotal.Text + "\n";
            GerarPDF(relatorio, 1, listViewRelatorio, "("+tittleRelatorio+")", $"Relatório ({tittleRelatorio})", relatorioDOC, 6);
        }

        // Listar todos as comandas finalizadas
        private void SetupComandasFinalizadas(string data)
        {
            listViewRelatorio.Clear();
            listViewRelatorio.View = View.Details;
            listViewRelatorio.FullRowSelect = true;
            listViewRelatorio.MultiSelect = false;
            listViewRelatorio.Columns.Add("Código", 100);
            listViewRelatorio.Columns.Add("Produto", 100);
            listViewRelatorio.Columns.Add("Valor", 100);
            listViewRelatorio.Columns.Add("Quantidade", 100);
            listViewRelatorio.Columns.Add("Total", 100);
            listViewRelatorio.Columns.Add("Data", 100);

            ListarComandasFinalizadas(data);
        }
        // DataTable de relatório
        readonly DataTable dataTableRelatorio = new DataTable();

        // Listar os pedidos finalizado
        private void ListarComandasFinalizadas(string data)
        {
            int[] items = { 0, 1, 2, 3, 4, 5 };
            ListarConteudoListView_OR_ComboBox($"SELECT pbc.CodigodoPedidosBebidaseComidas, bc.Nome, bc.Preco, pbc.Qtd, (pbc.Qtd * bc.Preco) AS TOTAL_VALOR, pbc.Data FROM `PedidosBebidaseComidas` AS pbc INNER JOIN `BebidaseComidas` AS bc ON pbc.CodigodaBebidaseComidas = bc.CodigodaBebidaseComidas WHERE Status LIKE 'Finalizado' AND Data LIKE '{data}'", 1, listViewRelatorio, null, null, connection, dataTableRelatorio, items);
        }

        // Função para gerar o relatório do dia
        private void buttonGerarPDFD_Click(object sender, EventArgs e)
        {
            SetupComandasFinalizadas(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
            RelatorioGeral($"Geral", String.Format("{0:dd/MM/yyyy}", DateTime.Now));
        }

        //Função para gerar o relatório do dia especifico
        private void buttonGerarPDFDE_Click(object sender, EventArgs e)
        {
            if(dateTimePickerRelatorioDE.Text != "")
            {
                SetupComandasFinalizadas(String.Format("{0:dd/MM/yyyy}", dateTimePickerRelatorioDE.Value));
                RelatorioGeral($"Geral", String.Format("{0:dd/MM/yyyy}", dateTimePickerRelatorioDE.Value));
            }
            else
            {
                MessageBox.Show("Selecione uma data!");
            }
        }


        // Função para bloquear accesso CRUD de Bebidas e Comidas / Jogos / Categorias e Visualizar relatorio
        public void Acesso()
        {
            if (labelUsername.Text != "adm")
            {
                tabControlBebidaseComidas.TabPages.Remove(tabPageRelatorios);
                // Bebidas e Comidas
                buttonBCCadastrar.Enabled = false;
                buttonBCDelete.Enabled = false;
                buttonBCEdit.Enabled = false;

                // Jogos
                buttonJEdit.Enabled = false;
                buttonJCadastrar.Enabled = false;
                buttonJDelete.Enabled = false;

                // Categorias
                buttonConfCadastro.Enabled = false;
                buttonConfEditar.Enabled = false;
                buttonConfDelete.Enabled = false;
            }
        }
    }
}

