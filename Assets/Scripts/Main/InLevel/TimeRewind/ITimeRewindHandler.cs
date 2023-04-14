public interface ITimeRewindHandler
{
    void ApplyPointInTime(object data);
    object GetPointInTime();
}