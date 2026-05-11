namespace ok_backup_code;

public partial class Form1 : Form
{
    private string pathArchivio;
    private string pathDir;
    const int DIMENSIONI_PRINCIPALI=150;
    const int OFSET = 80;
    const int CENTRO_X = 400;
    Button crea = new Button();
    Button ripristina = new Button();
    Button elimina = new Button();
    Button inizia = new Button();
    Button selettoreArchivio = new Button();
    FolderBrowserDialog archivio = new FolderBrowserDialog();
    public Form1()
    {
        //rendo la finestra non ridimensionabile
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        initPulsanti();

        Controls.Add(crea);
        Controls.Add(ripristina);
        Controls.Add(elimina);

        crea.Click += CreaBackUp_Click;
        ripristina.Click+=RipristinaCartella_Click;

        InitializeComponent();
    }
    public void initPulsanti()
    {
        crea.Height = DIMENSIONI_PRINCIPALI;
        crea.Width = DIMENSIONI_PRINCIPALI;
        crea.Text = "Fai Back Up";
        crea.Left=100;

        ripristina.Height = DIMENSIONI_PRINCIPALI;
        ripristina.Width = DIMENSIONI_PRINCIPALI;
        ripristina.Text = "Rispistina cartella";
        ripristina.Left=350;

        elimina.Height = DIMENSIONI_PRINCIPALI;
        elimina.Width = DIMENSIONI_PRINCIPALI;
        elimina.Text = "Elimina Back Up";
        elimina.Left=600;

        
        inizia.BackColor=Color.Blue;
        inizia.Text="Inizia Back Up";
        inizia.Height = 40;
        inizia.Width = 150;
        inizia.ForeColor = Color.White;

        selettoreArchivio.Text="Selezione Archivio";
        selettoreArchivio.Height = 40;
        selettoreArchivio.Width = 150;
        selettoreArchivio.Top = 300;

        archivio.Description = "Selezione archivio in cui salvare il back up";
        archivio.UseDescriptionForTitle = true;
    }
    public void CreaBackUp_Click(object sender, EventArgs e)
    {
        this.Controls.Clear();
        Controls.Add(inizia);
        Controls.Add(selettoreArchivio);

        selettoreArchivio.Click += Selezione_Click;
    }
    public void RipristinaCartella_Click(object sender, EventArgs e)
    {
        
    }

    //selezzione dell' archivio con Esplora File
    public void Selezione_Click(object sender, EventArgs e)
    {
        
    }
}
