using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace suitsAPI.Models
{
    public class Mammoth
    {
        public Instruction? CurrentInstruction { get; set; }
        public bool IsStarted { get; set; }
        public int ConnectedUsers { get; set; }

        public void StartDemo()
        {
            IsStarted = true;
            //TODO: set CurrentInstruction
        }
    }

}
