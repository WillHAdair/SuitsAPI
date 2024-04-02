using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace suitsAPI.Models
{
    public class Mammoth
    {
        public int instructionID { get; set; }
        public double instructionDuration { get; set; }
        public string description { get; set; }
        public Mammoth(int instruction, double duration, string desc) 
        {
            this.instructionID = instruction;
            this.instructionDuration = duration;
            this.description = desc;
        }
    }   
}
