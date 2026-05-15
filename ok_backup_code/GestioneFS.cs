//scoreggia di topo

using System;
using System.IO;
using System.IO.Compression;

namespace prova;

public class GestioneFS
{
    //nell' archivio oltre agli ZIP c'è anche un registro
    // il registro contiene per ogni ZIP   --->   -<NomeZIP><PathDiEstrazione>;
    //nome del registro
    private const string registroArchivio = "register.txt";

    //Path Dir          =       dove è la cartella di cui fare il backUp
    //Path Archivio     =       dove va salvato lo zip del 
    public void CreaBackUp(string pathDir,string pathArchivio,string nameArchivio,string nameBackUp)
    {
        string pathRegistroArchivio = Path.Combine(pathArchivio,registroArchivio);
        if (!Directory.Exists(pathArchivio))
        {
            Directory.CreateDirectory(pathArchivio);
            File.AppendAllText("E:\\program\\elencoArchivi.txt",pathArchivio+";"+Environment.NewLine);
        }

        //creo lo zip nella cartella archivio
        ZipFile.CreateFromDirectory(pathDir,pathArchivio+"\\"+nameBackUp+".zip"); //RINOMINARE

        //va agginta la parte del controllo doppi
        File.AppendAllText(pathRegistroArchivio,"<"+nameBackUp+"><"+pathDir+">;"+Environment.NewLine);
    }

    public void RipristinaCartella(string nameBackUp,string pathArchivio, bool sovrascrittura)
    {
        string pathEstrazione = pathArchivio+"\\"+nameBackUp+".zip";
        string pathRegistroArchivio = pathArchivio+"\\"+registroArchivio;
        string contenutoRegistro = File.ReadAllText(pathRegistroArchivio);
        string pathRipristino="";
        
        List<Backup> registro = stringToList(contenutoRegistro);
        Console.WriteLine("path zip: "+pathEstrazione);
        foreach(Backup campoRegistro in registro)
        {
            Console.WriteLine("nome: "+campoRegistro.nome+"     ||   path: " +campoRegistro.pathRipristino+"\n");
            if(nameBackUp.Equals(campoRegistro.nome)){
                pathRipristino=campoRegistro.pathRipristino;
                break;
            }
        }
        Console.WriteLine("path cartella: "+pathRipristino);

        if (sovrascrittura)
        {
            Directory.Delete(pathRipristino, true);
        }
        else
        {
            if(Directory.Exists(pathRipristino)){
                int copia = 0;
                do
                {
                    copia++;
                }while(Directory.Exists(pathRipristino+"("+copia+")"));
            }
        }
        //viene sovrascritto tutto se ( Sovrascrittura == true )
        ZipFile.ExtractToDirectory(pathEstrazione,pathRipristino,sovrascrittura);
    }

    public void eliminaBackUp(string nameBackUp,string pathArchivio)
    {
        string pathRegistroArchivio = pathArchivio+"\\"+registroArchivio;
        string contenutoRegistro = File.ReadAllText(pathRegistroArchivio);
        Backup eliminazione = new Backup(nameBackUp,"");

        List<Backup> registro = stringToList(contenutoRegistro);

        foreach(Backup campo in registro)
        {
            if (nameBackUp.Equals(campo.nome))
            {
                eliminazione.pathRipristino =campo.pathRipristino;
                break;
            }
        }

        File.Delete(pathArchivio+"\\"+nameBackUp+".zip");
        registro.Remove(eliminazione);

        File.Delete(pathRegistroArchivio);
        File.Create(pathRegistroArchivio);

        foreach (Backup campoRegistro in registro)
        {
            File.AppendAllText(pathRegistroArchivio,"<"+campoRegistro.nome+"><"+campoRegistro.pathRipristino+">;"+Environment.NewLine);
        }
    }

    public static List<Backup> stringToList(string stringaRegistro)
    {
        List<Backup> registro = new List<Backup>();
        Backup campoRegistro =new Backup("","");//elemento di ogni registro
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
                    campoRegistro = new Backup("","");
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

    public Archivio(string nome, string path)
    {
        this.nome=nome;
        this.path=path;
    }
}
