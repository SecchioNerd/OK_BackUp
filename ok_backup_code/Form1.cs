using prova;
using System.Drawing;

namespace ok_backup_code;

public partial class Form1 : Form
{
    GestioneFS gestionefs  =new GestioneFS();
    private string pathArchivio;
    private string pathDir;
    private string nameBackUp;
    const int DIMENSIONI_PRINCIPALI=150;
    const int OFSET = 80;
    const int CENTRO_X = 325;
    const int CENTRO_Y = 200;

    Archivio arcSelezionato;
    Button crea = new Button();
    Button ripristina = new Button();
    Button elimina = new Button();
    Button inizia = new Button();
    Button indietro = new Button();
    Button sfogliaDir = new Button();
    Button sfogliaArchivio = new Button();

    TextBox nomeBackup = new TextBox();
    CheckBox sovrascrittura = new CheckBox();
    ComboBox archivi = new ComboBox();
    ComboBox registriInArchivio = new ComboBox();

    PictureBox logoOk = new PictureBox();
    PictureBox logoOkCrea = new PictureBox();
    PictureBox logoOkRipristina = new PictureBox();
    PictureBox logoOkElimina = new PictureBox();

    bool isIndietro = false;
    bool isCartellaSelezionata =false;
    bool isArchivioSelezionato = false;

    public Form1()
    {
        InitializeComponent();
        //rendo la finestra non ridimensionabile
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;


        initPulsanti();

        menu();
        menu();   
    }

    public void menu()
    {
        isIndietro = false;
        this.Controls.Clear();
        Controls.Add(crea);
        Controls.Add(ripristina);
        Controls.Add(elimina);
        Controls.Add(logoOk);
        
        crea.Click += CreaBackUp_Click;
        ripristina.Click+=RipristinaCartella_Click;
        elimina.Click+=EliminaBackUp_Click;
    }

    public void initPulsanti()
    {
        arcSelezionato = new Archivio();

        logoOk.Image = Image.FromFile("C:\\Users\\YourUser\\OneDrive\\Desktop\\OK_BackUp\\ok_backup_code\\logoOKBackupRGBA.png");
        logoOk.Height = 200;
        logoOk.Width = 500;
        logoOk.SizeMode = PictureBoxSizeMode.Zoom;
        logoOk.Top = 10;
        logoOk.Left=150;
        
        logoOkCrea.Image = Image.FromFile("C:\\Users\\YourUser\\OneDrive\\Desktop\\OK_BackUp\\ok_backup_code\\loghiOkCrea.png");
        logoOkCrea.Height = 120;
        logoOkCrea.Width = 500;
        logoOkCrea.SizeMode = PictureBoxSizeMode.Zoom;
        logoOkCrea.Top = 10;
        logoOkCrea.Left=150;

        logoOkRipristina.Image = Image.FromFile("C:\\Users\\YourUser\\OneDrive\\Desktop\\OK_BackUp\\ok_backup_code\\loghiOkRipristina.png");
        logoOkRipristina.Height = 120;
        logoOkRipristina.Width = 500;
        logoOkRipristina.SizeMode = PictureBoxSizeMode.Zoom;
        logoOkRipristina.Top = 10;
        logoOkRipristina.Left=130;

        logoOkElimina.Image = Image.FromFile("C:\\Users\\YourUser\\OneDrive\\Desktop\\OK_BackUp\\ok_backup_code\\loghiOkElimina.png");
        logoOkElimina.Height = 120;
        logoOkElimina.Width = 500;
        logoOkElimina.SizeMode = PictureBoxSizeMode.Zoom;
        logoOkElimina.Top = 10;
        logoOkElimina.Left=130;

        crea.Height = DIMENSIONI_PRINCIPALI;
        crea.Width = DIMENSIONI_PRINCIPALI;
        crea.Text = "Fai Back Up";
        crea.Left=CENTRO_X-180;
        crea.Top = CENTRO_Y;

        ripristina.Height = DIMENSIONI_PRINCIPALI;
        ripristina.Width = DIMENSIONI_PRINCIPALI;
        ripristina.Text = "Rispistina cartella";
        ripristina.Left=CENTRO_X;
        ripristina.Top = CENTRO_Y;

        elimina.Height = DIMENSIONI_PRINCIPALI;
        elimina.Width = DIMENSIONI_PRINCIPALI;
        elimina.Text = "Elimina Back Up";
        elimina.Left=CENTRO_X+180;
        elimina.Top = CENTRO_Y;

        
        inizia.BackColor=Color.Blue;
        inizia.Text="Inizia";
        inizia.Height = 40;
        inizia.Width = 150;
        inizia.Left = 400;
        inizia.Top = 350;
        inizia.ForeColor = Color.White;
        
        indietro.Text="Indietro";
        indietro.Height = 40;
        indietro.Width = 150;
        indietro.Left = 200;
        indietro.Top = 350;

        sfogliaDir.Text = "Seleziona Directory di cui fare il backup";
        sfogliaDir.Width = 550;
        sfogliaDir.Height = 40;

        sfogliaArchivio.Text = "Seleziona archivio su cui salvare il backup";
        sfogliaArchivio.Width = 550;
        sfogliaArchivio.Height = 40;

        nomeBackup.Height = 40;
        nomeBackup.Width = 550;
        nomeBackup.Text = "nome backup";

        archivi.Height = 40;
        archivi.Width = 550;
        archivi.Text = "Seleziona archivio";
        archivi.DropDownStyle = ComboBoxStyle.DropDownList;

        registriInArchivio.Height = 40;
        registriInArchivio.Width = 550;
        registriInArchivio.Text = "Seleziona BackUp";
        registriInArchivio.DropDownStyle = ComboBoxStyle.DropDownList;

        sovrascrittura.Height = 25;
        sovrascrittura.Width = 25;
    }
    public void CreaBackUp_Click(object sender, EventArgs e)
    {
        this.Controls.Clear();
        int distanzaTop = 150;

        Label [] labels = new Label[3];
        labels[0]=new Label();
        labels[1]=new Label();
        labels[2]=new Label();

        labels[0].Text = "Archivio: ";
        labels[1].Text = "Cartella: ";
        labels[2].Text = "Nome Backup: ";

        for(int i=0;i<3;i++)
        {
            labels[i].Location = new Point(10, distanzaTop);
            labels[i].Size = new Size(120,40);
            this.Controls.Add(labels[i]);

            switch (i)
            {
                case 0:
                    sfogliaArchivio.Left = 140;
                    sfogliaArchivio.Top = distanzaTop;
                    Controls.Add(sfogliaArchivio);
                    break;
                
                case 1:
                    sfogliaDir.Left = 140;
                    sfogliaDir.Top = distanzaTop;
                    Controls.Add(sfogliaDir);
                    break;
                
                case 2:
                    nomeBackup.Left = 140;
                    nomeBackup.Top = distanzaTop;
                    Controls.Add(nomeBackup);
                    break;
            }

            distanzaTop+=60;
        }

        Controls.Add(inizia);
        Controls.Add(indietro);
        Controls.Add(logoOkCrea);
        //Controls.Add(sfogliaArchivio);

        sfogliaArchivio.Click += SelezionaArchivio_Click;
        sfogliaDir.Click += SelezionaCartella_Click;
        inizia.Click+=IniziaBackup_Click;
        
    }
    public void RipristinaCartella_Click(object sender, EventArgs e)
    {
        isArchivioSelezionato = false;
        isCartellaSelezionata = false;
        this.Controls.Clear();
        int distanzaTop = 150;

        List<Archivio> archiviList = GestioneFS.stringToListArchivio();
        archivi.Items.Clear();
        registriInArchivio.Items.Clear();
        
        foreach (Archivio a in archiviList)
        {
            archivi.Items.Add(a.nome);
        }

        Label [] labels = new Label[3];
        labels[0]=new Label();
        labels[1]=new Label();
        labels[2]=new Label();

        labels[0].Text = "Archivio:";
        labels[1].Text = "Nome Backup:";
        labels[2].Text = "Vuoi sovrascrivere l' originale";

        labels[0].Size = new Size(120,40);
        labels[1].Size = new Size(120,40);
        labels[2].Size = new Size(300,40);
        
        for(int i=0;i<3;i++)
        {
            if(i!=2)
                labels[i].Location = new Point(10, distanzaTop);
            else
                labels[i].Location = new Point(210, distanzaTop);
            this.Controls.Add(labels[i]);

            switch (i)
            {
                case 0:
                    archivi.Left = 140;
                    archivi.Top = distanzaTop;
                    Controls.Add(archivi);
                    break;
                
                case 1:
                    registriInArchivio.Left = 140;
                    registriInArchivio.Top = distanzaTop;
                    Controls.Add(registriInArchivio);
                    break;
                
                case 2:
                    sovrascrittura.Left = 180;
                    sovrascrittura.Top = distanzaTop;
                    Controls.Add(sovrascrittura);
                    break;
            }

            distanzaTop+=60;
        }

        Controls.Add(inizia);
        Controls.Add(indietro);
        Controls.Add(logoOkRipristina);
        inizia.Click += IniziaRipristino_CLick;
        indietro.Click+=Indietro_Click;    
        archivi.SelectedIndexChanged+=SelezionaArchivio_SelectedIndexChanged;  
        registriInArchivio.SelectedIndexChanged+=selezionaCartella_SelectedIndexChanged;
        
    }

    public void EliminaBackUp_Click(object sender, EventArgs e)
    {
    
        isArchivioSelezionato = false;
        isCartellaSelezionata = false;
        this.Controls.Clear();
        int distanzaTop = 160;

        List<Archivio> archiviList = GestioneFS.stringToListArchivio();
        archivi.Items.Clear();
        registriInArchivio.Items.Clear();
        
        foreach (Archivio a in archiviList)
        {
            archivi.Items.Add(a.nome);
        }

        Label [] labels = new Label[2];
        labels[0]=new Label();
        labels[1]=new Label();

        labels[0].Text = "Archivio:";
        labels[1].Text = "Nome Backup:";

        labels[0].Size = new Size(120,40);
        labels[1].Size = new Size(120,40);
        
        for(int i=0;i<2;i++)
        {
            if(i!=2)
                labels[i].Location = new Point(10, distanzaTop);
            else
                labels[i].Location = new Point(210, distanzaTop);
            this.Controls.Add(labels[i]);

            switch (i)
            {
                case 0:
                    archivi.Left = 140;
                    archivi.Top = distanzaTop;
                    Controls.Add(archivi);
                    break;
                
                case 1:
                    registriInArchivio.Left = 140;
                    registriInArchivio.Top = distanzaTop;
                    Controls.Add(registriInArchivio);
                    break;
            }

            distanzaTop+=60;
        }

        Controls.Add(inizia);
        Controls.Add(indietro);
        Controls.Add(logoOkElimina);
        inizia.Click += IniziaEliminazione_Click;
        indietro.Click+=Indietro_Click;    
        archivi.SelectedIndexChanged+=SelezionaArchivio_SelectedIndexChanged;  
        registriInArchivio.SelectedIndexChanged+=selezionaCartella_SelectedIndexChanged;
        
    }

    public void Indietro_Click(object sender, EventArgs e)
    {
        this.Controls.Clear();
        isIndietro = true;
        return;
    }

    //selezzione dell' archivio con Esplora File
    public void SelezionaCartella_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();

        fbd.Description = "Seleziona una cartella";

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            isCartellaSelezionata=true;
            pathDir = fbd.SelectedPath;
            sfogliaDir.Text = pathDir;
            
        }
    }

    public void SelezionaArchivio_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();

        fbd.Description = "Seleziona un archivio";

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            isArchivioSelezionato=true;
            pathArchivio = fbd.SelectedPath;
            sfogliaArchivio.Text = pathArchivio;
            
        }
    }

    public void IniziaBackup_Click(object sender, EventArgs e)
    {
        if (isArchivioSelezionato && isCartellaSelezionata)
        {
            this.Controls.Clear();
            gestionefs.CreaBackUp(pathDir,pathArchivio,nomeBackup.Text);
            Environment.Exit(0);
        }
        else
        {
            MessageBox.Show("Errore!, devi selezionare tutte le cartelle e archivi");
            return;
        }
    }

    public void IniziaRipristino_CLick(object sender, EventArgs e)
    {
        if (isArchivioSelezionato && isCartellaSelezionata)
        {
            //MessageBox.Show("ripristino");
            bool sovrascrittura_bool = sovrascrittura.Checked;
            this.Controls.Clear();
            gestionefs.RipristinaCartella(nameBackUp,pathArchivio,sovrascrittura_bool);
            Environment.Exit(0);
        }
        else
        {
            MessageBox.Show("Errore!, devi selezionare tutte le cartelle e archivi");
            return;
        }
    }

    public void IniziaEliminazione_Click(object sender, EventArgs e)
    {
        if (isArchivioSelezionato && isCartellaSelezionata)
        {
            //MessageBox.Show("eliminazione");
            this.Controls.Clear();
            gestionefs.EliminaBackUp(nameBackUp,pathArchivio);
            Environment.Exit(0);//esco dal programma
        }
        else
        {
            MessageBox.Show("Errore!, devi selezionare tutte le cartelle e archivi");
            return;
        }
    }

    public void SelezionaArchivio_SelectedIndexChanged(object sender, EventArgs e)
    {
        isArchivioSelezionato = true;
        arcSelezionato.nome = archivi.SelectedItem.ToString();
        List<Archivio> archiviList = GestioneFS.stringToListArchivio();
        pathArchivio = "";

        registriInArchivio.Items.Clear();

        foreach (Archivio a in archiviList)
        {
            if(a.nome.Equals(arcSelezionato.nome)){
                arcSelezionato.path = a.path;
                pathArchivio = a.path;
            }
        }

        string pathRegistroArchivio = pathArchivio+"\\"+"register.txt";
        string contenutoRegistro = File.ReadAllText(pathRegistroArchivio);

        List<Backup> backups = GestioneFS.stringToList(contenutoRegistro);
        //MessageBox.Show(backups == null ? "backups NULL" : backups.Count.ToString());

        foreach (Backup b in backups)
        {
            //MessageBox.Show(b.nome);
            registriInArchivio.Items.Add(b.nome);
        }

        return;
    }

    public void selezionaCartella_SelectedIndexChanged(object sender, EventArgs e)
    {
        isCartellaSelezionata  = true;
        nameBackUp = registriInArchivio.SelectedItem.ToString();
    }
}
