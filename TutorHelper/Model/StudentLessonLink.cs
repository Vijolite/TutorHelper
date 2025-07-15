namespace TutorHelper.Model
{
    public class StudentLessonLink
    {
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        public decimal Price { get; set; }
        public string UsualWeekDay { get; set; }
        public TimeOnly UsualTime { get; set; }
        public bool IsActual { get; set; }

        public StudentLessonLink (Student student, Lesson lesson, decimal price, string usualWeekDay, TimeOnly usualTime, bool isActual = true)
        {
            Student = student;
            Lesson = lesson;
            Price = price;
            UsualWeekDay = usualWeekDay;
            UsualTime = usualTime;
            IsActual = isActual;
        }
    }
}
