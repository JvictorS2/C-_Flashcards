namespace TricksFile;

public class Cards
{
    //Fields
    private string _cardFront;
    private string _cardBack;
    private string _stats;

    //Propreties

    public string CardFront
    {
        get
        {
            return _cardFront;
        }

        set
        {
            _cardFront = value;
        }
    }

    public string CardBack
    {
        get
        {
            return _cardBack;
        }

        set
        {
            _cardBack = value;
        }
    }

    public string Stats
    {
        get
        {
            return _stats;
        }
        set
        {
            _stats = value;
        }
    }

    //Constructor
    public Cards(string front, string back)
    {
        CardFront = front;
        CardBack = back;
        Stats = "it haven't studied yet";
    }

}