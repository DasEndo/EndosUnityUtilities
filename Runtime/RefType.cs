public class RefType<T>
{
	public T Value { get; set; }
	
	public RefType(T value)
	{
		Value = value;
	}
	
	public RefType()
	{
		Value = default;
	}
}