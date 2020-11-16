namespace zxVehicle.plane
{
    public class GameData  {

        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public GameData() { }
        public GameData(string id) {
            this.iD = id;
        }
    }

}