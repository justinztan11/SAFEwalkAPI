namespace SafewalkApplication.Contracts
{
    public interface IPerson
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string Token { get; set; }
        public string SocketId { get; set; }
    }
}
