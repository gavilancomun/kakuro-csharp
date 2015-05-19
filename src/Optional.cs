namespace kakuro {

  using System;
  using System.Linq;
  using System.Collections.Generic;

  public struct Optional<T> {
    private bool hasValue;
    private T value;

    public Optional(T value) {
      if (value == null) throw new ArgumentNullException("value");
      this.hasValue = true;
      this.value = value;
    }

    public static Optional<T> ofNullable(T value) {
      if (null == value) {
        return empty();
      }
      else {
        return new Optional<T>(value);
      }
    }

    public static Optional<T> empty() {
      return new Optional<T>();
    }

    public Optional<TOut> Select<TOut>(Func<T, TOut> selector) {
      return this.hasValue ? new Optional<TOut>(selector(this.value)) : new Optional<TOut>();
    }

    public Optional<TOut> SelectMany<TOut>(Func<T, Optional<TOut>> bind) {
      return this.hasValue ? bind(this.value) : new Optional<TOut>();
    }

    public IEnumerable<T> Where(Func<T, bool> predicate) {
      if (this.hasValue) {
        return predicate(this.value) ? Enumerable.Repeat(this.value, 1) : Enumerable.Empty<T>();
      }
      else {
        return Enumerable.Empty<T>();
      }
    }

    public bool HasValue {
      get { return this.hasValue; }
    }

    public T GetOr(T @default) {
      return this.hasValue ? this.value : @default;
    }

    public T get() {
      return value;
    }
  }
}
