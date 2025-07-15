namespace TutorHelper.Model
{
    public class Student
    {
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public string ParentEmail { get; set; }
        public bool IsCurrent { get; set; }

        public Student(string studentName, string parentName, string parentEmail, bool isCurrent)
        {
            StudentName = studentName;
            ParentName = parentName;
            ParentEmail = parentEmail;
            IsCurrent = isCurrent;
        }

    }
}