using TricksFile;

void PrintWellcome()
{
    /* 
        print the menu.
    */

    WriteLine("*** Wellcome back, your flashCards have been waiting for you ***");
    WriteLine("1 - Study deck");
    WriteLine("2 - Add card");
    WriteLine("3 - Delete card");
    WriteLine("4 - look deck");
    WriteLine("0 - Shut down program");
    WriteLine();
}

void AddCard(FileCsv fileCsv)
{
    /* 
        Add card with the information front and back the card.
    */

    Write("Type front of card: ");
    string front = ReadLine();

    Write("Type back of card: ");
    string back = ReadLine();

    Cards card = new Cards(front, back);

    fileCsv.addObjectCard(card);
    fileCsv.CloseArquivo();
    WriteLine("");
}



void main()
{

    string[] arrayFCsv = new string[4] { "id", "Card Front", "Card Back", "Sats" };

    FileCsv fileCsv = new FileCsv("DeckEnglishPortuguese", ".csv", arrayFCsv, "data/");

    

    int opcao = -1;
    do
    {
        
        PrintWellcome();
        
        try
        {
            Write("Pick out one of the options: ");
            opcao = Convert.ToInt32(ReadLine());
        }
        catch(FormatException)
        {
            WriteLine("Warning: please, Type only numbers");
            WriteLine("");
        }
        catch(Exception)
        {
            WriteLine("Warning: something didn't work, try again and please, Type only numbers");
            WriteLine("");
        }

        switch (opcao)
        {
            case 1:
                WriteLine("1");
                break;

            case 2:
                fileCsv.CreatOpenArquivo();
                AddCard(fileCsv);
                fileCsv.CloseArquivo();
                break;

            case 3:
                WriteLine("3");   
                break;

            case 4:
                fileCsv.ReadArquivo();
                break;

            default:
                break;

        }

        

    }
    while (!(opcao == 0));

    WriteLine("Thanks, get well S2!");

}



main();

