namespace TryParseSteam
{


    partial class GameDS
    {

    }
}

namespace TryParseSteam.GameDSTableAdapters
{

    public partial class GAME_LISTTableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }

    public partial class GAME_LIST_TEMP_KZTableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }

    public partial class GAME_LIST_TEMP_RUTableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }

    public partial class GAME_LIST_TEMP_TRTableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }

    public partial class GAME_LIST_TEMP_USATableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }



    public partial class OTHER_SITE_LISTTableAdapter
    {
        public int Timeout
        {
            set
            {
                for (int i = 0; i < this.CommandCollection.Length; i++)
                    if (this.CommandCollection[i] != null)
                        this.CommandCollection[i].CommandTimeout = value;
            }
        }
    }
}

