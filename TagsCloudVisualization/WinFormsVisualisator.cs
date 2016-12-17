using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.CloudBuilders;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization {
    public class WinFormsVisualisator : Form, ITagCloudVisualizator {
        public TagCloud Cloud { private get; set; }
        private TextBox FileInputField = new TextBox();
        private TextBox FileOutputField = new TextBox();
        private Button BuildCloudButton = new Button();
        private Button SaveImageButton = new Button();

        private IFileReader reader;
        private ICloudLayouter layouter;
        private IPreprocessor preprocessor;
        private IImageSaver saver;
        private ICloudBuilder cloudBuilder;

        public WinFormsVisualisator() {
            Size = new Size(200, 200);

            FileInputField.Text = "hungry_games_small.txt";

            BuildCloudButton.Text = "Build cloud";
            BuildCloudButton.Click += BuildCloudHandler;
            BuildCloudButton.Location = new Point(0, 25);

            FileOutputField.Text = "out file";
            FileOutputField.Location = new Point(0, 50);

            SaveImageButton.Text = "Save image";
            SaveImageButton.Click += SaveImageHandler;
            SaveImageButton.Location = new Point(0, 75);

            Controls.Add(BuildCloudButton);
            Controls.Add(FileInputField);
            Controls.Add(SaveImageButton);
            Controls.Add(FileOutputField);
        }

        protected override void OnPaint(PaintEventArgs e) {
            var graphics = e.Graphics;

            if (Cloud != null) {
                foreach(var tag in Cloud.Tags) {
                    var rect = new Rectangle(tag.Location, tag.Form);
                    TextRenderer.DrawText(graphics, tag.Text, tag.Font, rect, tag.Color);
                }
            }
        }

        public void Run() {
            Application.Run(this);
        }

        private void BuildCloudHandler(object sender, EventArgs e) {
            try {
                BuildCloud();
            }
            catch {
                MessageBox.Show("Can not create cloud!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SaveImageHandler(object sender, EventArgs e) {
            try {
                saver.SaveToFile(FileOutputField.Text, Cloud, new Size(Cloud.Width + layouter.Center.X,
                    Cloud.Height + layouter.Center.Y));
            }
            catch {
                MessageBox.Show("Can not save image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BuildCloud() {
            var words = reader.GetWords(FileInputField.Text);
            words = preprocessor.GetProcessedWords(words);

             Cloud = cloudBuilder.CreateCloud(words, layouter);
            var imageSize = new Size(layouter.CloudWidth + layouter.Center.X, layouter.CloudHeight + layouter.Center.Y);
            Size = new Size(Cloud.Width + layouter.Center.X, Cloud.Height + layouter.Center.Y);

            Invalidate();
        }

        public void SetSettings(IFileReader reader,
                                ICloudLayouter layouter, 
                                IPreprocessor preprocessor, 
                                IImageSaver saver,
                                ICloudBuilder builder) {
            this.reader = reader;
            this.layouter = layouter;
            this.preprocessor = preprocessor;
            this.saver = saver;
            this.cloudBuilder = builder;
        }
    }
}
