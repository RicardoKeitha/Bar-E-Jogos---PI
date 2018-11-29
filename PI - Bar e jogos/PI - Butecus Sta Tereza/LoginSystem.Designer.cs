namespace PI___Butecus_Sta_Tereza
{
    partial class LoginSystem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginSystem));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLogin = new System.Windows.Forms.TabPage();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLPassword = new System.Windows.Forms.TextBox();
            this.textBoxLUser = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPageRedefinirAcesso = new System.Windows.Forms.TabPage();
            this.maskedTextBoxRCPF = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRPasswordAdm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRedefinir = new System.Windows.Forms.Button();
            this.tabPageCadastrarNovoUsuario = new System.Windows.Forms.TabPage();
            this.maskedTextBoxCCPF = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCPasswordAdm = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCUser = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonCadastrar = new System.Windows.Forms.Button();
            this.tabPageDeletarUsuario = new System.Windows.Forms.TabPage();
            this.textBoxDPasswordAdm = new System.Windows.Forms.TextBox();
            this.buttonDDelete = new System.Windows.Forms.Button();
            this.textBoxDSearch = new System.Windows.Forms.TextBox();
            this.buttonDSearch = new System.Windows.Forms.Button();
            this.listViewDelete = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.buttonCUpdateConnection = new System.Windows.Forms.Button();
            this.buttonCCheckConnection = new System.Windows.Forms.Button();
            this.tabPageAjuda = new System.Windows.Forms.TabPage();
            this.tabPageSobre = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.axAcroPDFAjuda = new AxAcroPDFLib.AxAcroPDF();
            this.tabControl1.SuspendLayout();
            this.tabPageLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageRedefinirAcesso.SuspendLayout();
            this.tabPageCadastrarNovoUsuario.SuspendLayout();
            this.tabPageDeletarUsuario.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageAjuda.SuspendLayout();
            this.tabPageSobre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFAjuda)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLogin);
            this.tabControl1.Controls.Add(this.tabPageRedefinirAcesso);
            this.tabControl1.Controls.Add(this.tabPageCadastrarNovoUsuario);
            this.tabControl1.Controls.Add(this.tabPageDeletarUsuario);
            this.tabControl1.Controls.Add(this.tabPageConfig);
            this.tabControl1.Controls.Add(this.tabPageAjuda);
            this.tabControl1.Controls.Add(this.tabPageSobre);
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(15, 15);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 493);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageLogin
            // 
            this.tabPageLogin.Controls.Add(this.buttonLogin);
            this.tabPageLogin.Controls.Add(this.label2);
            this.tabPageLogin.Controls.Add(this.label1);
            this.tabPageLogin.Controls.Add(this.textBoxLPassword);
            this.tabPageLogin.Controls.Add(this.textBoxLUser);
            this.tabPageLogin.Controls.Add(this.pictureBox1);
            this.tabPageLogin.Location = new System.Drawing.Point(4, 46);
            this.tabPageLogin.Name = "tabPageLogin";
            this.tabPageLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogin.Size = new System.Drawing.Size(760, 443);
            this.tabPageLogin.TabIndex = 0;
            this.tabPageLogin.Text = "LOGIN";
            this.tabPageLogin.UseVisualStyleBackColor = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(428, 387);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "ACESSAR";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(261, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Senha *";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(253, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuário *";
            // 
            // textBoxLPassword
            // 
            this.textBoxLPassword.Location = new System.Drawing.Point(333, 336);
            this.textBoxLPassword.MaxLength = 10;
            this.textBoxLPassword.Name = "textBoxLPassword";
            this.textBoxLPassword.PasswordChar = '*';
            this.textBoxLPassword.Size = new System.Drawing.Size(170, 20);
            this.textBoxLPassword.TabIndex = 2;
            // 
            // textBoxLUser
            // 
            this.textBoxLUser.Location = new System.Drawing.Point(333, 295);
            this.textBoxLUser.Name = "textBoxLUser";
            this.textBoxLUser.Size = new System.Drawing.Size(170, 20);
            this.textBoxLUser.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PI___Butecus_Sta_Tereza.Properties.Resources.Imagem;
            this.pictureBox1.Location = new System.Drawing.Point(263, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 222);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPageRedefinirAcesso
            // 
            this.tabPageRedefinirAcesso.Controls.Add(this.maskedTextBoxRCPF);
            this.tabPageRedefinirAcesso.Controls.Add(this.label6);
            this.tabPageRedefinirAcesso.Controls.Add(this.textBoxRPasswordAdm);
            this.tabPageRedefinirAcesso.Controls.Add(this.label5);
            this.tabPageRedefinirAcesso.Controls.Add(this.textBoxRNewPassword);
            this.tabPageRedefinirAcesso.Controls.Add(this.label4);
            this.tabPageRedefinirAcesso.Controls.Add(this.textBoxRUser);
            this.tabPageRedefinirAcesso.Controls.Add(this.label3);
            this.tabPageRedefinirAcesso.Controls.Add(this.buttonRedefinir);
            this.tabPageRedefinirAcesso.Location = new System.Drawing.Point(4, 46);
            this.tabPageRedefinirAcesso.Name = "tabPageRedefinirAcesso";
            this.tabPageRedefinirAcesso.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRedefinirAcesso.Size = new System.Drawing.Size(760, 443);
            this.tabPageRedefinirAcesso.TabIndex = 1;
            this.tabPageRedefinirAcesso.Text = "REDEFINIR ACESSO";
            this.tabPageRedefinirAcesso.UseVisualStyleBackColor = true;
            // 
            // maskedTextBoxRCPF
            // 
            this.maskedTextBoxRCPF.Location = new System.Drawing.Point(360, 134);
            this.maskedTextBoxRCPF.Mask = "000\\.000\\.000\\-00";
            this.maskedTextBoxRCPF.Name = "maskedTextBoxRCPF";
            this.maskedTextBoxRCPF.Size = new System.Drawing.Size(147, 20);
            this.maskedTextBoxRCPF.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(181, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Senha de Administrador * ";
            // 
            // textBoxRPasswordAdm
            // 
            this.textBoxRPasswordAdm.Location = new System.Drawing.Point(360, 249);
            this.textBoxRPasswordAdm.MaxLength = 10;
            this.textBoxRPasswordAdm.Name = "textBoxRPasswordAdm";
            this.textBoxRPasswordAdm.PasswordChar = '*';
            this.textBoxRPasswordAdm.Size = new System.Drawing.Size(147, 20);
            this.textBoxRPasswordAdm.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(259, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nova Senha *";
            // 
            // textBoxRNewPassword
            // 
            this.textBoxRNewPassword.Location = new System.Drawing.Point(360, 212);
            this.textBoxRNewPassword.MaxLength = 10;
            this.textBoxRNewPassword.Name = "textBoxRNewPassword";
            this.textBoxRNewPassword.PasswordChar = '*';
            this.textBoxRNewPassword.Size = new System.Drawing.Size(147, 20);
            this.textBoxRNewPassword.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(288, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Usuário *";
            // 
            // textBoxRUser
            // 
            this.textBoxRUser.Location = new System.Drawing.Point(360, 173);
            this.textBoxRUser.Name = "textBoxRUser";
            this.textBoxRUser.Size = new System.Drawing.Size(147, 20);
            this.textBoxRUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "CPF *";
            // 
            // buttonRedefinir
            // 
            this.buttonRedefinir.Location = new System.Drawing.Point(432, 297);
            this.buttonRedefinir.Name = "buttonRedefinir";
            this.buttonRedefinir.Size = new System.Drawing.Size(75, 23);
            this.buttonRedefinir.TabIndex = 5;
            this.buttonRedefinir.Text = "REDEFINIR";
            this.buttonRedefinir.UseVisualStyleBackColor = true;
            this.buttonRedefinir.Click += new System.EventHandler(this.buttonRedefinir_Click);
            // 
            // tabPageCadastrarNovoUsuario
            // 
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.maskedTextBoxCCPF);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.label7);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.textBoxCPasswordAdm);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.label8);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.textBoxCPassword);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.label9);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.textBoxCUser);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.label10);
            this.tabPageCadastrarNovoUsuario.Controls.Add(this.buttonCadastrar);
            this.tabPageCadastrarNovoUsuario.Location = new System.Drawing.Point(4, 46);
            this.tabPageCadastrarNovoUsuario.Name = "tabPageCadastrarNovoUsuario";
            this.tabPageCadastrarNovoUsuario.Size = new System.Drawing.Size(760, 443);
            this.tabPageCadastrarNovoUsuario.TabIndex = 2;
            this.tabPageCadastrarNovoUsuario.Text = "CADASTRAR NOVO USUÁRIO";
            this.tabPageCadastrarNovoUsuario.UseVisualStyleBackColor = true;
            // 
            // maskedTextBoxCCPF
            // 
            this.maskedTextBoxCCPF.Location = new System.Drawing.Point(375, 138);
            this.maskedTextBoxCCPF.Mask = "000\\.000\\.000\\-00";
            this.maskedTextBoxCCPF.Name = "maskedTextBoxCCPF";
            this.maskedTextBoxCCPF.Size = new System.Drawing.Size(147, 20);
            this.maskedTextBoxCCPF.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(200, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Senha de Administrador *";
            // 
            // textBoxCPasswordAdm
            // 
            this.textBoxCPasswordAdm.Location = new System.Drawing.Point(375, 252);
            this.textBoxCPasswordAdm.MaxLength = 10;
            this.textBoxCPasswordAdm.Name = "textBoxCPasswordAdm";
            this.textBoxCPasswordAdm.PasswordChar = '*';
            this.textBoxCPasswordAdm.Size = new System.Drawing.Size(147, 20);
            this.textBoxCPasswordAdm.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(311, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Senha *";
            // 
            // textBoxCPassword
            // 
            this.textBoxCPassword.Location = new System.Drawing.Point(375, 217);
            this.textBoxCPassword.MaxLength = 10;
            this.textBoxCPassword.Name = "textBoxCPassword";
            this.textBoxCPassword.PasswordChar = '*';
            this.textBoxCPassword.Size = new System.Drawing.Size(147, 20);
            this.textBoxCPassword.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(303, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Usuário *";
            // 
            // textBoxCUser
            // 
            this.textBoxCUser.Location = new System.Drawing.Point(375, 178);
            this.textBoxCUser.Name = "textBoxCUser";
            this.textBoxCUser.Size = new System.Drawing.Size(147, 20);
            this.textBoxCUser.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(326, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "CPF *";
            // 
            // buttonCadastrar
            // 
            this.buttonCadastrar.Location = new System.Drawing.Point(447, 297);
            this.buttonCadastrar.Name = "buttonCadastrar";
            this.buttonCadastrar.Size = new System.Drawing.Size(75, 23);
            this.buttonCadastrar.TabIndex = 5;
            this.buttonCadastrar.Text = "CADASTRAR";
            this.buttonCadastrar.UseVisualStyleBackColor = true;
            this.buttonCadastrar.Click += new System.EventHandler(this.buttonCadastrar_Click);
            // 
            // tabPageDeletarUsuario
            // 
            this.tabPageDeletarUsuario.Controls.Add(this.textBoxDPasswordAdm);
            this.tabPageDeletarUsuario.Controls.Add(this.buttonDDelete);
            this.tabPageDeletarUsuario.Controls.Add(this.textBoxDSearch);
            this.tabPageDeletarUsuario.Controls.Add(this.buttonDSearch);
            this.tabPageDeletarUsuario.Controls.Add(this.listViewDelete);
            this.tabPageDeletarUsuario.Controls.Add(this.label11);
            this.tabPageDeletarUsuario.Location = new System.Drawing.Point(4, 46);
            this.tabPageDeletarUsuario.Name = "tabPageDeletarUsuario";
            this.tabPageDeletarUsuario.Size = new System.Drawing.Size(760, 443);
            this.tabPageDeletarUsuario.TabIndex = 3;
            this.tabPageDeletarUsuario.Text = "DELETAR USUÁRIO";
            this.tabPageDeletarUsuario.UseVisualStyleBackColor = true;
            // 
            // textBoxDPasswordAdm
            // 
            this.textBoxDPasswordAdm.Location = new System.Drawing.Point(298, 347);
            this.textBoxDPasswordAdm.MaxLength = 10;
            this.textBoxDPasswordAdm.Name = "textBoxDPasswordAdm";
            this.textBoxDPasswordAdm.PasswordChar = '*';
            this.textBoxDPasswordAdm.Size = new System.Drawing.Size(142, 20);
            this.textBoxDPasswordAdm.TabIndex = 4;
            // 
            // buttonDDelete
            // 
            this.buttonDDelete.Location = new System.Drawing.Point(526, 346);
            this.buttonDDelete.Name = "buttonDDelete";
            this.buttonDDelete.Size = new System.Drawing.Size(87, 23);
            this.buttonDDelete.TabIndex = 5;
            this.buttonDDelete.Text = "DELETAR";
            this.buttonDDelete.UseVisualStyleBackColor = true;
            this.buttonDDelete.Click += new System.EventHandler(this.buttonDDelete_Click);
            // 
            // textBoxDSearch
            // 
            this.textBoxDSearch.Location = new System.Drawing.Point(127, 89);
            this.textBoxDSearch.MaxLength = 10;
            this.textBoxDSearch.Name = "textBoxDSearch";
            this.textBoxDSearch.Size = new System.Drawing.Size(100, 20);
            this.textBoxDSearch.TabIndex = 1;
            // 
            // buttonDSearch
            // 
            this.buttonDSearch.Location = new System.Drawing.Point(233, 88);
            this.buttonDSearch.Name = "buttonDSearch";
            this.buttonDSearch.Size = new System.Drawing.Size(87, 23);
            this.buttonDSearch.TabIndex = 2;
            this.buttonDSearch.Text = "PESQUISAR";
            this.buttonDSearch.UseVisualStyleBackColor = true;
            this.buttonDSearch.Click += new System.EventHandler(this.buttonDSearch_Click);
            // 
            // listViewDelete
            // 
            this.listViewDelete.BackColor = System.Drawing.SystemColors.Control;
            this.listViewDelete.Location = new System.Drawing.Point(127, 131);
            this.listViewDelete.Name = "listViewDelete";
            this.listViewDelete.Size = new System.Drawing.Size(486, 192);
            this.listViewDelete.TabIndex = 3;
            this.listViewDelete.UseCompatibleStateImageBehavior = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(124, 349);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Senha de Administrador: ";
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.buttonCUpdateConnection);
            this.tabPageConfig.Controls.Add(this.buttonCCheckConnection);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 46);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Size = new System.Drawing.Size(760, 443);
            this.tabPageConfig.TabIndex = 6;
            this.tabPageConfig.Text = "CONFIGURAÇÕES";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // buttonCUpdateConnection
            // 
            this.buttonCUpdateConnection.Location = new System.Drawing.Point(283, 251);
            this.buttonCUpdateConnection.Name = "buttonCUpdateConnection";
            this.buttonCUpdateConnection.Size = new System.Drawing.Size(188, 41);
            this.buttonCUpdateConnection.TabIndex = 2;
            this.buttonCUpdateConnection.Text = "Atualizar a Base de Dados";
            this.buttonCUpdateConnection.UseVisualStyleBackColor = true;
            this.buttonCUpdateConnection.Click += new System.EventHandler(this.buttonCUpdateConnection_Click);
            // 
            // buttonCCheckConnection
            // 
            this.buttonCCheckConnection.Location = new System.Drawing.Point(283, 151);
            this.buttonCCheckConnection.Name = "buttonCCheckConnection";
            this.buttonCCheckConnection.Size = new System.Drawing.Size(188, 41);
            this.buttonCCheckConnection.TabIndex = 1;
            this.buttonCCheckConnection.Text = "Verificar a Conexão";
            this.buttonCCheckConnection.UseVisualStyleBackColor = true;
            this.buttonCCheckConnection.Click += new System.EventHandler(this.buttonCCheckConnection_Click);
            // 
            // tabPageAjuda
            // 
            this.tabPageAjuda.Controls.Add(this.axAcroPDFAjuda);
            this.tabPageAjuda.Location = new System.Drawing.Point(4, 46);
            this.tabPageAjuda.Name = "tabPageAjuda";
            this.tabPageAjuda.Size = new System.Drawing.Size(760, 443);
            this.tabPageAjuda.TabIndex = 4;
            this.tabPageAjuda.Text = "AJUDA";
            this.tabPageAjuda.UseVisualStyleBackColor = true;
            // 
            // tabPageSobre
            // 
            this.tabPageSobre.Controls.Add(this.label20);
            this.tabPageSobre.Controls.Add(this.label19);
            this.tabPageSobre.Controls.Add(this.label18);
            this.tabPageSobre.Controls.Add(this.label17);
            this.tabPageSobre.Controls.Add(this.label16);
            this.tabPageSobre.Controls.Add(this.label15);
            this.tabPageSobre.Controls.Add(this.label14);
            this.tabPageSobre.Controls.Add(this.label13);
            this.tabPageSobre.Controls.Add(this.label12);
            this.tabPageSobre.Location = new System.Drawing.Point(4, 46);
            this.tabPageSobre.Name = "tabPageSobre";
            this.tabPageSobre.Size = new System.Drawing.Size(760, 443);
            this.tabPageSobre.TabIndex = 5;
            this.tabPageSobre.Text = "SOBRE";
            this.tabPageSobre.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(291, 338);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(187, 20);
            this.label20.TabIndex = 8;
            this.label20.Text = "Sergio Bocaro Rodrigues";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(637, 426);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(123, 17);
            this.label19.TabIndex = 7;
            this.label19.Text = "Versão 1.0 (2018)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(307, 372);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(155, 20);
            this.label18.TabIndex = 6;
            this.label18.Text = "Weber Felipe Zerbini";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(268, 304);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(233, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Ricardo Keitha Suzuki Goshima";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(255, 270);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(259, 20);
            this.label16.TabIndex = 4;
            this.label16.Text = "Jennifer Naomi Nagatani Nagamine";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(301, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(166, 20);
            this.label15.TabIndex = 3;
            this.label15.Text = "Isaías de França Leite";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(250, 188);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(269, 24);
            this.label14.TabIndex = 2;
            this.label14.Text = "INTEGRANTES DO GRUPO";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(39, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(691, 20);
            this.label13.TabIndex = 1;
            this.label13.Text = "Software de gerenciamento para um bar com jogos - customizado para \"Butecus Sta T" +
    "eresinha\".";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(210, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(349, 26);
            this.label12.TabIndex = 0;
            this.label12.Text = "PROJETO INTERDISCIPLINAR";
            // 
            // axAcroPDFAjuda
            // 
            this.axAcroPDFAjuda.Enabled = true;
            this.axAcroPDFAjuda.Location = new System.Drawing.Point(17, 15);
            this.axAcroPDFAjuda.Name = "axAcroPDFAjuda";
            this.axAcroPDFAjuda.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDFAjuda.OcxState")));
            this.axAcroPDFAjuda.Size = new System.Drawing.Size(727, 412);
            this.axAcroPDFAjuda.TabIndex = 0;
            // 
            // LoginSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 517);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginSystem";
            this.Text = "Butecu\'s Sta Tereza";
            this.tabControl1.ResumeLayout(false);
            this.tabPageLogin.ResumeLayout(false);
            this.tabPageLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageRedefinirAcesso.ResumeLayout(false);
            this.tabPageRedefinirAcesso.PerformLayout();
            this.tabPageCadastrarNovoUsuario.ResumeLayout(false);
            this.tabPageCadastrarNovoUsuario.PerformLayout();
            this.tabPageDeletarUsuario.ResumeLayout(false);
            this.tabPageDeletarUsuario.PerformLayout();
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageAjuda.ResumeLayout(false);
            this.tabPageSobre.ResumeLayout(false);
            this.tabPageSobre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFAjuda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLogin;
        private System.Windows.Forms.TabPage tabPageRedefinirAcesso;
        private System.Windows.Forms.TabPage tabPageCadastrarNovoUsuario;
        private System.Windows.Forms.TabPage tabPageDeletarUsuario;
        private System.Windows.Forms.TabPage tabPageAjuda;
        private System.Windows.Forms.TabPage tabPageSobre;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLPassword;
        private System.Windows.Forms.TextBox textBoxLUser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRPasswordAdm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRedefinir;
        private System.Windows.Forms.Button buttonCadastrar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxRCPF;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCCPF;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCPasswordAdm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxCPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCUser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.ListView listViewDelete;
        private System.Windows.Forms.TextBox textBoxDPasswordAdm;
        private System.Windows.Forms.Button buttonDDelete;
        private System.Windows.Forms.TextBox textBoxDSearch;
        private System.Windows.Forms.Button buttonDSearch;
        private System.Windows.Forms.Button buttonCUpdateConnection;
        private System.Windows.Forms.Button buttonCCheckConnection;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private AxAcroPDFLib.AxAcroPDF axAcroPDFAjuda;
    }
}