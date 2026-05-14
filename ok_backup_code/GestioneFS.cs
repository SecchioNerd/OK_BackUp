//scoreggia di topo

using System;
using System.IO;
using System.IO.Compression;

namespace ok_backup_code;

public class GestioneFS
{
    private const string registroArchivio = "register.txt";

    //Path Dir          =       dove è la cartella di cui fare il backUp
    //Path Archivio     =       dove va salvato lo zip del 
    public void CreaBackUp(string pathDir,string pathArchivio,string nameArchivio,string nameBackUp)
    {
        string pathRegistroArchivio = Path.Combine(pathArchivio,registroArchivio);
        if (!Directory.Exists(pathArchivio))
        {
            Directory.CreateDirectory(pathArchivio);
            File.AppendAllText("@C:Program/OK_BackUp/elencoArchivi.txt",pathArchivio+";"+Environment.NewLine);//aggiungo al registro di elenco degli archivi
        }

        //creo lo zip nella cartella archivio
        ZipFile.CreateFromDirectory(pathDir,pathArchivio+".zip"); //RINOMINARE

        //va agginta la parte del controllo doppi
        File.AppendAllText(pathRegistroArchivio,"<"+nameArchivio+"><"+pathArchivio+";"+Environment.NewLine);
    }

    public void RipristinaCartella(string nameBackUp,string pathArchivio, bool sovrascrittura)
    {
        string pathEstrazione = Path.Combine(pathArchivio,(nameBackUp+".zip"));
        string pathRegistroArchivio = Path.Combine(pathArchivio,registroArchivio);
        string contenutoRegistro = File.ReadAllText(pathRegistroArchivio);
        string pathRipristino="";
        
        List<Campo> registro = stringToList(contenutoRegistro);

        foreach(Backup campoRegistro in registro)
        {
            if(nameBackUp.Equals(campoRegistro.nome))
                pathRipristino=campoRegistro.pathRipristino;
        }

        //viene sovrascritto tutto se ( Sovrascrittura == true )
        ZipFile.ExtractToDirectory(pathRegistroArchivio,pathRipristino,sovrascrittura);
    }

    private static List<Backup> stringToList(string stringaRegistro)
    {
        List<Backup> registro = new List<Backup>();
        Backup campoRegistro =new Campo("","");//elemento di ogni registro
        int contaMin=0;
        int contaMax=0;

        foreach(char carattere in stringaRegistro)
        {
            switch (carattere)
            {
                case '<':
                    contaMin++;
                    break;
                case '>':
                    contaMax++;
                    break;
                case ';':
                    registro.Add(campoRegistro);
                    campoRegistro = new Campo("","");
                    break;
                default:
                    if(contaMin==1 && contaMax==0)
                        campoRegistro.nome+=carattere;
                    else if(contaMin==2 && contaMax==1)
                        campoRegistro.pathRipristino+=carattere;
                    break;
            }
        }

        return registro;
    }
}

public class Backup
{
    public string nome;
    public string pathRipristino;

    public Backup(string nome, string pathRipristino)
    {
        this.nome=nome;
        this.pathRipristino=pathRipristino;
    }
}

public class Archivio
{
    public string nome;
    public string path;

    public Archivio(string nome, string pathRipristino)
    {
        this.nome=nome;
        this.pathRipristino=pathRipristino;
    }
}
