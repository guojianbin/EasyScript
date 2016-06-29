namespace Engine.Bases {

/// <summary>
/// @author Easily
/// </summary>
public class EasyBox<T> {

	public T Value { get; set; }

	public EasyBox(T value) {
		Value = value;
	}

}

}