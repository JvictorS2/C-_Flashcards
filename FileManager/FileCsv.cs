using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
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

    //CRUD

    public override void CreatOpenArquivo(bool resert=true)
    {
        /* 
            Cria um arquivo e abre o arquivo para uso.
            Caso o arquivo não exista o método irá criar um arquivo e escrever na primeira linha os valores dos campos 
            do arquivo csv, e depois irá deixar o arquivo aberto.
            Caso o arquivo já exista irá apenas abri-lo para uso.
            Também define o id com base no id da ultima linha do arquivo csv.
        */

        //Cria o arquivo
        _sw = new StreamWriter(Path + Name + Extension, resert, Encoding.UTF8);
        _sw.Close();
        
        //Escreve a primeira linha caso ela não exista.
        if(this.ReadAllLinesArquivo().Length == 0)
        {
            _sw = new StreamWriter(Path + Name + Extension, resert, Encoding.UTF8);
            RecordMensage(ConvertArraytoString(FirstLine));
            _sw.Close();
        }

        //Recupera o id atual do arquivo, o ultimo id do arquivo que sempre será o id atual.
        if(this.ReadAllLinesArquivo().Length > 1)
            _id = GetCurrentId();

        //Abre o arquivo
        _sw = new StreamWriter(Path + Name + Extension, resert, Encoding.UTF8);

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

    public void UpdateLineArquivo(int numberLine, Cards card)
    {
        //Converte todas as linhas do arquivo em um array
        string[] lines = ReadAllLinesArquivo();

        //Atualiza a linha desejada
        lines[numberLine - 1] = ConvertArraytoString(ConvertObjectCardToArray(card));

        // Reescreve o arquivo
        File.WriteAllLines(Path + Name + Extension, lines);
    }

   public void RemvoeLineById(string id)
   {

        RemoveLineArquivo(GetLineNumbeById(id));
   }

    public void addObjectCard(Cards card)
    {
        /* 
            Transforma o objeto card em um array e depois escreve no arquivo csv os valores desse array.
            paramentro: objeto card.
        */
        _id++;
        string[] lineCard = ConvertObjectCardToArray(card);
        RecordMensage(ConvertArraytoString(lineCard));
    }

    //Methods de auxilio da classe fileCsv
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

    private string[] ConvertObjectCardToArray(Cards card)
    {
        string[] array = new string[4];
        array[0] = Convert.ToString(_id);
        array[1] = card.CardFront;
        array[2] = card.CardBack;
        array[3] = card.Stats;

        return array;
    }

    private string[] ConvertObjectCardToArray(Cards card, string id)
    {
        string[] array = new string[4];
        array[0] = Convert.ToString(id);
        array[1] = card.CardFront;
        array[2] = card.CardBack;
        array[3] = card.Stats;

        return array;
    }
    
    private int GetCurrentId()
    {
        /* 
            Retrona o valor do Id da ultima linha do arquivo.
        */
        // Ler todas as linhas do arquivo
        string[] Allarray = ReadAllLinesArquivo();
        
        // Transforma a ultima linha do arquivo em um array
        string[] arrayLastLine = ConvertStringToArray(Allarray[Allarray.Length - 1]);

        // Retorna o id do array convertido para int
        return Convert.ToInt32(arrayLastLine[0]);
    }
    
    public string GetLineByid(string id)
    {
        /* 
            Recebe um id, e retornar a linha que possui este id caso encontre
            caso não a encontre irá imprimir uma mensagem de aviso e irá retornar null.
            parâmetro:
                O id da linha em que deseja obter o valor.
            usado em:
                GetLineNumberId()
        */

        // array de string com todas as linhas do arquivo.
        string[] linesString = ReadAllLinesArquivo();

        // array de array de string com todas as linhas do arquivo
        string[][] linesArray = new string[linesString.Length][];

        // converte todos o itens do "linesString" em array e adiciona no array "linesArray"
        for (int i = 0; i < linesString.Length; i++)
        {
            linesArray[i] = ConvertStringToArray(linesString[i]);
        }

        // Procura pelo array que possui a id informado e atribui a um array
        string[][] result = linesArray.Where(array => array[0].Equals(id)).ToArray();

        // try catch para informa que o array não foi encontrado pois aciona uma exception
        try
        {
            //WriteLine(ConvertArraytoString(result[0]));
            return ConvertArraytoString(result[0]);
        }
        catch(IndexOutOfRangeException)
        {
            WriteLine("Id wasn't found!");
        }

        return null;
 
    }

    private int GetLineNumbeById(string id)
    {
        string[] linesStrings = ReadAllLinesArquivo();

        string lineWished = GetLineByid(id);

        for(int i = 0; i < linesStrings.Length; i++)
        {
            if(linesStrings[i].Equals(lineWished))
            {
                return i + 1;
                
            }
        }

        return -1;
    }

}