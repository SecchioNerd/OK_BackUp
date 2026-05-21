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
    public const string registroArchivio = "register.txt";
    public const string registroSupremo = "E:\\program\\elenco_archivi.txt";


    //Path Dir          =       dove è la cartella di cui fare il backUp
    //Path Archivio     =       dove va salvato lo zip del 
    public void CreaBackUp(string pathDir,string pathArchivio,string nameBackUp)
    {
        string pathRegistroArchivio = Path.Combine(pathArchivio,registroArchivio);
        if (!Directory.Exists(pathArchivio))
        {
            Directory.CreateDirectory(pathArchivio);
            File.AppendAllText("E:\\program\\elencoArchivi.txt",pathArchivio+";"+Environment.NewLine);
        }
        if(!File.Exists(pathRegistroArchivio))
            File.AppendAllText(registroSupremo,"<"+pathArchivio+">;"+Environment.NewLine);

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
            //Console.WriteLine("nome: "+campoRegistro.nome+"     ||   path: " +campoRegistro.pathRipristino+"\n");
            Console.WriteLine("nome: "+campoRegistro.nome+"     ||   path: " +campoRegistro.pathRipristino+"\n");
            if(nameBackUp.Equals(campoRegistro.nome)){
                pathRipristino=campoRegistro.pathRipristino;
                break;
            }
        }
        //Console.WriteLine("path cartella: "+pathRipristino);

        if (sovrascrittura)
        {
            ZipFile.ExtractToDirectory(pathEstrazione,pathRipristino,sovrascrittura);
        }
        else
        {
            if(Directory.Exists(pathRipristino)){
                int copia = 0;
                do
                {
                    copia++;                    
                }while(Directory.Exists(pathRipristino+"("+copia+")"));
                pathRipristino+="("+copia+")";
            }

            ZipFile.ExtractToDirectory(pathEstrazione,pathRipristino,sovrascrittura);
        }
        //viene sovrascritto tutto se ( Sovrascrittura == true )
    }

    public void EliminaBackUp(string nameBackUp,string pathArchivio)
    {
        string pathRegistroArchivio = pathArchivio+"\\"+registroArchivio;
        string contenutoRegistro = File.ReadAllText(pathRegistroArchivio);
        Backup eliminazione = new Backup(nameBackUp,"");
        eliminazione.nome = nameBackUp;

        List<Backup> registro = stringToList(contenutoRegistro);
        List<Backup> newRegistro = new List<Backup>();

        foreach(Backup campo in registro)
        {
            if (!nameBackUp.Equals(campo.nome))
            {
                newRegistro.Add(campo);
            }
        }

        File.Delete(pathArchivio+"\\"+nameBackUp+".zip");
        File.Delete(pathRegistroArchivio);
        //File.Create(pathRegistroArchivio);
        //File.Create(pathRegistroArchivio).Close();
        contenutoRegistro = "";

        foreach (Backup campoRegistro in newRegistro)
        {
            contenutoRegistro+="<"+campoRegistro.nome+"><"+campoRegistro.pathRipristino+">;"+Environment.NewLine;
        }
        File.AppendAllText(pathRegistroArchivio,contenutoRegistro);  //  <- riga che solleva l' eccezione
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
                    contaMax = 0;
                    contaMin = 0;
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

    public static List<Archivio> stringToListArchivio()
    {
        string contenutoRegistro = File.ReadAllText(registroSupremo);
        List<Archivio> registro = new List<Archivio>();
        Archivio campoRegistro = new Archivio();
        int contaMin=0;
        int contaMax=0;

        foreach(char carattere in contenutoRegistro)
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
                    campoRegistro.calcolaNome(campoRegistro.path);
                    registro.Add(campoRegistro);
                    campoRegistro = new Archivio();
                    contaMin = 0;
                    contaMax=0;
                    Console.WriteLine("\n"+campoRegistro);
                    break;
                default:
                    if(contaMin==1 && contaMax==0)
                        campoRegistro.path+=carattere;
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

    public void calcolaNome(string path)
    {
        this.nome = "";
        this.path=path;
        int lengthStart = 0;
        for (int i = 0; i < path.Length; i++)
        {
            if(path[i]=='\\')
                lengthStart=i;
        }
        for (int i=lengthStart+1;i<path.Length;i++)
        {
            this.nome+=path[i];
        }
    }
    public Archivio(string nome, string path)
    {
        this.nome=nome;
        this.path=path;
    }

    public Archivio()
    {
        this.nome = "";
        this.path="";
    }
}
