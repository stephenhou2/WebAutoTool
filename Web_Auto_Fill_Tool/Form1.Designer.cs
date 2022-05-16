namespace Web_Auto_Fill_Tool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnAddExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_excute_result = new System.Windows.Forms.Label();
            this.btn_run = new System.Windows.Forms.Button();
            this.SetExplorer = new System.Windows.Forms.Button();
            this.ExplorerPath = new System.Windows.Forms.Label();
            this.ShowConsole = new System.Windows.Forms.CheckBox();
            this.ExcelSelectView = new System.Windows.Forms.CheckedListBox();
            this.BtnRemoveExcel = new System.Windows.Forms.Button();
            this.BtnSelectAll = new System.Windows.Forms.Button();
            this.BtnGenerateScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnAddExcel
            // 
            this.BtnAddExcel.Location = new System.Drawing.Point(25, 12);
            this.BtnAddExcel.Name = "BtnAddExcel";
            this.BtnAddExcel.Size = new System.Drawing.Size(135, 23);
            this.BtnAddExcel.TabIndex = 0;
            this.BtnAddExcel.Text = "添加Excel到清单中";
            this.BtnAddExcel.UseVisualStyleBackColor = true;
            this.BtnAddExcel.Click += new System.EventHandler(this.BtnAddExcelClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // label_excute_result
            // 
            this.label_excute_result.AutoSize = true;
            this.label_excute_result.Location = new System.Drawing.Point(23, 429);
            this.label_excute_result.Name = "label_excute_result";
            this.label_excute_result.Size = new System.Drawing.Size(0, 12);
            this.label_excute_result.TabIndex = 3;
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(649, 418);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(122, 23);
            this.btn_run.TabIndex = 6;
            this.btn_run.Text = "运行";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // SetExplorer
            // 
            this.SetExplorer.Location = new System.Drawing.Point(25, 364);
            this.SetExplorer.Name = "SetExplorer";
            this.SetExplorer.Size = new System.Drawing.Size(103, 23);
            this.SetExplorer.TabIndex = 7;
            this.SetExplorer.Text = "设置浏览器";
            this.SetExplorer.UseVisualStyleBackColor = true;
            this.SetExplorer.Click += new System.EventHandler(this.SetExplorer_Click);
            // 
            // ExplorerPath
            // 
            this.ExplorerPath.AutoSize = true;
            this.ExplorerPath.Location = new System.Drawing.Point(134, 369);
            this.ExplorerPath.Name = "ExplorerPath";
            this.ExplorerPath.Size = new System.Drawing.Size(0, 12);
            this.ExplorerPath.TabIndex = 8;
            // 
            // ShowConsole
            // 
            this.ShowConsole.AutoSize = true;
            this.ShowConsole.Location = new System.Drawing.Point(668, 371);
            this.ShowConsole.Name = "ShowConsole";
            this.ShowConsole.Size = new System.Drawing.Size(84, 16);
            this.ShowConsole.TabIndex = 9;
            this.ShowConsole.Text = "显示控制台";
            this.ShowConsole.UseVisualStyleBackColor = true;
            this.ShowConsole.CheckedChanged += new System.EventHandler(this.ShowConsole_CheckedChanged);
            // 
            // ExcelSelectView
            // 
            this.ExcelSelectView.FormattingEnabled = true;
            this.ExcelSelectView.Location = new System.Drawing.Point(25, 41);
            this.ExcelSelectView.Name = "ExcelSelectView";
            this.ExcelSelectView.Size = new System.Drawing.Size(746, 308);
            this.ExcelSelectView.TabIndex = 10;
            // 
            // BtnRemoveExcel
            // 
            this.BtnRemoveExcel.Location = new System.Drawing.Point(176, 12);
            this.BtnRemoveExcel.Name = "BtnRemoveExcel";
            this.BtnRemoveExcel.Size = new System.Drawing.Size(135, 23);
            this.BtnRemoveExcel.TabIndex = 11;
            this.BtnRemoveExcel.Text = "从清单中移除Excel";
            this.BtnRemoveExcel.UseVisualStyleBackColor = true;
            this.BtnRemoveExcel.Click += new System.EventHandler(this.BtnRemoveExcelClick);
            // 
            // BtnSelectAll
            // 
            this.BtnSelectAll.Location = new System.Drawing.Point(329, 12);
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectAll.TabIndex = 12;
            this.BtnSelectAll.Text = "全选";
            this.BtnSelectAll.UseVisualStyleBackColor = true;
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAllClick);
            // 
            // BtnGenerateScript
            // 
            this.BtnGenerateScript.Location = new System.Drawing.Point(429, 12);
            this.BtnGenerateScript.Name = "BtnGenerateScript";
            this.BtnGenerateScript.Size = new System.Drawing.Size(75, 23);
            this.BtnGenerateScript.TabIndex = 13;
            this.BtnGenerateScript.Text = "刷新脚本";
            this.BtnGenerateScript.UseVisualStyleBackColor = true;
            this.BtnGenerateScript.Click += new System.EventHandler(this.BtnGenerateScript_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnGenerateScript);
            this.Controls.Add(this.BtnSelectAll);
            this.Controls.Add(this.BtnRemoveExcel);
            this.Controls.Add(this.ExcelSelectView);
            this.Controls.Add(this.ShowConsole);
            this.Controls.Add(this.ExplorerPath);
            this.Controls.Add(this.SetExplorer);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.label_excute_result);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAddExcel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAddExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_excute_result;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button SetExplorer;
        private System.Windows.Forms.Label ExplorerPath;
        private System.Windows.Forms.CheckBox ShowConsole;
        private System.Windows.Forms.CheckedListBox ExcelSelectView;
        private System.Windows.Forms.Button BtnRemoveExcel;
        private System.Windows.Forms.Button BtnSelectAll;
        private System.Windows.Forms.Button BtnGenerateScript;
    }
}

