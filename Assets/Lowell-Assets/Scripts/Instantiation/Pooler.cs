public interface Pooler<T> {
    // Immediately stows the given T.
    public void Release(T obj);
}