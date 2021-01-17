namespace Communications.Responses
{
    public interface DestinationAddressResponse
    {
        public string DestinationType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}