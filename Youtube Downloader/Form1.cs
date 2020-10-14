using System;
using System.IO;
using System.Windows.Forms;
using VideoLibrary;

namespace Youtube_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Veuillez entré une vidéo.");
                return;
            }

            if (comboBox1.Text != "mp3" && comboBox1.Text != "mp4")
            {
                MessageBox.Show("Veuillez entré un format de vidéo.");
                return;
            }
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Ton chemin" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        richTextBox1.AppendText(Environment.NewLine + "Capture des information de la video...");

                        var youtube = YouTube.Default;
                        
                        var video = await youtube.GetVideoAsync(textBox1.Text);

                        if (comboBox1.Text == "mp3")
                        {
                            richTextBox1.AppendText(Environment.NewLine + "Téléchargement en cours...");
                            File.WriteAllBytes(fbd.SelectedPath + @"\" + video.FullName + ".mp3",
                                await video.GetBytesAsync());
                            richTextBox1.AppendText(Environment.NewLine + "Téléchargement fini dans: " + fbd.SelectedPath +
                                                    @"\" + video.FullName);
                        } else if (comboBox1.Text == "mp4")
                        {
                            richTextBox1.AppendText(Environment.NewLine + "Téléchargement en cours...");

                            File.WriteAllBytes(fbd.SelectedPath + @"\" + video.FullName + ".mp4", await video.GetBytesAsync());
                            richTextBox1.AppendText(Environment.NewLine + "Téléchargement fini dans: " + fbd.SelectedPath + @"\" + video.FullName);
                        }
                        else
                        {
                            MessageBox.Show("Veuillez entré un format de vidéo.");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Failed to decrypt URL");
                    }
                    


                    
                }
            }
        }
    }
}