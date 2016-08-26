using RESTaurant.App.Domain;

namespace RESTaurant.App.Repository
{
    class AddressDocument
    {
        public string Building { get; private set; }
        public Coordinate Coord { get; private set; }
        public string Street { get; private set; }
        public string Zipcode { get; private set; }
    }
}
