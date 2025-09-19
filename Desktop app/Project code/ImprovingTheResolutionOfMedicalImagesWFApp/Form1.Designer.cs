using System.Threading.Tasks;

namespace ImprovingTheResolutionOfMedicalImagesWFApp
{
    partial class Form1
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
            this.btnConvert = new System.Windows.Forms.Button();
            this.picOutputImage = new System.Windows.Forms.PictureBox();
            this.picInputImage = new System.Windows.Forms.PictureBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.lblOutputWidth = new System.Windows.Forms.Label();
            this.lblOutputHeight = new System.Windows.Forms.Label();
            this.lblInputWidth = new System.Windows.Forms.Label();
            this.lblInputHeight = new System.Windows.Forms.Label();
            this.lblDeltaT = new System.Windows.Forms.Label();
            this.btnUpsampleStandard = new System.Windows.Forms.Button();
            this.btnUpsampleEdgePreserving = new System.Windows.Forms.Button();
            this.btnDownsampleStandard = new System.Windows.Forms.Button();
            this.btnDownsampleEdgePreserving = new System.Windows.Forms.Button();
            this.btnImproveReadabilityStandard = new System.Windows.Forms.Button();
            this.btnImproveReadabilityEdgePreserving = new System.Windows.Forms.Button();
            this.btnSegment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRecogniseCovid = new System.Windows.Forms.Button();
            this.btnRecogniseObject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picOutputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInputImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(700, 161);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(120, 49);
            this.btnConvert.TabIndex = 8;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // picOutputImage
            // 
            this.picOutputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOutputImage.Location = new System.Drawing.Point(839, 82);
            this.picOutputImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picOutputImage.Name = "picOutputImage";
            this.picOutputImage.Size = new System.Drawing.Size(600, 632);
            this.picOutputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOutputImage.TabIndex = 7;
            this.picOutputImage.TabStop = false;
            // 
            // picInputImage
            // 
            this.picInputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picInputImage.Location = new System.Drawing.Point(77, 82);
            this.picInputImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picInputImage.Name = "picInputImage";
            this.picInputImage.Size = new System.Drawing.Size(600, 632);
            this.picInputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInputImage.TabIndex = 6;
            this.picInputImage.TabStop = false;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadImage.Location = new System.Drawing.Point(700, 82);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(120, 49);
            this.btnLoadImage.TabIndex = 9;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.BtnLoadImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Input Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1099, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Output Image";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.Location = new System.Drawing.Point(700, 237);
            this.btnSaveAs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(120, 49);
            this.btnSaveAs.TabIndex = 13;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.BtnSaveAs_Click);
            // 
            // lblOutputWidth
            // 
            this.lblOutputWidth.AutoSize = true;
            this.lblOutputWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputWidth.Location = new System.Drawing.Point(853, 718);
            this.lblOutputWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutputWidth.Name = "lblOutputWidth";
            this.lblOutputWidth.Size = new System.Drawing.Size(0, 20);
            this.lblOutputWidth.TabIndex = 14;
            // 
            // lblOutputHeight
            // 
            this.lblOutputHeight.AutoSize = true;
            this.lblOutputHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputHeight.Location = new System.Drawing.Point(1128, 718);
            this.lblOutputHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutputHeight.Name = "lblOutputHeight";
            this.lblOutputHeight.Size = new System.Drawing.Size(0, 20);
            this.lblOutputHeight.TabIndex = 15;
            // 
            // lblInputWidth
            // 
            this.lblInputWidth.AutoSize = true;
            this.lblInputWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputWidth.Location = new System.Drawing.Point(77, 718);
            this.lblInputWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInputWidth.Name = "lblInputWidth";
            this.lblInputWidth.Size = new System.Drawing.Size(0, 20);
            this.lblInputWidth.TabIndex = 16;
            // 
            // lblInputHeight
            // 
            this.lblInputHeight.AutoSize = true;
            this.lblInputHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputHeight.Location = new System.Drawing.Point(373, 718);
            this.lblInputHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInputHeight.Name = "lblInputHeight";
            this.lblInputHeight.Size = new System.Drawing.Size(0, 20);
            this.lblInputHeight.TabIndex = 17;
            // 
            // lblDeltaT
            // 
            this.lblDeltaT.AutoSize = true;
            this.lblDeltaT.Location = new System.Drawing.Point(698, 733);
            this.lblDeltaT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeltaT.Name = "lblDeltaT";
            this.lblDeltaT.Size = new System.Drawing.Size(42, 13);
            this.lblDeltaT.TabIndex = 21;
            this.lblDeltaT.Text = "DeltaT:";
            this.lblDeltaT.Visible = false;
            // 
            // btnUpsampleStandard
            // 
            this.btnUpsampleStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpsampleStandard.Location = new System.Drawing.Point(700, 343);
            this.btnUpsampleStandard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpsampleStandard.Name = "btnUpsampleStandard";
            this.btnUpsampleStandard.Size = new System.Drawing.Size(120, 49);
            this.btnUpsampleStandard.TabIndex = 22;
            this.btnUpsampleStandard.Text = "Upsample (Standard)";
            this.btnUpsampleStandard.UseVisualStyleBackColor = true;
            this.btnUpsampleStandard.Click += new System.EventHandler(this.btnUpsampleStandard_Click);
            // 
            // btnUpsampleEdgePreserving
            // 
            this.btnUpsampleEdgePreserving.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpsampleEdgePreserving.Location = new System.Drawing.Point(700, 396);
            this.btnUpsampleEdgePreserving.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpsampleEdgePreserving.Name = "btnUpsampleEdgePreserving";
            this.btnUpsampleEdgePreserving.Size = new System.Drawing.Size(120, 49);
            this.btnUpsampleEdgePreserving.TabIndex = 23;
            this.btnUpsampleEdgePreserving.Text = "Upsample (Edge Preserving)";
            this.btnUpsampleEdgePreserving.UseVisualStyleBackColor = true;
            this.btnUpsampleEdgePreserving.Click += new System.EventHandler(this.btnUpsampleEdgePreserving_Click);
            // 
            // btnDownsampleStandard
            // 
            this.btnDownsampleStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownsampleStandard.Location = new System.Drawing.Point(700, 450);
            this.btnDownsampleStandard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDownsampleStandard.Name = "btnDownsampleStandard";
            this.btnDownsampleStandard.Size = new System.Drawing.Size(120, 49);
            this.btnDownsampleStandard.TabIndex = 24;
            this.btnDownsampleStandard.Text = "Downsample (Standard)";
            this.btnDownsampleStandard.UseVisualStyleBackColor = true;
            this.btnDownsampleStandard.Click += new System.EventHandler(this.btnDownsampleStandard_Click);
            // 
            // btnDownsampleEdgePreserving
            // 
            this.btnDownsampleEdgePreserving.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownsampleEdgePreserving.Location = new System.Drawing.Point(700, 504);
            this.btnDownsampleEdgePreserving.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDownsampleEdgePreserving.Name = "btnDownsampleEdgePreserving";
            this.btnDownsampleEdgePreserving.Size = new System.Drawing.Size(120, 49);
            this.btnDownsampleEdgePreserving.TabIndex = 25;
            this.btnDownsampleEdgePreserving.Text = "Downsample (Edge Preserving)";
            this.btnDownsampleEdgePreserving.UseVisualStyleBackColor = true;
            this.btnDownsampleEdgePreserving.Click += new System.EventHandler(this.btnDownsampleEdgePreserving_Click);
            // 
            // btnImproveReadabilityStandard
            // 
            this.btnImproveReadabilityStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImproveReadabilityStandard.Location = new System.Drawing.Point(700, 557);
            this.btnImproveReadabilityStandard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnImproveReadabilityStandard.Name = "btnImproveReadabilityStandard";
            this.btnImproveReadabilityStandard.Size = new System.Drawing.Size(120, 49);
            this.btnImproveReadabilityStandard.TabIndex = 26;
            this.btnImproveReadabilityStandard.Text = "Improve Readability (Standard)";
            this.btnImproveReadabilityStandard.UseVisualStyleBackColor = true;
            this.btnImproveReadabilityStandard.Click += new System.EventHandler(this.btnImproveReadabilityStandard_Click);
            // 
            // btnImproveReadabilityEdgePreserving
            // 
            this.btnImproveReadabilityEdgePreserving.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImproveReadabilityEdgePreserving.Location = new System.Drawing.Point(700, 611);
            this.btnImproveReadabilityEdgePreserving.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnImproveReadabilityEdgePreserving.Name = "btnImproveReadabilityEdgePreserving";
            this.btnImproveReadabilityEdgePreserving.Size = new System.Drawing.Size(120, 49);
            this.btnImproveReadabilityEdgePreserving.TabIndex = 27;
            this.btnImproveReadabilityEdgePreserving.Text = "Improve Readability (Edge Preserving)";
            this.btnImproveReadabilityEdgePreserving.UseVisualStyleBackColor = true;
            this.btnImproveReadabilityEdgePreserving.Click += new System.EventHandler(this.btnImproveReadabilityEdgePreserving_Click);
            // 
            // btnSegment
            // 
            this.btnSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSegment.Location = new System.Drawing.Point(700, 665);
            this.btnSegment.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSegment.Name = "btnSegment";
            this.btnSegment.Size = new System.Drawing.Size(120, 49);
            this.btnSegment.TabIndex = 28;
            this.btnSegment.Text = "Segment";
            this.btnSegment.UseVisualStyleBackColor = true;
            this.btnSegment.Click += new System.EventHandler(this.btnSegment_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(735, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 29;
            // 
            // btnRecogniseCovid
            // 
            this.btnRecogniseCovid.Location = new System.Drawing.Point(585, 744);
            this.btnRecogniseCovid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRecogniseCovid.Name = "btnRecogniseCovid";
            this.btnRecogniseCovid.Size = new System.Drawing.Size(92, 41);
            this.btnRecogniseCovid.TabIndex = 30;
            this.btnRecogniseCovid.Text = "AI Recognise Covid 19 ";
            this.btnRecogniseCovid.UseVisualStyleBackColor = true;
            this.btnRecogniseCovid.Click += new System.EventHandler(this.btnRecogniseCovid_Click);
            // 
            // btnRecogniseObject
            // 
            this.btnRecogniseObject.Location = new System.Drawing.Point(77, 749);
            this.btnRecogniseObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRecogniseObject.Name = "btnRecogniseObject";
            this.btnRecogniseObject.Size = new System.Drawing.Size(75, 36);
            this.btnRecogniseObject.TabIndex = 32;
            this.btnRecogniseObject.Text = "Recognise Object";
            this.btnRecogniseObject.UseVisualStyleBackColor = true;
            this.btnRecogniseObject.Click += new System.EventHandler(this.btnRecogniseObject_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1396, 803);
            this.Controls.Add(this.btnRecogniseObject);
            this.Controls.Add(this.btnRecogniseCovid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSegment);
            this.Controls.Add(this.btnImproveReadabilityEdgePreserving);
            this.Controls.Add(this.btnImproveReadabilityStandard);
            this.Controls.Add(this.btnDownsampleEdgePreserving);
            this.Controls.Add(this.btnDownsampleStandard);
            this.Controls.Add(this.btnUpsampleEdgePreserving);
            this.Controls.Add(this.btnUpsampleStandard);
            this.Controls.Add(this.lblDeltaT);
            this.Controls.Add(this.lblInputHeight);
            this.Controls.Add(this.lblInputWidth);
            this.Controls.Add(this.lblOutputHeight);
            this.Controls.Add(this.lblOutputWidth);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.picOutputImage);
            this.Controls.Add(this.picInputImage);
            this.Location = new System.Drawing.Point(54, 83);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Improving the Resolution and Readability of Two Dimentional Medical Images Throug" +
    "h Mathematical Transformations";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.picOutputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInputImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.PictureBox picOutputImage;
        private System.Windows.Forms.PictureBox picInputImage;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Label lblOutputWidth;
        private System.Windows.Forms.Label lblOutputHeight;
        private System.Windows.Forms.Label lblInputWidth;
        private System.Windows.Forms.Label lblInputHeight;
        private System.Windows.Forms.Label lblDeltaT;
        private System.Windows.Forms.Button btnUpsampleStandard;
        private System.Windows.Forms.Button btnUpsampleEdgePreserving;
        private System.Windows.Forms.Button btnDownsampleStandard;
        private System.Windows.Forms.Button btnDownsampleEdgePreserving;
        private System.Windows.Forms.Button btnImproveReadabilityStandard;
        private System.Windows.Forms.Button btnImproveReadabilityEdgePreserving;
        private System.Windows.Forms.Button btnSegment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRecogniseCovid;
        private System.Windows.Forms.Button btnRecogniseObject;
    }
}

