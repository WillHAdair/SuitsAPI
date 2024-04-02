namespace suitsAPI.Models
{
    public class Instruction
    {
        public required string Name { get; set; }
        public required int Code { get; set; }
        public required double TimeToComplete { get; set; }
        public Instruction? Next { get; set; }
    }
}
