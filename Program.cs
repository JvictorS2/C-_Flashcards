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
    WriteLine("4 - Update card");
    WriteLine("5 - look deck");
    WriteLine("0 - Shut down program");
    WriteLine();
}

void AddCard(FileCsv fileCsv)
{
    /* 
        Add card with the information front and back the card.
        Parameters:
            object FileCsv, object that represent a file.
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

void RemoveCard(FileCsv fileCsv)
{
    /* 
        Remove the card of deck;
    */

    // Validing input.
    bool isvalid;
    string id = null;

    do
    {
        isvalid = true;

        WriteLine("Type the id of card that you want remove for you deck: ");
        Write("Obs: apenas números e 0 para cancelar: ");

        id = ReadLine();

        // back to menu
        if (id.Equals("0"))
        {
            WriteLine();
            WriteLine("Backing to menu...");
            WriteLine();
            return;
        }

        // Valid if the id exist
        if (fileCsv.GetLineByid(Convert.ToString(id)) == null)
        {
            isvalid = false;
            WriteLine();
            WriteLine("Sorry, Your id wasn't found, try again.");
            WriteLine();
        }

    }
    while (!isvalid);

    fileCsv.RemvoeLineById(id);

}

void UpdateCard(FileCsv fileCsv)
{
    /* 
        Update a card
        
    */
}



void main()
{
    /*  
        Program main, here there is a menu with all options to the deck
    */

    // First line of the file csv
    string[] arrayFCsv = new string[4] { "id", "Card Front", "Card Back", "Sats" };

    // Object filecsv
    FileCsv fileCsv = new FileCsv("DeckEnglishPortuguese", ".csv", arrayFCsv, "data/");

    // Menu
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
            // Study deck
            case 1:
                WriteLine("1");
                break;

            // Add card
            case 2:
                fileCsv.CreatOpenArquivo();
                AddCard(fileCsv);
                fileCsv.CloseArquivo();
                break;

            // Remove card
            case 3:
                RemoveCard(fileCsv);
                break;

            // Update card
            case 4:
                UpdateCard(fileCsv);
                break;

            // Read all deck
            case 5:
                fileCsv.ReadArquivo();
                break;

            default:
                WriteLine();
                WriteLine("Invalid input, please pick out only that opions avaiable!");
                WriteLine();
                break;

        }  

    }
    while (!(opcao == 0));

    WriteLine("Thanks, get well S2!");
}


main();
