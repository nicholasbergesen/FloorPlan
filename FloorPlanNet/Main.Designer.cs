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
            this.btnTestImage = new System.Windows.Forms.Button();
            this.btnFlip = new System.Windows.Forms.Button();
            this.btnTestData = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnIdentifyFloorPlans = new System.Windows.Forms.Button();
            this.lblCost = new System.Windows.Forms.Label();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnSkip = new System.Windows.Forms.Button();
            this.txtTrainingFiles = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnCreateNetwork = new System.Windows.Forms.Button();
            this.btnLoadNetwork = new System.Windows.Forms.Button();
            this.btnNotFloorPlan = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnFloorPlan = new System.Windows.Forms.Button();
            this.btnSaveNetwork = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtGenInputs = new System.Windows.Forms.TextBox();
            this.btnGenerateClassifier = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 196);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(653, 536);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTestImage);
            this.panel1.Controls.Add(this.btnFlip);
            this.panel1.Controls.Add(this.btnTestData);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnIdentifyFloorPlans);
            this.panel1.Controls.Add(this.lblCost);
            this.panel1.Controls.Add(this.btnLoadImage);
            this.panel1.Controls.Add(this.txtInput);
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
            this.panel1.Controls.Add(this.lblOutput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1390, 190);
            this.panel1.TabIndex = 11;
            // 
            // btnTestImage
            // 
            this.btnTestImage.Location = new System.Drawing.Point(618, 12);
            this.btnTestImage.Name = "btnTestImage";
            this.btnTestImage.Size = new System.Drawing.Size(75, 23);
            this.btnTestImage.TabIndex = 27;
            this.btnTestImage.Text = "Test Image";
            this.btnTestImage.UseVisualStyleBackColor = true;
            this.btnTestImage.Click += new System.EventHandler(this.btnTestImage_Click);
            // 
            // btnFlip
            // 
            this.btnFlip.Location = new System.Drawing.Point(699, 12);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(75, 23);
            this.btnFlip.TabIndex = 26;
            this.btnFlip.Text = "btnFlip";
            this.btnFlip.UseVisualStyleBackColor = true;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // btnTestData
            // 
            this.btnTestData.Location = new System.Drawing.Point(780, 12);
            this.btnTestData.Name = "btnTestData";
            this.btnTestData.Size = new System.Drawing.Size(107, 23);
            this.btnTestData.TabIndex = 25;
            this.btnTestData.Text = "Gen Test Data";
            this.btnTestData.UseVisualStyleBackColor = true;
            this.btnTestData.Click += new System.EventHandler(this.btnTestData_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(237, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 23);
            this.btnStop.TabIndex = 24;
            this.btnStop.Text = "Stop Identifying";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnIdentifyFloorPlans
            // 
            this.btnIdentifyFloorPlans.Location = new System.Drawing.Point(93, 69);
            this.btnIdentifyFloorPlans.Name = "btnIdentifyFloorPlans";
            this.btnIdentifyFloorPlans.Size = new System.Drawing.Size(138, 23);
            this.btnIdentifyFloorPlans.TabIndex = 23;
            this.btnIdentifyFloorPlans.Text = "Identify Floor Plans";
            this.btnIdentifyFloorPlans.UseVisualStyleBackColor = true;
            this.btnIdentifyFloorPlans.Click += new System.EventHandler(this.btnIdentifyFloorPlans_Click);
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(196, 160);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(29, 15);
            this.lblCost.TabIndex = 22;
            this.lblCost.Text = "cost";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(440, 126);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(87, 23);
            this.btnLoadImage.TabIndex = 21;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(12, 127);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(422, 23);
            this.txtInput.TabIndex = 20;
            this.txtInput.Text = "https://images.prop24.com/150782963";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(422, 23);
            this.progressBar1.TabIndex = 19;
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(319, 156);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(91, 23);
            this.btnSkip.TabIndex = 18;
            this.btnSkip.Text = "Next Image";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // txtTrainingFiles
            // 
            this.txtTrainingFiles.Location = new System.Drawing.Point(12, 11);
            this.txtTrainingFiles.Name = "txtTrainingFiles";
            this.txtTrainingFiles.Size = new System.Drawing.Size(422, 23);
            this.txtTrainingFiles.TabIndex = 17;
            this.txtTrainingFiles.Text = "C:\\working\\floor-plan\\FloorPlanNet\\TrainingData\\";
            // 
            // txtStatus
            // 
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam;
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
            this.btnCreateNetwork.Location = new System.Drawing.Point(1274, 12);
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
            this.btnLoadNetwork.Location = new System.Drawing.Point(1274, 41);
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
            this.btnTrain.Location = new System.Drawing.Point(12, 69);
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
            this.btnSaveNetwork.Location = new System.Drawing.Point(1274, 70);
            this.btnSaveNetwork.Name = "btnSaveNetwork";
            this.btnSaveNetwork.Size = new System.Drawing.Size(104, 23);
            this.btnSaveNetwork.TabIndex = 6;
            this.btnSaveNetwork.Text = "Save Network";
            this.btnSaveNetwork.UseVisualStyleBackColor = true;
            this.btnSaveNetwork.Click += new System.EventHandler(this.btnSaveNetwork_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(416, 160);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(66, 15);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "confidence";
            // 
            // txtGenInputs
            // 
            this.txtGenInputs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGenInputs.Location = new System.Drawing.Point(990, 226);
            this.txtGenInputs.MaxLength = 2147483646;
            this.txtGenInputs.Multiline = true;
            this.txtGenInputs.Name = "txtGenInputs";
            this.txtGenInputs.Size = new System.Drawing.Size(388, 506);
            this.txtGenInputs.TabIndex = 12;
            // 
            // btnGenerateClassifier
            // 
            this.btnGenerateClassifier.Location = new System.Drawing.Point(990, 197);
            this.btnGenerateClassifier.Name = "btnGenerateClassifier";
            this.btnGenerateClassifier.Size = new System.Drawing.Size(176, 23);
            this.btnGenerateClassifier.TabIndex = 13;
            this.btnGenerateClassifier.Text = "Generate Classifier HTML";
            this.btnGenerateClassifier.UseVisualStyleBackColor = true;
            this.btnGenerateClassifier.Click += new System.EventHandler(this.btnGenerateClassifier_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(735, 253);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 200);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 744);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnGenerateClassifier);
            this.Controls.Add(this.txtGenInputs);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Floor Plan Trainer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtTrainingFiles;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Button btnIdentifyFloorPlans;
        private System.Windows.Forms.TextBox txtGenInputs;
        private System.Windows.Forms.Button btnGenerateClassifier;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTestData;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Button btnTestImage;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

