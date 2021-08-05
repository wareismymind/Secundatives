namespace wimm.Secundatives
{
    /// <summary>
    /// A semantic type representing a lack of value.
    /// </summary>
    /// <remarks>
    /// None is primarily used to indicate the absence of a value in a <see cref="Maybe{T}"/> and to represent the lack of a 
    /// value in <see cref="Result{T, TError}"/> when semantically a type of <see cref="Maybe{TError}"/> would not make sense.
    /// </remarks>
    public class None { }
}
