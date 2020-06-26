namespace FloorPlanNet
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSkip = new System.Windows.Forms.Button();
            this.txtTrainingFiles = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnCreateNetwork = new System.Windows.Forms.Button();
            this.btnLoadNetwork = new System.Windows.Forms.Button();
            this.btnNotFloorPlan = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnFloorPlan = new System.Windows.Forms.Button();
            this.btnSaveNetwork = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1380, 728);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnSkip);
            this.panel1.Controls.Add(this.txtTrainingFiles);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.btnCreateNetwork);
            this.panel1.Controls.Add(this.btnLoadNetwork);
            this.panel1.Controls.Add(this.btnNotFloorPlan);
            this.panel1.Controls.Add(this.btnTrain);
            this.panel1.Controls.Add(this.btnFloorPlan);
            this.panel1.Controls.Add(this.btnSaveNetwork);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1380, 190);
            this.panel1.TabIndex = 11;
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(196, 156);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(117, 23);
            this.btnSkip.TabIndex = 18;
            this.btnSkip.Text = "Skip (Next Image)";
            this.btnSkip.UseVisualStyleBackColor = true;
            // 
            // txtTrainingFiles
            // 
            this.txtTrainingFiles.Location = new System.Drawing.Point(12, 11);
            this.txtTrainingFiles.Name = "txtTrainingFiles";
            this.txtTrainingFiles.Size = new System.Drawing.Size(422, 23);
            this.txtTrainingFiles.TabIndex = 17;
            this.txtTrainingFiles.Text = "C:\\Users\\nicho\\Desktop\\ImageFloorPlanIdentifier\\FloorPlanNet\\TrainingData";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(893, 11);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(365, 168);
            this.txtStatus.TabIndex = 16;
            // 
            // btnCreateNetwork
            // 
            this.btnCreateNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNetwork.Location = new System.Drawing.Point(1264, 11);
            this.btnCreateNetwork.Name = "btnCreateNetwork";
            this.btnCreateNetwork.Size = new System.Drawing.Size(104, 23);
            this.btnCreateNetwork.TabIndex = 10;
            this.btnCreateNetwork.Text = "Create Network";
            this.btnCreateNetwork.UseVisualStyleBackColor = true;
            this.btnCreateNetwork.Click += new System.EventHandler(this.btnCreateNetwork_Click);
            // 
            // btnLoadNetwork
            // 
            this.btnLoadNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadNetwork.Location = new System.Drawing.Point(1264, 40);
            this.btnLoadNetwork.Name = "btnLoadNetwork";
            this.btnLoadNetwork.Size = new System.Drawing.Size(104, 23);
            this.btnLoadNetwork.TabIndex = 5;
            this.btnLoadNetwork.Text = "Load Network";
            this.btnLoadNetwork.UseVisualStyleBackColor = true;
            this.btnLoadNetwork.Click += new System.EventHandler(this.btnLoadNetwork_Click);
            // 
            // btnNotFloorPlan
            // 
            this.btnNotFloorPlan.Location = new System.Drawing.Point(93, 156);
            this.btnNotFloorPlan.Name = "btnNotFloorPlan";
            this.btnNotFloorPlan.Size = new System.Drawing.Size(97, 23);
            this.btnNotFloorPlan.TabIndex = 9;
            this.btnNotFloorPlan.Text = "Not Floor Plan";
            this.btnNotFloorPlan.UseVisualStyleBackColor = true;
            this.btnNotFloorPlan.Click += new System.EventHandler(this.btnNotFloorPlan_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(440, 12);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(75, 23);
            this.btnTrain.TabIndex = 1;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnFloorPlan
            // 
            this.btnFloorPlan.Location = new System.Drawing.Point(12, 156);
            this.btnFloorPlan.Name = "btnFloorPlan";
            this.btnFloorPlan.Size = new System.Drawing.Size(75, 23);
            this.btnFloorPlan.TabIndex = 8;
            this.btnFloorPlan.Text = "Floor Plan";
            this.btnFloorPlan.UseVisualStyleBackColor = true;
            this.btnFloorPlan.Click += new System.EventHandler(this.btnFloorPlan_Click);
            // 
            // btnSaveNetwork
            // 
            this.btnSaveNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNetwork.Location = new System.Drawing.Point(1264, 69);
            this.btnSaveNetwork.Name = "btnSaveNetwork";
            this.btnSaveNetwork.Size = new System.Drawing.Size(104, 23);
            this.btnSaveNetwork.TabIndex = 6;
            this.btnSaveNetwork.Text = "Save Network";
            this.btnSaveNetwork.UseVisualStyleBackColor = true;
            this.btnSaveNetwork.Click += new System.EventHandler(this.btnSaveNetwork_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "-1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(422, 23);
            this.progressBar1.TabIndex = 19;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 728);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Main";
            this.Text = "Floor Plan Trainer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreateNetwork;
        private System.Windows.Forms.Button btnLoadNetwork;
        private System.Windows.Forms.Button btnNotFloorPlan;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnFloorPlan;
        private System.Windows.Forms.Button btnSaveNetwork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtTrainingFiles;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

