export class AppStorage {
  public static readonly ApplicationPrefix = "interviewproject";

  public static readonly CurrentUser = "current-user";

  /**
   * Uses a "localStorage" to store a data or delete item if "null" specified.
   * @param key The ksy of stored object.
   * @param item Stored value.
   * */
  static setItem(key: string, item: any): void {
    if (item == null) {
      localStorage.removeItem(`${AppStorage.ApplicationPrefix}.${key}`);
    } else {
      localStorage.setItem(`${AppStorage.ApplicationPrefix}.${key}`, JSON.stringify(item));
    }
  }

  /**
   * Uses a "localStorage" to get a stored data or "null" if nothing found or fetched data could not be parsed.
   * @param key The ksy of stored object.
   * */
  static getItem<T = string>(key: string): T {
    const storedString = localStorage.getItem(`${AppStorage.ApplicationPrefix}.${key}`);

    if (storedString == null) return <T>null;

    try {
      return JSON.parse(storedString) as T;
    } catch (error) {
      return <T>null;
    }
  }
}
