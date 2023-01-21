namespace Broker.Responses
{
    public class UpdateLibraryResponse
    {
        public Guid? Id { get; set; }
        public bool IsSuccess { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
