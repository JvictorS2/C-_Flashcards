
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TricksFile;
#nullable disable

public class FileCsv : Arquivo
{

    private string[] _fisrtLine;

    //id referente ao arquivo csv deste objeto.
    private int _id;

    public string[] FirstLine
    {
        get
        {
            return _fisrtLine;
        }
        set
        {
            _fisrtLine = value;
        }
    }

    //Constructor
    public FileCsv(string nome, string extension, string[] firstLine) : base(nome,extension)
    {
        FirstLine = firstLine;
        _id = 0;
    }

    public FileCsv(string nome, string extension, string[] firstLine, string path) : this(nome, extension,firstLine)
    {
        Path = path;
    }

    //Method for classe Filecsv

    public void addObjectCard(Cards card)
    {
        /* 
            Transforma o objeto card em um array e depois escreve no arquivo csv os valores desse array
        */
        _id++;
        string[] array = new string[4];
        array[0] = Convert.ToString(_id);
        array[1] = card.CardFront;
        array[2] = card.CardBack;
        array[3] = card.Stats;

        RecordMensage(array);
    }

    public override void CreatOpenArquivo()
    {
        /* 
            Cria um arquivo e abre o arquivo para uso.
            Caso o arquivo não exista o método irá criar um arquivo e escrever na primeira linha os valores dos campos 
            do arquivo csv, e depois irá deixar o arquivo aberto.
            Caso o arquivo já exista irá apenas abri-lo para uso.
            Também define o id com base no id da ultima linha do arquivo csv.
        */

        //Cria o arquivo
        _sw = new StreamWriter(Path + Name + Extension, true, Encoding.UTF8);
        _sw.Close();
        
        //Escreve a primeira linha caso ela não exista.
        if(this.ReadAllLines().Length == 0)
        {
            _sw = new StreamWriter(Path + Name + Extension, true, Encoding.UTF8);
            RecordMensage(FirstLine);
            _sw.Close();
        }
       
        if(this.ReadAllLines().Length > 1)
            _id = CurrentId();

        _sw = new StreamWriter(Path + Name + Extension, true, Encoding.UTF8);

    }

    public void RecordMensage(string[] mensage)
    {
        /* 
            Method overchage
            Receber um array convert em uma string separada por ";" e escreve no arquivo csv.
         */
        
        RecordMensage(ConvertArraytoString(mensage));
    }

    public override void ReadArquivo()
    {
        /* 
            Ler todas as linhas do arquivo. Exibir de maneira "Organizada" no terminal.
            Obs: O arquivo precisa está fechado para poder ser lido.
        */

        string line;
        _sr = new StreamReader(Path + Name + Extension);
        line = _sr.ReadLine();

        WriteLine();
        WriteLine("***** Show me all cards *****");
        WriteLine();

        while (line != null)
        {

            if(line != ConvertArraytoString(FirstLine))
            {
                string[] arrayLine = ConvertStringToArray(line);

                for (int i = 0; i < arrayLine.Length; i++)
                {
                    if (i != 0)
                        Write("  /  ");
                    Write($"{arrayLine[i]}");

                }

                WriteLine("");
                WriteLine("");
            }

            line = _sr.ReadLine();
        }

        WriteLine();

        _sr.Close();
    }

    //Method into classe filecsv
    private string ConvertArraytoString(string[] array)
    {
        /* 
            Converte um array em uma string.
            usada para transformar um array em uma string para ser escrita na linha do arquivo.
        */
        string mensagem = "";

        for (int i = 0; i < array.Length; i++)
        {
            if (i != array.Length - 1)
                mensagem += array[i] + ";";
            else
                mensagem += array[i];
        }

        return mensagem;
    }

    private string[] ConvertStringToArray(string str)
    {
        /* 
            Converte uma string em um array.
            usada para transformar uma linha do arquivo em um array.
        */
        string[] array = str.Split(";");
        return array;
    }

    private int CurrentId()
    {
        /* 
            Retrona o valor do Id da ultima linha do arquivo.
        */
        string[] Allarray = ReadAllLines();
        //WriteLine(Allarray.Length);
        //WriteLine(Allarray[Allarray.Length - 1]);
        string[] arrayLastLine = ConvertStringToArray(Allarray[Allarray.Length - 1]);
        return Convert.ToInt32(arrayLastLine[0]);
    }
}