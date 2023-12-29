using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TricksFile;
#nullable disable
public class Arquivo
{
    //fields
    protected string _name;
    protected string _path;
    protected string _extension;
    protected StreamWriter _sw;
    protected StreamReader _sr;
    
    // propreties
    public string Mensagem { get; set; }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _name = value;
            }
        }
    }

    public string Path
    {
        get
        {
            return _path;
        }
        set
        {
            _path = value;
        }
    }

    public string Extension
    {
        get
        {
            return _extension;
        }
        set
        {
            _extension = value;
        }
    }

    //Constructor
    public Arquivo(string nome, string extension)
    {
        Name = nome;
        Extension = extension;
        Path = "";
    }

    public Arquivo(string nome, string extension, string path) : this(nome,extension)
    {
        Path = path;
    }

    //Methods

    // CRUD ---------------------------------------------------
    public virtual void CreatOpenArquivo(bool resert=true)
    {
        /* 
            Cria um arquivo no caminho informado. o parâmetro é a extensão do arquivo.
            Dev tip: Ele pode sofre overrider por classe filhas.
            Parâmetro:
                resert - define se o arquivo irá ser reescrito do zero (false) ou irá ser escrito normalmente (true - default)
         */
        
        _sw = new StreamWriter(Path + Name + Extension, resert, Encoding.UTF8);

    }

    public virtual void ReadArquivo()
    {
        /* 
            Ler todas as linhas do arquivo.
            Obs: O arquivo precisa está fechado para poder ser lido.
            Dev tip: Ele pode sobre overrider por Classes filhas.
         */

        string linha;
        _sr = new StreamReader(Path + Name + Extension);
        linha = _sr.ReadLine();

        while (linha != null)
        {
            Console.WriteLine(linha);
            linha = _sr.ReadLine();
        }
        _sr.Close();
    }

    public void RecordMensage(string mensage)
    {
        /* 
            Grava uma mensagem no arquivo.
            Parâmetro:
                mensage - texto que será gravado no aquivo.
         */
        _sw.WriteLine(mensage);
    }

    public void RemoveLineArquivo(int whichLine)
    {
        /*  
            Apaga uma linha do arquivo
            parâmetro:
                whichLine - número da linha que irá ser apagada do arquivo

        */

        // Lê todas as linhas do arquivo, exceto a que queremos excluir
        var lines = ReadAllLinesArquivo();

        // Exclui a linha desejada
        lines = lines.Where((line, index) => index + 1 != whichLine).ToArray();

        // Reescreve o arquivo com as linhas restantes
        File.WriteAllLines(Path + Name + Extension, lines);

    }

    public void UpdateLineArquivo(int whichLine, string newline)
    {
        /* 
            Atualiza uma linha do arquivo.
            parâmetro:
                numberline - linha que deseja autlaizar
                newline - texto que irá substituir a linha escolhida
        */

        //Converte todas as linhas do arquivo em um array
        string[] lines = ReadAllLinesArquivo();

        //Atualiza a linha desejada
        lines[whichLine - 1] = newline;

        // Reescreve o arquivo
        File.WriteAllLines(Path + Name + Extension, lines);
    }

    // CRUD ----------------------------------------------------

    public void CloseArquivo()
    {
        /* 
            Fecha o arquivo.
         */
        _sw.Close();
    }

    public string[] ReadAllLinesArquivo()
    {
        /* 
            Devolve um array com todas as linhas do arquivo.
        */
        return File.ReadAllLines(Path + Name + Extension);
       
    }

}

