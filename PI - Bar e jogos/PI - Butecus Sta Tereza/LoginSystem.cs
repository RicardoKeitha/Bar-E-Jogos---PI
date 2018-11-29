using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace PI___Butecus_Sta_Tereza
{
    public partial class LoginSystem : Form
    {

        // Conexão com banco de dados
        private static string localConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/PI - Banco de Dados.mdb;";
        readonly OleDbConnection connection = new OleDbConnection(Convert.ToString(localConnection));


        public LoginSystem()
        {


            InitializeComponent();


            mostarAjuda();


            // Setups
            InsertAdm();
            SetupDelete();
        }
        public void mostarAjuda()
        {
            axAcroPDFAjuda.src = "C:/Users/Isaías/Desktop/PI - Butecus Sta Tereza/Manual.pdf";
            axAcroPDFAjuda.Show();
        }
        // ####################################################################################################################################################################################################### //
        // ####################################################################################### FUNÇÕES ####################################################################################################### //
        // ####################################################################################################################################################################################################### //
        // Função que listar os dados do database no ListView ou comboBox
        private void ListarConteudoListView_OR_ComboBox(string sql, int opcao, ListView listView, ComboBox comboBox, OleDbConnection connection, DataTable dataTable, int[] Items)
        {
            // Opções
            // opcao == 1 == ListView
            // opcao == 2 == ComboBox

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
                    else
                    {
                        comboBox.Items.Add(row[Items[0]].ToString());
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



        // Função que realizar o Login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxLUser.Text == "" || textBoxLPassword.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                int loginValue = CountRegistros($"SELECT COUNT(*) FROM `Usuario` WHERE `Nome` LIKE '{textBoxLUser.Text}' AND `Senha` LIKE '{textBoxLPassword.Text}'", connection);
                if (loginValue == 0)
                {
                    MessageBox.Show("Usuário ou senha errado!");
                }
                else
                {
                    MenuSystem menuSystem = new MenuSystem();
                    if (MessageBox.Show("Login efetuado com sucesso!") == DialogResult.OK)
                    {
                        menuSystem.UserLogin(textBoxLUser.Text);
                        menuSystem.Acesso();
                        menuSystem.Show();
                        this.Hide();
                    }
                }
                
            }
        }

        // Limpar formulário de Redefinir acesso
        private void LimparFormularioRedefinir()
        {
            maskedTextBoxRCPF.Text = "";
            textBoxRNewPassword.Text = "";
            textBoxRUser.Text = "";
            textBoxRNewPassword.Text = "";
            textBoxRPasswordAdm.Text = "";
        }

        // Limpar formulário de Login
        private void LimparFormularioLogin()
        {
            textBoxLUser.Text = "";
            textBoxLPassword.Text = "";
        }


        // Função para redefinir senha de usuário
        private void buttonRedefinir_Click(object sender, EventArgs e)
        {
            if(maskedTextBoxRCPF.Text == "" || textBoxRNewPassword.Text == "" || textBoxRUser.Text == "" || textBoxRNewPassword.Text == "" || textBoxRPasswordAdm.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                int adm = CountRegistros($"SELECT COUNT(*) FROM `Usuario` WHERE Nome LIKE 'Adm' AND Senha LIKE '{textBoxRPasswordAdm.Text}'", connection);
                if (adm == 0)
                {
                    MessageBox.Show("Senha errada de Administrador!");
                }
                else
                {
                    EditarRegistro($"UPDATE `Usuario` SET Nome = '{textBoxRUser.Text}', Senha = '{textBoxRNewPassword.Text}' WHERE CPF = '{maskedTextBoxRCPF.Text}' AND Nome <> 'Adm'", 1, "Certeza que deseja redefinir o acesso para esse usuário?", "Redefinir", "Redefinição de acesso efetuado com sucesso", connection);
                    ListarUsuarios();
                    LimparFormularioRedefinir();
                }
            }
        }

        // Limpar formulário de cadastro
        private void LimparFormularioCadastro()
        {
            textBoxCUser.Text = "";
            textBoxCPasswordAdm.Text = "";
            textBoxCPassword.Text = "";
            maskedTextBoxCCPF.Text = "";
        }

        // Função para cadastrar usuários
        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            if(textBoxCUser.Text == "" || textBoxCPasswordAdm.Text == "" || textBoxCPassword.Text == "" || maskedTextBoxCCPF.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
            }
            else
            {
                int adm = CountRegistros($"SELECT COUNT(*) FROM `Usuario` WHERE Nome LIKE 'Adm' AND Senha LIKE '{textBoxCPasswordAdm.Text}'", connection);
                if (adm == 0)
                {
                    MessageBox.Show("Senha errada de Administrador!");
                }
                else
                {
                    CadastrarRegistros($"INSERT INTO `Usuario`(CPF, Nome, Senha)VALUES('{maskedTextBoxCCPF.Text}','{textBoxCUser.Text}','{textBoxCPassword.Text}')", 1, "Cadastrado com sucesso!", connection);
                    ListarUsuarios();
                    LimparFormularioCadastro();
                }
                
            }
        }


        // DataTables de Delete
        readonly DataTable dataTableDelete = new DataTable();
        // Setup ListViewDelete
        private void SetupDelete()
        {
            listViewDelete.View = View.Details;
            listViewDelete.FullRowSelect = true;
            listViewDelete.MultiSelect = false;
            listViewDelete.HideSelection = false;
            listViewDelete.Columns.Add("CPF", 100);
            listViewDelete.Columns.Add("Usuário", 100);

            // Listar
            ListarUsuarios();
        }

        // Função que listar os usuários
        private void ListarUsuarios()
        {
            int[] items = { 0, 1 };
            ListarConteudoListView_OR_ComboBox("SELECT * FROM `Usuario` WHERE Nome <> 'Adm' ", 1, listViewDelete, null, connection, dataTableDelete, items);
        }

        // Função que pesquisa os usuários
        private void buttonDSearch_Click(object sender, EventArgs e)
        {
            if (textBoxDSearch.Text == "")
            {
                ListarUsuarios();
            }
            else
            {
                int[] items = { 0, 1 };
                string sql = $"SELECT * FROM `Usuario` WHERE CPF LIKE '{textBoxDSearch.Text}' OR Nome LIKE '{textBoxDSearch.Text}'";
                ListarConteudoListView_OR_ComboBox(sql, 1, listViewDelete, null, connection, dataTableDelete, items);
            }
        }

        // Limpar formulário de Delete
        private void LimparFormularioDelete()
        {
            textBoxDPasswordAdm.Text = "";
            textBoxDSearch.Text = "";
        }

        // Função para deletar usuários
        private void buttonDDelete_Click(object sender, EventArgs e)
        {
            if (listViewDelete.SelectedIndices.Count > 0)
            {
                if (textBoxDPasswordAdm.Text == "")
                {
                    MessageBox.Show("Digite a senha de administrador!");
                }
                else
                {
                    int adm = CountRegistros($"SELECT COUNT(*) FROM `Usuario` WHERE Nome LIKE 'Adm' AND Senha LIKE '{textBoxDPasswordAdm.Text}'", connection);
                    if (adm == 0)
                    {
                        MessageBox.Show("Senha errada de Administrador!");
                    }
                    else
                    {
                        String selectedCPF = listViewDelete.SelectedItems[0].SubItems[0].Text;
                        DeletarRegistros($"DELETE FROM `Usuario` WHERE CPF = '{selectedCPF}'", "DELETAR", "Você tem certeza que deseja deletar esse usuário?", "Deletado com Sucesso!", connection);
                        LimparFormularioDelete();
                        ListarUsuarios();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um usuário!");
            }
        }

        // Função que verificar a conexão
        private void buttonCCheckConnection_Click(object sender, EventArgs e)
        {
            connection.Open();
            connection.ResetState();
            connection.Close();
            MessageBox.Show("Verificação efetuada com sucesso!");

        }

        // Função que verificar base de dados 
        private void buttonCUpdateConnection_Click(object sender, EventArgs e)
        {
            connection.Open();
            connection.ResetState();
            connection.Close();
            MessageBox.Show("Atualização efetuada com sucesso!");
        }

        // Gerar senha de administrador
        private void InsertAdm()
        {
            int adm = CountRegistros("SELECT COUNT(*) FROM `usuario` WHERE CPF LIKE '111.111.111-11' AND Nome LIKE 'Adm'", connection);
            if(adm == 0)
            {
                CadastrarRegistros("INSERT INTO `Usuario`(CPF, Nome, Senha)VALUES('111.111.111-11', 'Adm', 'adm123') ", 2, "",connection);
            }
        }
    }
}
