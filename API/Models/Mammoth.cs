namespace API.Models
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
