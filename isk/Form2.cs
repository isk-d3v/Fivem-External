using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using KeyAuth;
using Newtonsoft.Json.Linq;

namespace isk
{
    public partial class Form2 : Form
    {
        public static api KeyAuthApp = new api(
           name: "",
           ownerid: "",
           secret: "",
           version: "1.0"
         );

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();
            
            LoadColorSettings(); // Carrega a cor existente no settings.json
        }

       

        private void LoadColorSettings()
        {
            string jsonFilePath = "settings.json";
            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var jsonObject = JObject.Parse(jsonData);

                if (jsonObject["Color"] != null)
                {
                    // Carrega a cor existente no settings.json
                    Color hoverColor = ColorTranslator.FromHtml(jsonObject["Color"].ToString());

                    // Aplica a cor nos estados de "hover"
                    discordid.FocusedState.BorderColor = hoverColor;
                    discordid.HoverState.BorderColor = hoverColor;
                    loginbutton.HoverState.BorderColor = hoverColor;
                    loginbutton.HoverState.FillColor = hoverColor;
                }
            }
        }

      
        private void SaveColorSettings(Color color)
        {
            string jsonFilePath = "settings.json";
            string jsonData = File.Exists(jsonFilePath) ? File.ReadAllText(jsonFilePath) : "{}";

            var jsonObject = JObject.Parse(jsonData);

            jsonObject["Color"] = ColorTranslator.ToHtml(color);

            File.WriteAllText(jsonFilePath, jsonObject.ToString());
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(discordid.Text, discordid.Text);
            if (KeyAuthApp.response.success)
            {
                

                int posX = this.Location.X;
                int posY = this.Location.Y;

                Form1 Main = new Form1();
                Main.StartPosition = FormStartPosition.Manual;
                Main.Location = new Point(posX, posY);
                Main.Show();

                this.Hide();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void colorPickerButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;

                    discordid.BorderColor = selectedColor;
                    loginbutton.BorderColor = selectedColor;
                    loginbutton.FillColor = selectedColor;

                    SaveColorSettings(selectedColor);
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void discordid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
