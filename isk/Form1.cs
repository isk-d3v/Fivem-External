using ImGuiNET;
using mydick;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isk
{
    public partial class Form1 : Form
    {
        private const string SettingsFile = "settings.json";
        private Point initialPanelContenedorLocation;
        private getpic _profilePicHandler;
        
        public Form1()
        {
            InitializeComponent();
            
            this.initialPanelContenedorLocation = AimPanel.Location;
            this.Width = 590;
            this.Height = 400;
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AimPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.VisualPainel.Visible = false;
            this.ConfigPanel.Visible = false;
            AimPanel.Location = initialPanelContenedorLocation;
            this.AimPanel.Visible = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.VisualPainel.Visible = true;
            this.ConfigPanel.Visible = false;
            VisualPainel.Location = initialPanelContenedorLocation;
            this.AimPanel.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.VisualPainel.Visible = false;
            this.ConfigPanel.Visible = true;
            ConfigPanel.Location = initialPanelContenedorLocation;
            this.AimPanel.Visible = false;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool isHwidVisible = false;

        private async void Form1_Load(object sender, EventArgs e)
        {
            _profilePicHandler = new getpic();
            await LoadProfilePictureAsync();
            
            LoadSavedColor();

            label25.Text = $" {Form2.KeyAuthApp.user_data.username}";
            //label26.Text = $" {UnixTimeToDateTime(long.Parse(Form2.KeyAuthApp.user_data.subscriptions[0].expiry))}";
            label27.Text = "Clique aqui para mostrar HWID";
            label27.Click += new EventHandler(label27_Click);

            label26.Text = ExpiryDaysLeft();
        }


        private void label27_Click(object sender, EventArgs e)
        {
            if (isHwidVisible)
            {
                label27.Text = "Clique aqui para mostrar HWID";
                isHwidVisible = false;
            }
            else
            {
                label27.Text = Form2.KeyAuthApp.user_data.hwid;
                isHwidVisible = true;
            }
        }


        public static bool SubExist(string name, int len)
        {
            for (var i = 0; i < len; i++)
            {
                if (Form2.KeyAuthApp.user_data.subscriptions[i].subscription == name)
                {
                    return true;
                }
            }
            return false;
        }
        public string ExpiryDaysLeft()
        {
            try
            {
                DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                long expiryUnix;
                if (!long.TryParse(Form2.KeyAuthApp.user_data.subscriptions[0].expiry, out expiryUnix))
                {
                    return "Erro na conversão do tempo Unix";
                }

                DateTime expiryDate = baseDate.AddSeconds(expiryUnix).ToLocalTime();

                TimeSpan difference = expiryDate - DateTime.Now;

                if (difference.TotalDays < 0)
                {
                    return "A expiração já passou";
                }
                int daysLeft = (int)Math.Floor(difference.TotalDays);
                return $"{daysLeft} Days";
            }
            catch (Exception ex)
            {
                return $"Erro ao calcular: {ex.Message}";
            }
        }





        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }
        private async Task LoadProfilePictureAsync()
        {
            string avatarUrl = await _profilePicHandler.GetDiscordProfilePictureUrl();

            if (!string.IsNullOrEmpty(avatarUrl))
            {
                System.Drawing.Image profileImage = await _profilePicHandler.LoadImageFromUrl(avatarUrl);
                if (profileImage != null)
                {
                    
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    ApplyColor(colorDialog.Color);
                    SaveColor(colorDialog.Color);
                }
            }
        }

        private void ApplyColor(Color color)
        {
            // Buttons
            guna2Button5.FillColor = color;
            guna2Button5.BorderColor = color;
            guna2Button1.CheckedState.BorderColor = color;
            guna2Button1.CheckedState.CustomBorderColor = color;
            guna2Button1.CheckedState.BorderColor = color;
            guna2Button2.CheckedState.BorderColor = color;
            guna2Button2.CheckedState.CustomBorderColor = color;
            guna2Button2.CheckedState.BorderColor = color;
            guna2Button3.CheckedState.BorderColor = color;
            guna2Button3.CheckedState.CustomBorderColor = color;
            guna2Button3.CheckedState.BorderColor = color;
            guna2Button1.CheckedState.FillColor = color;
            guna2Button2.CheckedState.FillColor = color;
            guna2Button3.CheckedState.FillColor = color;
            guna2Button4.FillColor = color;
            guna2Button4.BorderColor = color;

            // Panels
            guna2Panel3.CustomBorderColor = color;
            guna2Panel4.CustomBorderColor = color;
            guna2Panel2.CustomBorderColor = color;
            guna2Panel5.CustomBorderColor = color;
            guna2Panel7.CustomBorderColor = color;
            

            // Check Button
            Aimbot.CheckedState.FillColor = color;
            guna2CustomCheckBox1.CheckedState.FillColor = color;
            guna2CustomCheckBox2.CheckedState.FillColor = color;
            guna2CustomCheckBox5.CheckedState.FillColor = color;
            guna2CustomCheckBox4.CheckedState.FillColor = color;
            guna2CustomCheckBox6.CheckedState.FillColor = color;
            guna2CustomCheckBox7.CheckedState.FillColor = color;
            guna2CustomCheckBox3.CheckedState.FillColor = color;
            guna2CustomCheckBox8.CheckedState.FillColor = color;
            Aimbot.CheckedState.BorderColor = color;
            guna2CustomCheckBox1.CheckedState.BorderColor = color;
            guna2CustomCheckBox2.CheckedState.BorderColor = color;
            guna2CustomCheckBox5.CheckedState.BorderColor = color;
            guna2CustomCheckBox4.CheckedState.BorderColor = color;
            guna2CustomCheckBox6.CheckedState.BorderColor = color;
            guna2CustomCheckBox7.CheckedState.BorderColor = color;
            guna2CustomCheckBox3.CheckedState.BorderColor = color;
            guna2CustomCheckBox8.CheckedState.BorderColor = color;
        }

        private void SaveColor(Color color)
        {
            var settings = new
            {
                Color = ColorTranslator.ToHtml(color)
            };

            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SettingsFile, json);
        }

        private void LoadSavedColor()
        {
            if (File.Exists(SettingsFile))
            {
                string json = File.ReadAllText(SettingsFile);
                dynamic settings = JsonConvert.DeserializeObject(json);
                string colorHtml = settings.Color;
                Color color = ColorTranslator.FromHtml(colorHtml);

                ApplyColor(color);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Variável para armazenar a cor selecionada
            Vector4 color = new Vector4(1, 0, 0, 1); // Inicialmente vermelho

            // Inicializa o ImGui (caso ainda não tenha sido inicializado)
            ImGui.CreateContext();
            ImGui.StyleColorsDark();

            // Cria uma janela para o seletor de cores
            ImGui.Begin("Color Picker");

            // Mostra o seletor de cores
            if (ImGui.ColorPicker4("Pick a color", ref color))
            {
                // Se a cor for alterada, você pode fazer algo com a nova cor
                // Exemplo: alterar a cor de fundo de um componente ou salvar a cor
                Color newColor = Color.FromArgb(
                    (int)(color.W * 255),
                    (int)(color.X * 255),
                    (int)(color.Y * 255),
                    (int)(color.Z * 255)
                );

                // Aplicar a cor a um botão ou outro elemento
                guna2Button4.FillColor = newColor;
            }

            ImGui.End();
        }

        private void guna2CustomCheckBox8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConfigPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
